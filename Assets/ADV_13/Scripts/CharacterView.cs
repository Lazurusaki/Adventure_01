using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace ADV_13
{
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour
    {
        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
        private readonly int _isDeadKey = Animator.StringToHash("IsDead");

        private Animator _animator;
        private Coroutine _takeDamageCoroutine;

        public event Action DeathCompleted;

        public void Initialize(Character character)
        {
            _animator = GetComponent<Animator>();
            character.VelocityChanged += OnVelocityChanged;
            character.Died += OnDied;
        }

        private void OnDied()
        {
            _animator.SetBool(_isDeadKey, true);
        }

        private void OnDeathCompleted()
        {
            DeathCompleted?.Invoke();
        }

        private void OnVelocityChanged(float velocity)
        {
            bool isRunning = _animator.GetBool(_isRunningKey);

            switch (isRunning)
            {
                case false when velocity > 0:
                    _animator.SetBool(_isRunningKey, true);
                    break;
                case true when velocity == 0:
                    _animator.SetBool(_isRunningKey, false);
                    break;
            }
        }
    }
}