using System;

namespace ADV_13
{
    public interface ICondition
    {
        public event Action Completed;

        public void Start();
        public void Reset();
    }
}
