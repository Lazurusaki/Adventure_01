using UnityEngine;

namespace ADV_11
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private float _injuredPercent;

        private Health _health;

        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");
        private readonly int InjuredLayerKey = 1;

        private Animator _animator;
        private bool _isInjured;

        private void OnValidate()
        {
            _injuredPercent = Mathf.Clamp(_injuredPercent, 0, 100);
        }

        public void Initialize(Health health)
        {
            _animator = GetComponent<Animator>();
            _health = health;
        }

        private void Update()
        {
            if (_health != null && _isInjured == false)
                TryInjure();
        }

        private bool TryInjure()
        {
            if (_health.CurrentHealth <= _health.MaxHealth * _injuredPercent / 100)
            {
                _animator.SetLayerWeight(InjuredLayerKey, 1f);
                return true;
            }

            return false;
        }

        public void Die()
        {
            _animator.SetBool(IsDeadKey, true);
        }

        public void StartRunning()
        {
            _animator.SetBool(IsRunningKey, true);
        }

        public void StopRunning()
        {
            _animator.SetBool(IsRunningKey, false);
        }
    }
}
