using System;
using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WinConditions _winCondition;
        [SerializeField] private LooseConditions _looseCondition;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ShooterCharacter _playerCharacterPrefab;
        [SerializeField] private CollisionDamageCharacter _enemyCharacterPrefab;
        [SerializeField] private Transform _playerStart;
        [SerializeField] private Transform _enemySpawnPoints;
        [SerializeField] private float _enemySpawnCooldown;
        [SerializeField] private float _enemyDirectionChangeFrequency;
        [SerializeField] private ResultView _resultView;

        private void OnValidate()
        {
            _enemySpawnCooldown = Mathf.Max(_enemySpawnCooldown, 0);
        }

        private void Awake()
        {
            CheckExceptions();
            
            var inputDetector = new InputDetector(this);
            var factory = new Factory(_playerCharacterPrefab, _enemyCharacterPrefab);
            var pool = new Pool(factory);
            var enemiesSpawner = new EnemiesCooldownSpawner(this, _enemySpawnPoints, factory, _enemySpawnCooldown, _enemyDirectionChangeFrequency);
            
            var game = new Game(_winCondition, _looseCondition, _camera, factory, pool, _playerStart, inputDetector, enemiesSpawner, _resultView);

            game.Start();
        }
        
        private void CheckExceptions()
        {
            if (_camera is null) throw new NullReferenceException("Camera is null");
            if (_playerCharacterPrefab is null) throw new NullReferenceException("PlayerCharacterPrefab is null");
            if (_enemyCharacterPrefab is null) throw new NullReferenceException("EnemyCharacterPrefab is null");
            if (_playerStart is null) throw new NullReferenceException("PlayerStart is null");
            if (_enemySpawnPoints is null) throw new NullReferenceException("EnemySpawnPoints is null");
            if (_resultView is null) throw new NullReferenceException("ResultView is null");
        }
    }
}