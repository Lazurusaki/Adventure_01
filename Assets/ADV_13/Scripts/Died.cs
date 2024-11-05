using System;

namespace  ADV_13
{
    public class Died : ICondition
    {
        public event Action Completed;

        private readonly Character _character;

        public Died(Character character)
        {
            _character = character;
            _character.Died += OnDied;
        }
        
        private void OnDied()
        {
            Completed?.Invoke();
        }
    }
  
}

