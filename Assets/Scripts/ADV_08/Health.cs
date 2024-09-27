using UnityEngine;

namespace ADV_08
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;

        public float CurrentHealth { get; private set; }

        private void Start()
        {
            CurrentHealth = 50;
        }

        private void OnValidate()
        {
            if (_maxHealth < 0)
                _maxHealth = 0;
        }

        public void Heal(float amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _maxHealth);
        }
    }
}
