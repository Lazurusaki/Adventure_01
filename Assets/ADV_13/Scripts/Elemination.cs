using System;
using Unity.VisualScripting;

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
        }

        public void Start()
        {
            if (_killCounter is null)
                throw new NullReferenceException("KillCounter is null");
            
            _killCounter.Changed += kills => { if (kills >= Kills) Completed?.Invoke(); };
        }

        public void Reset()
        {
            if (_killCounter is null)
                throw new NullReferenceException("KillCounter is null");
            
            _killCounter.Changed -= (kills) => Completed?.Invoke();
        }
    }
}