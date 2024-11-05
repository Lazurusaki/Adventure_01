using System;

namespace ADV_13
{
    public class EnemyOverload : ICondition
    {
        private const int EnemiesCount = 5;

        private readonly ObservableList<Character> _enemies;

        public event Action Completed;

        public EnemyOverload(ObservableList<Character> enemies)
        {
            _enemies = enemies;
            _enemies.Changed += OnEnemiesChanged;
        }

        private void OnEnemiesChanged(int count)
        {
            if (count >= EnemiesCount)
                Completed?.Invoke();
        }
    }
}