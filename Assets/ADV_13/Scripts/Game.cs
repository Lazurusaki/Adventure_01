using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Game
    {
        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;
        private readonly CinemachineVirtualCamera _camera;
        private readonly CharacterFabric _characterFabric;
        private readonly CharacterPool _characterPool;
        private readonly Transform _playerStart;
        private readonly EnemiesCooldownSpawner _enemiesSpawner;
        private readonly PlayerController _playerController;
        private readonly ResultView _resultView;
        private readonly CharacterHolder _characterHolder;
        private readonly ObservableList<Character> _enemies;
        private readonly KillCounter _killCounter;

        public Game(ICondition winCondition,
            ICondition looseCondition,
            CinemachineVirtualCamera camera,
            CharacterFabric characterFabric,
            CharacterPool characterPool,
            Transform playerStart,
            InputDetector inputDetector,
            EnemiesCooldownSpawner enemiesSpawner,
            CharacterHolder characterHolder,
            KillCounter killCounter,
            ObservableList<Character> enemies,
            ResultView resultView)
        {
            _winCondition = winCondition;
            _looseCondition = looseCondition;
            _camera = camera;
            _characterFabric = characterFabric;
            _characterPool = characterPool;
            _playerStart = playerStart;
            _enemiesSpawner = enemiesSpawner;
            _playerController = new PlayerController(inputDetector);
            _resultView = resultView;
            _characterHolder = characterHolder;
            _killCounter = killCounter;
            _enemies = enemies;

            _enemiesSpawner.EnemySpawned += OnEnemySpawned;
            _resultView.Completed += OnGameOver;
        }

        public void Start()
        {
            Character playerCharacter = _characterFabric.SpawnCharacter(CharacterTypes.Player, _playerStart.position);
            _playerController.SetCharacter(playerCharacter);
            _characterHolder.SetCharacter(playerCharacter);
            _camera.Follow = playerCharacter.transform;
            _playerController.Enable();

            if (playerCharacter is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill += OnKill;

            _winCondition.Completed += OnWin;
            _looseCondition.Completed += OnLoose;
            _winCondition.Start();
            _looseCondition.Start();

            _enemiesSpawner.Start();
        }

        private void Stop()
        {
            _enemiesSpawner.Stop();
            _playerController.Disable();

            if (_characterHolder.GetCharacter() is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill -= OnKill;

            _winCondition.Completed -= OnWin;
            _looseCondition.Completed -= OnLoose;
            _winCondition.Reset();
            _looseCondition.Reset();
            _characterHolder.ClearCharacter();
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