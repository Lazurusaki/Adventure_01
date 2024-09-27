using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationController : MonoBehaviour
    {
        private const string AnimatorIsMovingName = "IsMoving";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoving(bool isMooving)
        {
            _animator.SetBool(AnimatorIsMovingName, isMooving);
        }
    }
}
