using UnityEngine;

namespace ADV_11
{
    [RequireComponent(typeof(SphereCollider))]
    public class Bomb : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _detonateTime;
        [SerializeField] private BombView _view;
        [SerializeField] private float _damage;
        [SerializeField] private float _activateRadius;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private AudioClip _explosionSound;

        private AudioSource _explosionSource;
        private SphereCollider _detectCollider;
        private bool _isTimerStarted;

        private void OnValidate()
        {
            _detonateTime = Mathf.Max(0, _detonateTime);
            _damage = Mathf.Max(0, _damage);
            _explosionRadius = Mathf.Max(0, _explosionRadius);
        }

        public void Initialize(AudioSource explosionSource)
        {
            if (_view == null)
                throw new System.NullReferenceException("Bomb view is not set");

            if (_explosionSound == null)
                throw new System.NullReferenceException("Explosion sound is not set");

            _detectCollider = GetComponent<SphereCollider>();
            _detectCollider.radius = _activateRadius;
            _explosionSource = explosionSource;
            _view.Initialize(_detonateTime);
        }

        private void Update()
        {
            if (_isTimerStarted)
            {
                if (_detonateTime <= 0)
                    Detonate();
                else
                    _detonateTime -= Time.deltaTime;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _activateRadius);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                Activate();
            }
        }

        private void Activate()
        {
            _isTimerStarted = true;

            if (_view != null)
                _view.Activate();
        }

        private float CalculateDamage(Vector3 targetPosition)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            float damage = _damage * (1 - (distance / _explosionRadius));
            return damage;
        }

        private void Detonate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            if (colliders.Length > 0)
                foreach (Collider collider in colliders)
                    if (collider.TryGetComponent(out IDamagable damagable))
                        damagable.TakeDamage(CalculateDamage(collider.transform.position));

            if (_view != null)
                _view.Detonate();

            if (_explosionSound != null && _explosionSource != null)
            {
                _explosionSource.transform.position = transform.position;
                _explosionSource.PlayOneShot(_explosionSound);
            }

            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            Activate();
        }
    }
}
