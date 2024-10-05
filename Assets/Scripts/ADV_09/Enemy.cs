using UnityEngine;

namespace ADV_09
{
    public class Enemy : MonoBehaviour
    {
        private IEnemyStateHandler _idleStateBehaviorHandler;
        private IEnemyStateHandler _reactionStateBehaviorHandler;

        private PlayerTracker _playerTracker;
        private Transform _playerTransform;
        private EnemyStates _currentState;

        public bool IsInitialized { get; set; }
        public Mover Mover { get; private set; }

        public void Initialize(Transform playerTransform,
                               EnemyIdleStateBehaviors idleStateBehavior,
                               EnemyReactionStateBehaviors reactionStateBehavior)
        {
            _playerTransform = playerTransform;
            Mover = new Mover();
            _playerTracker = new PlayerTracker(transform, _playerTransform);
        }

        private void Update()
        {
            if (IsInitialized)
            {
                _playerTracker.Update();

                if (_playerTracker.IsInReact)
                {
                    _reactionStateBehaviorHandler.UpdateState();
                    _currentState = EnemyStates.React;
                }
                else
                {
                    if (_currentState == EnemyStates.React)
                    {
                        _currentState = EnemyStates.Idle;
                        _idleStateBehaviorHandler.EnterState();
                    }

                    _idleStateBehaviorHandler.UpdateState();
                }
            }
        }

        public void SetIdleStateBehaviorHandler(IEnemyStateHandler idleStateBehaviorHandler)
        {
            _idleStateBehaviorHandler = idleStateBehaviorHandler;
        }

        public void SetReactionStateBehaviorHandler(IEnemyStateHandler reactionStateBehaviorHandler)
        {
            _reactionStateBehaviorHandler = reactionStateBehaviorHandler;
        }
    }
}
