using System;

namespace  ADV_13
{
    public class Died : ICondition
    {
        public event Action Completed;
        
        public void Initialize(Character character)
        {
            character.Died += () => Completed?.Invoke();
        }
    }
}

