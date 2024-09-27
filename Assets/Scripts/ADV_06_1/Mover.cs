using UnityEngine;

namespace ADV_06_01
{
    public class Mover : MonoBehaviour
    {
        private const string AnimatorIsMovingName = "isMooving";

        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private Animator _animator;
        private CharacterInput _characterInput;
        private readonly float _inputDeadZone = 0.1f;

        private void Awake()
        {
            TryGetComponent(out _animator);
            TryGetComponent(out _characterInput);
        }

        private void Update()
        {
            if (_characterInput == null)
                return;

            if (_characterInput.AxisInput.magnitude <= _inputDeadZone)
            {
                if (_animator != null)
                    _animator.SetBool(AnimatorIsMovingName, false);

                return;
            }

            Move(_characterInput.AxisInput);
            LookToMoving(_characterInput.AxisInput);

            if (_animator != null)
                _animator.SetBool(AnimatorIsMovingName, true);
        }

        private void LookToMoving(Vector3 input)
        {
            Quaternion lookRotation = Quaternion.LookRotation(input);
            float step = _rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }

        public void Move(Vector3 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        }
    }
}
