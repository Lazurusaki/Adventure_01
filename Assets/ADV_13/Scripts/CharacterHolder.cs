using System;

namespace ADV_13
{
    public class CharacterHolder
    {
        private Character _character;

        public event Action<Character> CharacterSet;
        public event Action CharacterClear;

        public Character GetCharacter() => _character;

        public void SetCharacter(Character character)
        {
            _character = character;
            CharacterSet?.Invoke(_character);
        }

        public void ClearCharacter()
        {
            CharacterClear?.Invoke();
            _character = null;
        }
    }
}
