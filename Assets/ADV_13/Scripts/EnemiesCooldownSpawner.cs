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
        private readonly MonoBehaviour _owner;
        private readonly List<Vector3> _positions;
        private readonly Factory _factory;
        private readonly float _cooldown;
        private readonly float _enemyDirectionChangeFrequency;

        private Coroutine _spawnCoroutine;

        private bool _isEnabled;

        public event Action<Character> EnemySpawned;

        public EnemiesCooldownSpawner(MonoBehaviour owner, Transform spawnPointsContainer, Factory factory,
            float spawnCooldown, float enemyDirectionChangeFrequency)
        {
            _owner = owner;
            _positions = spawnPointsContainer.Cast<Transform>()
                .Select(point => point.position)
                .ToList();

            _factory = factory;
            _cooldown = spawnCooldown;
            _enemyDirectionChangeFrequency = enemyDirectionChangeFrequency;
        }

        public void Start()
        {
            if (_spawnCoroutine is not null)
                _owner.StopCoroutine(_spawnCoroutine);

            _isEnabled = true;
            _spawnCoroutine = _owner.StartCoroutine(CooldownSpawn());
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
                var character = _factory.SpawnCharacter(CharacterTypes.Enemy, _positions[randomIndex]);
                var aiController = new AIWandererController(character, _enemyDirectionChangeFrequency);
                aiController.Activate();
                EnemySpawned?.Invoke(character);
                yield return cooldown;
            }
        }
    }
}