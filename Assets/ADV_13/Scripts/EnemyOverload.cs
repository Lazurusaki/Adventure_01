using System;

namespace ADV_13
{
    public class EnemyOverload : ICondition
    {
        private const int EnemiesCount = 5;
        private readonly ObservableList<Character> _enemies;

        public event Action Completed;

        public EnemyOverload (ObservableList<Character> enemies)
        {
            _enemies = enemies;
        }

        public void Start()
        {
            if (_enemies is null)
                throw new NullReferenceException("enemies is null");
            
            _enemies.Changed += count => { if (count >= EnemiesCount) Completed?.Invoke(); };
        }

        public void Reset()
        {
            if (_enemies is null)
                throw new NullReferenceException("enemies is null");
            
            _enemies.Changed -= count => Completed?.Invoke(); 
        }
    }
}