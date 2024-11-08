using System;
using UnityEngine;

namespace  ADV_13
{
    public class Died : ICondition
    {
        public event Action Completed;

        private Character _character;
        
        public Died(CharacterHolder characterHolder)
        {
            characterHolder.CharacterSet += OnCharacterSet;
            characterHolder.CharacterClear += OnCharacterClear;
        }

        private void OnCharacterSet(Character character)
        {
            _character = character;
        }

        private void OnCharacterClear()
        {
            Reset();
        }

        public void Start()
        {
            if (_character is null)
                throw new NullReferenceException("Character is null");
            
            _character.Died  += () => Completed?.Invoke();
        }

        public void Reset()
        {
            if (_character is not null)
                _character.Died  -= () => Completed?.Invoke(); 
        }
    }
}

