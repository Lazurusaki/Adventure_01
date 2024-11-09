using Cinemachine;
using UnityEngine;

namespace ADV_13
{
    public class Game
    {
        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;
        private readonly CharacterFabric _characterFabric;
        private readonly CharacterPool _characterPool;
        private readonly Transform _playerStart;
        private readonly EnemiesCooldownSpawner _enemiesSpawner;
        private readonly ResultView _resultView;
        private readonly CharacterHolder _playerCharacterHolder;
        private readonly ObservableList<Character> _enemiesHolder;
        private readonly KillCounter _killCounter;

        public Game(ICondition winCondition,
            ICondition looseCondition,
            CharacterFabric characterFabric,
            CharacterPool characterPool,
            Transform playerStart,
            EnemiesCooldownSpawner enemiesSpawner,
            CharacterHolder playerCharacterHolder,
            KillCounter killCounter,
            ObservableList<Character> enemiesHolder,
            ResultView resultView)
        {
            _winCondition = winCondition;
            _looseCondition = looseCondition;
            _characterFabric = characterFabric;
            _characterPool = characterPool;
            _playerStart = playerStart;
            _enemiesSpawner = enemiesSpawner;
            _resultView = resultView;
            _playerCharacterHolder = playerCharacterHolder;
            _killCounter = killCounter;
            _enemiesHolder = enemiesHolder;

            _enemiesSpawner.EnemySpawned += OnEnemySpawned;
            _resultView.Completed += OnGameOver;
        }

        public void Start()
        {
            Character playerCharacter = _characterFabric.SpawnCharacter(CharacterTypes.Player, _playerStart.position);
            _playerCharacterHolder.SetCharacter(playerCharacter);

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
            _playerCharacterHolder.ClearCharacter();

            if (_playerCharacterHolder.GetCharacter() is ShooterCharacter shooterCharacter)
                shooterCharacter.Kill -= OnKill;

            _winCondition.Completed -= OnWin;
            _looseCondition.Completed -= OnLoose;
            _winCondition.Reset();
            _looseCondition.Reset(); 
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
            _enemiesHolder.Add(character);
        }

        private void OnCharacterDeath(Character character)
        {
            character.DeathCompleted -= OnCharacterDeath;
            _characterPool.Clear(character.transform);
            _enemiesHolder.Remove(character);
        }

        private void OnGameOver()
        {
            _characterPool.ClearAll();
            _enemiesHolder.Clear();
            _killCounter.Reset();
            Start();
        }
    }
}