using UnityEngine;

namespace ADV_08
{
    public class ItemMedKit : Item
    {
        [SerializeField] private float _healAmount;

        private void OnValidate()
        {
            if (_healAmount < 0)
                _healAmount = 0;
        }

        public override void Activate(Transform owner)
        {
            if (owner.TryGetComponent(out Health _health) == false)
            {
                Debug.LogError("Health component not found");
                return;
            }

            base.Activate(owner);
            _health.Heal(_healAmount);
            Debug.Log($"Health increased +{_healAmount} HP");
        }
    }
}
