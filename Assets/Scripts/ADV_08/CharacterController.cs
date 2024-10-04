using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent (typeof(LookAtRotator))]

    public class CharacterController : MonoBehaviour
    {
        public bool IsMoving { get; private set; }

        private Mover _mover;
        private LookAtRotator _lookAtRotator;
        private CharacterAnimationController _characterAnimationController;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _lookAtRotator = GetComponent<LookAtRotator>();

            if (TryGetComponent(out _characterAnimationController) == false)
                Debug.LogWarning("CharacterAnimationController not found");
        }

        public void MoveStart(Vector3 moveDirection)
        {
            _lookAtRotator.LookAtDirection(moveDirection);
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