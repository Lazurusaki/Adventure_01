using UnityEngine;
using UnityEngine.UI;

namespace ADV_11
{
    public class HealthbarDisplayer: MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarPrefab;

        private Transform _socket;
        private Health _health;
        private HealthBar _healthBar;

        private bool _isInitialized;

        public void Initialize(Health health, Transform socket)
        {
            if (_healthBarPrefab == null)
                throw new System.NullReferenceException("Healthbar prefav is not set");

            _socket = socket;
            _health = health;
            _healthBar = Instantiate(_healthBarPrefab, Vector3.zero, Quaternion.identity, null);
            _healthBar.Slider.maxValue = _health.MaxHealth;
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _healthBar.Slider.value = _health.CurrentHealth;
                _healthBar.transform.position = _socket.position;
                _healthBar.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}

