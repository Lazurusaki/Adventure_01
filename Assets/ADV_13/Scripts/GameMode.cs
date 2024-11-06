using System;
using Unity.VisualScripting;

namespace ADV_13
{
    public class GameMode
    {
        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;

        private Character _playerCharacter;
        private KillCounter _killCounter;
        private ObservableList<Character> _enemies;

        public event Action Win;
        public event Action Loose;
        
        public GameMode(EndGameConditions winConditionName, EndGameConditions looseConditionName)
        {
            _winCondition = GetCondition(winConditionName);
            _looseCondition = GetCondition(looseConditionName);

            _winCondition.Completed += () => Win?.Invoke();
            _looseCondition.Completed  += () => Loose?.Invoke();
        }

        public void Initialize(Character playerCharacter, KillCounter killCounter, ObservableList<Character> enemies)
        {
            _playerCharacter = playerCharacter;
            _killCounter = killCounter;
            _enemies = enemies;
            
            InitializeCondition(_winCondition);
            InitializeCondition(_looseCondition);
        }
        
        private ICondition GetCondition(EndGameConditions conditionName)
        {
            ICondition condition = null;
            
            switch (conditionName)
            {
                case EndGameConditions.TimeSurvival:
                    condition = new TimeSurvival();
                    break;
                case EndGameConditions.Elemination:
                    condition = new Elemination();
                    break;
                case EndGameConditions.Died:
                    condition = new Died();
                    break;
                case EndGameConditions.EnemyOverload:
                    condition = new EnemyOverload();
                    break;
            }

            return condition;
        }
        
        private void InitializeCondition(ICondition condition)
        {
            if (_playerCharacter is null) throw new NullReferenceException("PlayerCharacter is empty");
            if (_killCounter is null) throw new NullReferenceException("KillCounter is empty");
            if (_enemies is null) throw new NullReferenceException("Enemies is empty");
            
            switch (condition)
            {
                case TimeSurvival timeSurvival:
                    timeSurvival.Initialize(_playerCharacter);
                    break;
                case Elemination elemination:
                    elemination.Initialize(_killCounter);
                    break;
                case Died died:
                    died.Initialize(_playerCharacter);
                    break;
                case EnemyOverload enemyOverload:
                    enemyOverload.Initialize(_enemies);
                    break;
            }
        }
    }
}