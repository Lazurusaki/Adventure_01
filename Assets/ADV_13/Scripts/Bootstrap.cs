using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADV_13
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private EndGameConditions _winCondition;
        [SerializeField] private EndGameConditions _looseCondition;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ShooterCharacter _playerCharacterPrefab;
        [SerializeField] private CollisionDamageCharacter _enemyCharacterPrefab;
        [SerializeField] private Transform _playerStart;
        [SerializeField] private Transform _enemySpawnPoints;
        [SerializeField] private ResultView _resultView;
        
        [SerializeField] private float _enemySpawnCooldown;
        [SerializeField] private float _enemyDirectionChangeFrequency;
        
        private void OnValidate()
        {
            _enemySpawnCooldown = Mathf.Max(_enemySpawnCooldown, 0);
            _enemyDirectionChangeFrequency= Mathf.Max(_enemyDirectionChangeFrequency, 0);
        }

        private void Awake()
        {
            CheckExceptions();
            
            var inputDetector = new InputDetector(this);
            var characterFactory = new CharacterFabric(_playerCharacterPrefab, _enemyCharacterPrefab);
            var characterPool = new CharacterPool(characterFactory);
            var enemiesSpawner = new EnemiesCooldownSpawner(this, _enemySpawnPoints, characterFactory, _enemySpawnCooldown, _enemyDirectionChangeFrequency);
            
            var killCounter = new KillCounter();
            var characterHolder = new CharacterHolder();
            var enemies = new ObservableList<Character>();
            var conditionFabric = new ConditionFabric(this, characterHolder, enemies, killCounter);
            var winCondition = conditionFabric.CreateCondition(_winCondition);
            var looseCondition = conditionFabric.CreateCondition(_looseCondition);
            
            var game = new Game(
                winCondition,
                looseCondition,
                _camera,
                characterFactory,
                characterPool,
                _playerStart,
                inputDetector,
                enemiesSpawner,
                characterHolder,
                killCounter,
                enemies,
                _resultView
            );     
            
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