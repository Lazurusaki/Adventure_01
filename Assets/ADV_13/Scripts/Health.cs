using System;
using UnityEngine;

namespace ADV_13
{
    public class Health
    {
        private float _health;
        
        public event Action Empty; 

        public Health(float maxHealth)
        {
            _health = maxHealth;
        }
        
        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            
            if (_health == 0)
                Empty?.Invoke();
        }
    }
}