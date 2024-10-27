using System.Collections;
using UnityEngine;

namespace ADV_11
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private float _injuredPercent;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _damageFlickerFrequency;
        [SerializeField] private float _damageFlickerDuration;
        [SerializeField] private Color _damageFlickerColor;
        [SerializeField] private float _deathFadeDuration;
        
        private Health _health;

        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");
        private readonly int InjuredLayerKey = 1;

        private Animator _animator;
        private EmmisiveFlicker _flashFlicker;
        private Fader _deathFader;
        private Coroutine _takeDamageCoroutine;
        private bool _isInjured;
        private float _flickerTimer;
        public  bool IsDeathComplete;
        
        private void OnValidate()
        {
            _injuredPercent = Mathf.Clamp(_injuredPercent, 0, 100);
            _damageFlickerFrequency = Mathf.Max(_damageFlickerFrequency, 0);
            _deathFadeDuration = Mathf.Max(_deathFadeDuration, 0);
        }

        public void Initialize(Health health)
        {
            _animator = GetComponent<Animator>();
            _health = health;

            if (_renderer == null)
                throw new System.NullReferenceException("Renderer is not set");

            _flashFlicker = new EmmisiveFlicker(this,_renderer, _damageFlickerFrequency, _damageFlickerColor);
            _deathFader = new Fader(this, _renderer, _deathFadeDuration);
        }

        private void Update()
        {
            if (_health != null && _isInjured == false)
                TryInjure();

            if (_deathFader.IsCompleted)
                IsDeathComplete = true;
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

        private IEnumerator TakeDamageFlicker()
        {
            _flickerTimer = 0;

            while (_flickerTimer < _damageFlickerDuration)
            {
                _flickerTimer += Time.deltaTime;
                yield return null;
            }

            _flashFlicker.Deactivate();
        }

        public void Die()
        {
            _flashFlicker.Deactivate();     
            _animator.SetBool(IsDeadKey, true);
        }

        public void StartJumping()
        {
            _animator.SetBool(IsJumpingKey, true);
        }

        public void StopJumping()
        {
            _animator.SetBool(IsJumpingKey, false);
        }

        public void StartRunning()
        {
            _animator.SetBool(IsRunningKey, true);
        }

        public void StopRunning()
        {
            _animator.SetBool(IsRunningKey, false);
        }

        public void TakeDamage()
        {
            _flashFlicker.Activate();

            if (_takeDamageCoroutine != null)
                StopCoroutine(_takeDamageCoroutine);

            _takeDamageCoroutine = StartCoroutine(TakeDamageFlicker());
        }

        public void OnDeathAnimationEnd()
        {
            _deathFader.Activate();

        }
    }
}
