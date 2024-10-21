using UnityEngine;

namespace ADV_11
{
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour
    {        
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");
        private readonly int InjuredLayerKey = 1;

        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Die()
        {
            _animator.SetBool(IsDeadKey, true);
        }

        public void Injure()
        {
            _animator.SetLayerWeight(InjuredLayerKey, 1f);
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
