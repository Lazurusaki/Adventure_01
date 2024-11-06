namespace ADV_13
{
    public class ConditionFabric
    {
        private readonly Character _character;
        private readonly ObservableList<Character> _enemies;
        private readonly KillCounter _killCounter;
        
        public ConditionFabric(Character character,
                               KillCounter killCounter,
                               ObservableList<Character> enemies)
        {
            _enemies = enemies;
            _killCounter = killCounter;
            _character = character;
        }
        
        public void InitializeCondition(ICondition condition)
        {
            switch (condition)
            {
                case TimeSurvival timeSurvival:
                    timeSurvival.Initialize(_character);
                    break;
                case Elemination elemination:
                    elemination.Initialize(_killCounter);
                    break;
                case Died died:
                    died.Initialize(_character);
                    break;
                case EnemyOverload enemyOverload:
                    enemyOverload.Initialize(_enemies);
                    break;
            }
        }
    }
}
