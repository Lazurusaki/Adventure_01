using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterController : MonoBehaviour, IDamagable
    {
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private Transform _healthBarSocket;

        private NavMeshAgent _agent;
        private Health _health;
        private bool _isDead;
        private bool _isInitialized;

        public bool IsTargetReached { get; private set; }
        public Transform HealthBarSocket => _healthBarSocket;

        public void Initialize(Health health)
        {
            if (_characterView == null)
                throw new System.NullReferenceException("CharacterView is not Set");

            if (_healthBarSocket == null)
                throw new System.NullReferenceException("Healthbar Socket is not Set");

            _health = health;
            _characterView.Initialize(_health);
            _agent = GetComponent<NavMeshAgent>();
            IsTargetReached = true;
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                if (_agent.pathPending == false && IsTargetReached == false && _agent.remainingDistance <= _agent.stoppingDistance)
                {
                    _characterView.StopRunning();
                    IsTargetReached = true;
                }

                if (_health.CurrentHealth <= 0 && _isDead == false)
                    Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _agent.ResetPath();

            if (_characterView != null)
                _characterView.Die();
        }

        public void MoveTo(Vector3 destination)
        {
            if (_isDead) return;

            if (_agent.SetDestination(destination))
            {
                IsTargetReached = false;
                _characterView.StartRunning();
            }
        }

        public void TakeDamage(float amount)
        {
            _health.TakeDamage(amount);
        }
    }
}
