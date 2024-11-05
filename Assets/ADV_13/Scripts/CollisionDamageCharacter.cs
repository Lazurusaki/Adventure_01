using UnityEngine;

namespace ADV_13
{
    public class CollisionDamageCharacter : Character
    {
        [SerializeField] private float _damage;

        private void OnValidate()
        {
            _damage = Mathf.Max(_damage, 0);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (IsDead) 
                return;
            
            if (other.gameObject.TryGetComponent(out IDamageable damageable) && 
                other.gameObject.TryGetComponent<CollisionDamageCharacter>(out _) == false)
                damageable.TakeDamage(_damage);
        }
    }
}