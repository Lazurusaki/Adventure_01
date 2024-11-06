using System;
using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Game
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly CharacterFactory _characterFactory;
        private readonly CharacterPool _characterPool;
        private readonly Transform _playerStart;
        private readonly EnemiesCooldownSpawner _enemiesSpawner;
        private readonly PlayerController _playerController;
        private readonly ResultView _resultView;
        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;
        private readonly ObservableList<Character> _enemies;
        private readonly KillCounter _killCounter;

        private GameMode _gameMode;
        private Character _playerCharacter;

        public event Action PlayerDied;
        public event Action Kill;
        public event Action EnemiesChanged;
        public event Action GameOver;

        public Game(ICondition winCondition, ICondition looseCondition,
            CinemachineVirtualCamera camera,
            CharacterFactory characterFactory,
            CharacterPool characterPool,
            Transform playerStart,
            InputDetector inputDetector,
            EnemiesCooldownSpawner enemiesSpawner,
            ResultView resultView)
        {
            _camera = camera;
            _characterFactory = characterFactory;
            _enemies = new ObservableList<Character>();
            _killCounter = new KillCounter();
            _characterPool = characterPool;
            _playerStart = playerStart;
            _enemiesSpawner = enemiesSpawner;
            _playerController = new PlayerController(inputDetector);
            _resultView = resultView;
            _winCondition = winCondition;
            _looseCondition = looseCondition;
            
            _enemiesSpawner.EnemySpawned += OnEnemySpawned;
            _resultView.Completed += OnGameOver;
        }

        public void Start()
        {
            _playerCharacter = _characterFactory.SpawnCharacter(CharacterTypes.Player, _playerStart.position);
            _playerController.SetCharacter(_playerCharacter);
            
            

            // ConditionFabric conditionFabric = new ConditionFabric(_playerCharacter, _killCounter, _enemies);
            // conditionFabric.InitializeCondition(_winCondition);
            // conditionFabric.InitializeCondition(_looseCondition);
            // _winCondition.Completed += OnWin;
            // _looseCondition.Completed += OnLoose;
            
            _camera.Follow = _playerCharacter.transform;
            _playerController.Enable();
            _enemiesSpawner.Start();
            
            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill += OnKill;
        }

        private void Stop()
        {
            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill -= OnKill;
            
            _winCondition.Completed -= OnWin;
            _looseCondition.Completed -= OnLoose;
            _enemiesSpawner.Stop();
            _playerController.Disable();
        }

        private void OnWin()
        {
            Stop();
            _resultView.Show(Results.Win);
        }

        private void OnLoose()
        {
            Stop();
            _resultView.Show(Results.Loose);
        }

        private void OnKill()
        {
            _killCounter.AddKill();
        }

        private void OnEnemySpawned(Character character)
        {
            character.DeathCompleted += OnCharacterDeath;
            _enemies.Add(character);
        }

        private void OnCharacterDeath(Character character)
        {
            character.DeathCompleted -= OnCharacterDeath;
            _characterPool.Clear(character.transform);
            _enemies.Remove(character);

            if (character == _playerCharacter)
                PlayerDied?.Invoke();
        }
        
        private void OnGameOver()
        {
            _characterPool.ClearAll();
            _enemies.Clear();
            _killCounter.Reset();
            GameOver?.Invoke();
        }
    }
}