using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Health))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private float _injuredPercent;
        [SerializeField] private float _inactivityTime;
        [SerializeField] private float _patrolRadius;

        private NavMeshAgent _agent;
        private Health _health;

        private bool _isTargetReached;
        private bool _isInjured;
        private bool _isDead;
        private float _inactivityTimer;

        private void Awake()
        {
            if (_characterView == null)
                throw new System.NullReferenceException("CharacterView is not Set");

            _agent = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
            _isTargetReached = true;
            ResetInactivityTimer();
        }

        private void OnValidate()
        {
            if (_injuredPercent <= 0)
                _injuredPercent = 0;

            if (_inactivityTime <= 0)
                _inactivityTime = 0;
        }

        private void Update()
        {
            if (_agent.pathPending == false && _isTargetReached == false && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                _characterView.StopRunning();
                _isTargetReached = true;
                ResetInactivityTimer();
            }

            if (_health.CurrentHealth <= _health.MaxHealth * _injuredPercent / 100 && _isInjured == false)
                Injure();

            if (_health.CurrentHealth <= 0 && _isDead == false)
                Die();

            if (_isTargetReached)
            {
                _inactivityTimer -= Time.deltaTime;

                if (_inactivityTimer <= 0)
                    if (TryFindPosition(out var position))
                        MoveTo(position);
            }
        }

        private void Die()
        {
            _isDead = true;
            _agent.ResetPath();

            if (_characterView != null)
                _characterView.Die();
        }

        private void Injure()
        {
            _isInjured = true;

            if (_characterView != null)
                _characterView.Injure();
        }

        private void ResetInactivityTimer()
        {
            _inactivityTimer = _inactivityTime;
        }

        public void MoveTo(Vector3 destination)
        {
            if (_isDead) return;

            if (_agent.SetDestination(destination))
            {
                _isTargetReached = false;
                _characterView.StartRunning();
            }
        }

        private bool TryFindPosition(out Vector3 position)
        {
            float tryCount = 10;
            position = Vector3.zero;

            for (int i = 0; i <= tryCount; i++)
            {
                Vector3 randomPoint = Random.insideUnitSphere * _patrolRadius + transform.position;

                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
                {
                    position = hit.position;
                    return true;
                }
            }

            return false;
        }
    }
}
