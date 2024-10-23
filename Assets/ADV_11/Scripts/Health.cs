using UnityEngine;

namespace ADV_11
{
    public class Health
    {
        private float _maxHealth;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => _maxHealth;

        public Health(float MaxHealth)
        {
            _maxHealth = MaxHealth;
            CurrentHealth = _maxHealth;
        }

        private void OnValidate()
        {
            _maxHealth = Mathf.Max(0, _maxHealth);
        }

        public void Heal(float amount)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + amount, 0, _maxHealth);
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        }
    }
}
