using UnityEngine;

namespace ADV_11
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _maxHealth;

        public float CurrentHealth { get; private set; }
        public float MaxHealth { get { return _maxHealth; } }

        private void Start()
        {
            CurrentHealth = _maxHealth;
        }

        private void OnValidate()
        {
            if (_maxHealth < 0)
                _maxHealth = 0;
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
