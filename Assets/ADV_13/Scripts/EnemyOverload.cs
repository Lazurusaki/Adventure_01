using System;

namespace ADV_13
{
    public class EnemyOverload : ICondition
    {
        private const int EnemiesCount = 5;

        public event Action Completed;

        public void Initialize (ObservableList<Character> enemies)
        {
            enemies.Changed += count => { if (count >= EnemiesCount) Completed?.Invoke(); };
        }
    }
}