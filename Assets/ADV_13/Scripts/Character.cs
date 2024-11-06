using System;
using UnityEngine;

namespace ADV_13
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxHealth;
        [SerializeField] private CharacterView _view;

        private Health _health;
        private Mover _mover;
        private LookRotator _rotator;
        private Rigidbody _rigidbody;
        private Collider _collider;

        public event Action Died;
        public event Action<Character> DeathCompleted;
        public event Action<float> VelocityChanged;

        public bool IsDead { get; private set; }

        private void OnValidate()
        {
            _rotationSpeed = Mathf.Max(_rotationSpeed, 0);
            _speed = Mathf.Max(_speed, 0);
            _maxHealth = Mathf.Max(_maxHealth, 0);
        }

        public virtual void Initialize()
        {
            if (_view is null)
                throw new NullReferenceException("View is not set");

            _rigidbody = GetComponent<Rigidbody>();
            
            _mover = new Mover(_rigidbody);
            _rotator = new LookRotator(_rigidbody, _rotationSpeed);
            _health = new Health(_maxHealth);
 
            _view.Initialize(this);
            
            _health.Empty += OnDied;
            _view.DeathCompleted += OnDeathCompleted;
        }

        private void OnDied()
        {
            Disable();

            IsDead = true;
            Died?.Invoke();
        }

        private void OnDeathCompleted()
        {
            DeathCompleted?.Invoke(this);
        }

        public void Move(Vector3 direction)
        {
            _mover.Move(direction * _speed);
            VelocityChanged?.Invoke(direction.magnitude);
        }

        public void StartRotation(Vector3 direction)
        {
            _rotator.StartRotation(direction);
        }

        public void StopRotation()
        {
            _rotator.StopRotation();
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
        }

        public void Disable()
        {
            Move(Vector3.zero);
            GetComponent<Collider>().isTrigger = true;
        }
    }
}