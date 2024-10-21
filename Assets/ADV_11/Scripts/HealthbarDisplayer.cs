using UnityEngine;
using UnityEngine.UI;

namespace ADV_11
{

    public class HealthbarDisplayer : MonoBehaviour
    {
        [SerializeField] private Transform _socket;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Health _health;

        private void Awake()
        {
            if (_socket == null)
                throw new System.NullReferenceException("Target object is not set");

            if (_healthBar == null)
                throw new System.NullReferenceException("Healthbar is not set");

            if (_health == null)
                throw new System.NullReferenceException("Healt is not set");

            _healthBar.maxValue = _health.MaxHealth;
        }

        private void Update()
        {
            if (_socket == null || _healthBar == null || _health == null)
                return;

            _healthBar.value = _health.CurrentHealth;
            _healthBar.transform.position = _socket.position;
            _healthBar.transform.rotation = Camera.main.transform.rotation;
        }
    }
}

