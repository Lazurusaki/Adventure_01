using UnityEngine;

namespace ADV_11
{
    [RequireComponent(typeof(SphereCollider))]
    public class Bomb : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _timer;
        [SerializeField] private BombView _bombView;
        [SerializeField] private float _damage;
        [SerializeField] private float _activateRadius;
        [SerializeField] private float _explosionRadius;

        private SphereCollider _detectCollider;
        private bool _isTimerStarted;

        private void OnValidate()
        {
            _timer = Mathf.Max(0, _timer);
            _damage = Mathf.Max(0, _damage);
            _explosionRadius = Mathf.Max(0, _explosionRadius);
        }

        private void Awake()
        {
            if (_bombView == null)
                throw new System.NullReferenceException("Bomb view is not set");

            _detectCollider = GetComponent<SphereCollider>();
            _detectCollider.radius = _activateRadius;
        }

        private void Update()
        {
            if (_isTimerStarted)
            {
                if (_timer <= 0)
                    Detonate();
                else
                    _timer -= Time.deltaTime;
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

            if (_bombView != null)
                _bombView.Activate();
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

            if (_bombView != null)
                _bombView.Detonate();

            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            Activate();
        }
    }
}
