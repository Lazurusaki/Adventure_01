using UnityEngine;

namespace ADV_09
{
    [RequireComponent(typeof(Mover))]
    public class Enemy : MonoBehaviour
    {
        private IBehavior _idleBehavior;
        private IBehavior _reactionBehavior;

        private IBehavior _currentBehavior;
        private PlayerTracker _playerTracker;
        private Transform _playerTransform;
        private Mover _mover;

        private bool _isInitialized;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        public void Initialize(Transform playerTransform, IBehavior idleBehavior, IBehavior reactionBehavior)
        {
            _idleBehavior = idleBehavior;
            _reactionBehavior = reactionBehavior;
            _currentBehavior = _idleBehavior;
            _playerTransform = playerTransform;
            _playerTracker = new PlayerTracker(transform, _playerTransform);
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _playerTracker.Update();

                if (_playerTracker.IsReact)
                {
                    ChangeBehaviorTo(_reactionBehavior);
                }
                else
                {
                    ChangeBehaviorTo(_idleBehavior);
                }

                _currentBehavior.Update();
            }
        }

        private void ChangeBehaviorTo(IBehavior behavior)
        {
            if (_currentBehavior != behavior)
            {
                _currentBehavior = behavior;
                _currentBehavior.Enter();
            }
        }
    }
}
