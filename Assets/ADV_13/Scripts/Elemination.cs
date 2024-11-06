using System;
using Unity.VisualScripting;

namespace ADV_13
{
    public class Elemination : ICondition
    {
        private const int Kills = 5;
        
        public event Action Completed;
        
        public void Initialize(KillCounter killCounter)
        {
            killCounter.Changed += kills => { if (kills >= Kills) Completed?.Invoke(); };
        }
    }
}