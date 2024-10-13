using UnityEngine;

namespace ADV_09
{
    [RequireComponent(typeof(Mover))]
    public class Enemy : MonoBehaviour
    {
        private IBehavior _idleBehavior;
        private IBehavior _reactionBehavior;

        private IBehavior _currentBehavior;
        private TargetTracker _distanceTracker;
        private Transform _player;
        private Mover _mover;

        private bool _isInitialized;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        public void Initialize(TargetTracker targetTracker, IBehavior idleBehavior, IBehavior reactionBehavior)
        {
            _idleBehavior = idleBehavior;
            _reactionBehavior = reactionBehavior;
            _currentBehavior = _idleBehavior;
            _distanceTracker = targetTracker;
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _distanceTracker.Update();

                if (_distanceTracker.IsReact)
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
