using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Mover))]

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        public bool IsMoving { get; private set; }

        private Mover _mover;
        private CharacterAnimationController _characterAnimationController;

        private void Awake()
        {
            _mover = GetComponent<Mover>();

            if (TryGetComponent(out _characterAnimationController) == false)
                Debug.LogWarning("CharacterAnimationController not found");
        }

        private void LookToMoving(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float step = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }

        public void MoveStart(Vector3 moveDirection)
        {
            LookToMoving(moveDirection);
            _mover.Move(moveDirection);
            IsMoving = true;

            if (_characterAnimationController != null)
                _characterAnimationController.SetMoving(true);
        }

        public void MoveEnd()
        {
            IsMoving = false;

            if (_characterAnimationController != null)
                _characterAnimationController.SetMoving(false);
        }
    }
}