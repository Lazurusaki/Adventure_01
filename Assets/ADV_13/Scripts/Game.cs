using System;
using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Game
    {
        private readonly GameMode _gameMode;
        private readonly CinemachineVirtualCamera _camera;
        private readonly CharacterFabric _characterFabric;
        private readonly CharacterPool _characterPool;
        private readonly Transform _playerStart;
        private readonly EnemiesCooldownSpawner _enemiesSpawner;
        private readonly PlayerController _playerController;
        private readonly ResultView _resultView;
        private readonly ObservableList<Character> _enemies;
        private readonly KillCounter _killCounter;

        private Character _playerCharacter;

        public Game(GameMode gameMode,
            CinemachineVirtualCamera camera,
            CharacterFabric characterFabric,
            CharacterPool characterPool,
            Transform playerStart,
            InputDetector inputDetector,
            EnemiesCooldownSpawner enemiesSpawner,
            ResultView resultView)
        {
            _gameMode = gameMode;
            _camera = camera;
            _characterFabric = characterFabric;
            _enemies = new ObservableList<Character>();
            _killCounter = new KillCounter();
            _characterPool = characterPool;
            _playerStart = playerStart;
            _enemiesSpawner = enemiesSpawner;
            _playerController = new PlayerController(inputDetector);
            _resultView = resultView;

            _enemiesSpawner.EnemySpawned += OnEnemySpawned;
            _resultView.Completed += OnGameOver;
        }

        public void Start()
        {
            _playerCharacter = _characterFabric.SpawnCharacter(CharacterTypes.Player, _playerStart.position);
            _gameMode.Initialize(_playerCharacter, _killCounter, _enemies);
            _playerController.SetCharacter(_playerCharacter);
            _camera.Follow = _playerCharacter.transform;
            _playerController.Enable();
            _enemiesSpawner.Start();

            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill += OnKill;

            _gameMode.Win += OnWin;
            _gameMode.Loose += OnLoose;
        }

        private void Stop()
        {
            _enemiesSpawner.Stop();
            _playerController.Disable();

            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill -= OnKill;

            _gameMode.Win -= OnWin;
            _gameMode.Loose -= OnLoose;
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
        }

        private void OnGameOver()
        {
            _characterPool.ClearAll();
            _enemies.Clear();
            _killCounter.Reset();
            Start();
        }
    }
}