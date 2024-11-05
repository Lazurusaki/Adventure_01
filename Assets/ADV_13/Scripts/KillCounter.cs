using System;

namespace ADV_13
{
    public class KillCounter
    {
        private int _kills;
        private Character _character;

        public event Action<int> Changed;

        public void AddKill()
        {
            _kills++;
            Changed?.Invoke(_kills);
        }

        public void Reset()
        {
            _kills = 0;
        }
    }
}