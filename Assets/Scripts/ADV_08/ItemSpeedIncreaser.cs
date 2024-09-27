using UnityEngine;

namespace ADV_08
{
    public class ItemSpeedIncreaser : Item
    {
        [SerializeField] private float _increaseValue;

        private void OnValidate()
        {
            if (_increaseValue < 0)
                _increaseValue = 0;
        }

        public override void Activate(Transform owner)
        {
            if (owner.TryGetComponent(out Mover _mover) == false)
            {
                Debug.LogError("Mover component not found");
                return;
            }

            base.Activate(owner);
            _mover.AddSpeed(_increaseValue);
            Debug.Log($"Speed Increased by {_increaseValue}");
        }
    }
}
