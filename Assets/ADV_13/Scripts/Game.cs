using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Game
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly Factory _factory;
        private readonly Pool _pool;
        private readonly Transform _playerStart;
        private readonly EnemiesCooldownSpawner _enemiesSpawner;
        private readonly PlayerController _playerController;
        private readonly ResultView _resultView;
        private readonly WinConditions _winCondition;
        private readonly LooseConditions _looseCondition;
        private readonly ObservableList<Character> _enemies;
        private readonly KillCounter _killCounter;

        private GameMode _gameMode;
        private Character _playerCharacter;

        public Game(WinConditions winCondition,
            LooseConditions looseCondition,
            CinemachineVirtualCamera camera,
            Factory factory,
            Pool pool,
            Transform playerStart,
            InputDetector inputDetector,
            EnemiesCooldownSpawner enemiesSpawner,
            ResultView resultView)
        {
            _winCondition = winCondition;
            _looseCondition = looseCondition;
            _camera = camera;
            _factory = factory;
            _enemies = new ObservableList<Character>();
            _killCounter = new KillCounter();
            _pool = pool;
            _playerStart = playerStart;
            _enemiesSpawner = enemiesSpawner;
            _enemiesSpawner.EnemySpawned += OnEnemySpawned;
            _playerController = new PlayerController(inputDetector);
            _resultView = resultView;
            _resultView.Completed += OnShowResultCompleted;
        }

        public void Start()
        {
            _playerCharacter = _factory.SpawnCharacter(CharacterTypes.Player, _playerStart.position);
            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill += OnKill;

            _playerController.SetCharacter(_playerCharacter);
            _camera.Follow = _playerCharacter.transform;

            _gameMode = new GameMode(_playerCharacter, _enemies, _killCounter, _winCondition, _looseCondition);
            _gameMode.Win += OnWin;
            _gameMode.Loose += OnLoose;

            _playerController.Enable();
            _enemiesSpawner.Start();
        }

        private void Stop()
        {
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
            _pool.Clear(character.transform);
            _enemies.Remove(character);
        }

        private void OnShowResultCompleted()
        {
            Restart();
        }

        private void Restart()
        {
            _pool.ClearAll();
            _enemies.Clear();
            _killCounter.Reset();

            if (_playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill -= OnKill;

            _gameMode.Win -= OnWin;
            _gameMode.Loose -= OnLoose;
            Start();
        }
    }
}