using System;

namespace ADV_13
{
    public class Elemination : ICondition
    {
        private const int Kills = 5;

        private readonly KillCounter _killCounter;

        public event Action Completed;
        
        public Elemination(KillCounter killCounter)
        {
            _killCounter = killCounter;
            _killCounter.Changed += OnKillsChanged;
        }

        private void OnKillsChanged(int kills)
        {
            if (kills >= Kills)
                Completed?.Invoke();
        }
    }
}