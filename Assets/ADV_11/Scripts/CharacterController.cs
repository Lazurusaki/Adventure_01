using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterController : MonoBehaviour, IDamagable
    {
        [SerializeField] private CharacterView _view;
        [SerializeField] private Transform _healthBarSocket;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _jumpDuration;

        private NavMeshAgent _agent;
        private Health _health;
        private bool _isDead;
        private bool _isInitialized;
        private Coroutine _jumpCoroutine;

        public  bool IsDeathComplete { get; private set; }
        public bool IsTargetReached { get; private set; }
        public Transform HealthBarSocket => _healthBarSocket;

        public void Initialize(Health health)
        {
            if (_view == null)
                throw new System.NullReferenceException("CharacterView is not Set");

            if (_healthBarSocket == null)
                throw new System.NullReferenceException("Healthbar Socket is not Set");

            _health = health;
            _view.Initialize(_health);
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
                    _view.StopRunning();
                    IsTargetReached = true;
                }

                if (_agent.isOnOffMeshLink)
                {
                    if (_jumpCoroutine == null)
                        _jumpCoroutine = StartCoroutine(Jump());
                }

                if (_health.CurrentHealth <= 0 && _isDead == false)
                    Die();

                if (_isDead == true && _view.IsDeathComplete)
                    IsDeathComplete = true;
            }
        }

        private void Die()
        {
            _isDead = true;
            _agent.ResetPath();

            if (_view != null)
                _view.Die();
        }

        private IEnumerator Jump()
        {
            _view.StartJumping();
            OffMeshLinkData data = _agent.currentOffMeshLinkData;
            Vector3 startPos = _agent.transform.position;
            Vector3 endPos = data.endPos + Vector3.up * _agent.baseOffset;

            float progress = 0;

            while (progress < _jumpDuration)
            {
                float yOffset = _jumpCurve.Evaluate(progress / _jumpDuration);
                _agent.transform.position = Vector3.Lerp(startPos, endPos, progress / _jumpDuration) + yOffset * Vector3.up;
                transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(endPos - startPos).eulerAngles.y, 0);
                progress += Time.deltaTime;
                yield return null;
            }

            _agent.CompleteOffMeshLink();
            _view.StopJumping();
            _jumpCoroutine = null;
        }

        public void MoveTo(Vector3 destination)
        {
            if (_isDead) return;

            if (_agent.SetDestination(destination))
            {
                IsTargetReached = false;
                _view.StartRunning();
            }
        }

        public void TakeDamage(float amount)
        {
            _health.TakeDamage(amount);
            _view.TakeDamage();
        }
    }
}
