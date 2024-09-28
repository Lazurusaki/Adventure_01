using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Animator))]

    public class CharacterAnimationController : MonoBehaviour
    {
        private const string IsMovingName = "IsMoving";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoving(bool isMoving)
        {
            _animator.SetBool(IsMovingName, isMoving);
        }
    }
}
