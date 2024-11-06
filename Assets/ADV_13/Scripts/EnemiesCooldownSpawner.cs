using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

namespace ADV_13
{
    public class EnemiesCooldownSpawner
    {
        private readonly MonoBehaviour _coroutineHost;
        private readonly List<Vector3> _positions;
        private readonly CharacterFactory _characterFactory;
        private readonly float _cooldown;
        private readonly float _enemyDirectionChangeFrequency;

        private Coroutine _spawnCoroutine;

        private bool _isEnabled;

        public event Action<Character> EnemySpawned;

        public EnemiesCooldownSpawner(MonoBehaviour coroutineHost, Transform spawnPointsContainer, CharacterFactory characterFactory,
            float spawnCooldown, float enemyDirectionChangeFrequency)
        {
            _coroutineHost = coroutineHost;
            _positions = spawnPointsContainer.Cast<Transform>()
                .Select(point => point.position)
                .ToList();

            _characterFactory = characterFactory;
            _cooldown = spawnCooldown;
            _enemyDirectionChangeFrequency = enemyDirectionChangeFrequency;
        }

        public void Start()
        {
            if (_spawnCoroutine is not null)
                _coroutineHost.StopCoroutine(_spawnCoroutine);

            _isEnabled = true;
            _spawnCoroutine = _coroutineHost.StartCoroutine(CooldownSpawn());
        }

        public void Stop()
        {
            _isEnabled = false;
        }

        private IEnumerator CooldownSpawn()
        {
            var cooldown = new WaitForSeconds(_cooldown);

            while (_isEnabled)
            {
                var randomIndex = Random.Range(0, _positions.Count);
                var character = _characterFactory.SpawnCharacter(CharacterTypes.Enemy, _positions[randomIndex]);
                var aiController = new AIWandererController(character, _enemyDirectionChangeFrequency);
                aiController.Activate();
                EnemySpawned?.Invoke(character);
                yield return cooldown;
            }
        }
    }
}