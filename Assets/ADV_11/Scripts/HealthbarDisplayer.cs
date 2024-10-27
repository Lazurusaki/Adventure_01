using UnityEngine;

namespace ADV_11
{
    public class HealthbarDisplayer
    {
        private Transform _socket;
        private Health _health;
        private HealthBar _healthBar;

        public HealthbarDisplayer(HealthBar healthbar, Health health, Transform socket)
        {   
            _healthBar = healthbar;
            _socket = socket;
            _health = health;
            _healthBar.Slider.maxValue = _health.MaxHealth;
        }

        public void Update()
        {
            if (_healthBar != null & _socket != null)
            {
                _healthBar.Slider.value = _health.CurrentHealth;
                _healthBar.transform.position = _socket.position;
                _healthBar.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}

