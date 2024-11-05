using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage;
        
        private readonly float  _lifeTime = 1f;
        
        private float _timer;
        private Coroutine _lifeTimeCoroutine;

        public event Action<Collision> Hit;

        private void OnValidate()
        {
            _damage = Mathf.Max(_damage, 0);
        }
        
        public void Initialize()
        {
            if (_lifeTimeCoroutine is not null)
                StopCoroutine(_lifeTimeCoroutine);
            
            _lifeTimeCoroutine = StartCoroutine(LifeTime());
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                Hit?.Invoke(other);
            }

            Destroy(gameObject);
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}
