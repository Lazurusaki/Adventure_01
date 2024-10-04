using UnityEngine;

namespace ADV_09
{
    public class Enemy : MonoBehaviour
    {
        private IEnemyStateHandler _idleStateBehaviorHandler;
        private IEnemyStateHandler _reactionStateBehaviorHandler;

        private PlayerTracker _playerTracker;
        private Mover _mover;
        private Transform _playerTransform;

        private bool _isInitialized;

        public void Initialize(Transform playerTransform,
                               EnemyIdleStateBehaviors idleStateBehavior,
                               EnemyReactionStateBehaviors reactionStateBehavior,
                               Transform patrolPointsContainer,
                               ParticleSystem destroyEffect)
        {
            _playerTransform = playerTransform;
            _mover = new Mover();
            _playerTracker = new PlayerTracker(transform, _playerTransform);

            switch (idleStateBehavior)
            {
                case EnemyIdleStateBehaviors.Idle:
                    _idleStateBehaviorHandler = new Idler();
                    break;
                case EnemyIdleStateBehaviors.Patrol:
                    _idleStateBehaviorHandler = new Patrol(transform, _mover, patrolPointsContainer);
                    break;
                case EnemyIdleStateBehaviors.Wander:
                    _idleStateBehaviorHandler = new Wanderer(transform, _mover);
                    break;
            }

            switch (reactionStateBehavior)
            {
                case EnemyReactionStateBehaviors.RunAway:
                    _reactionStateBehaviorHandler = new Coward(transform, _mover, playerTransform);
                    break;
                case EnemyReactionStateBehaviors.Agress:
                    _reactionStateBehaviorHandler = new Agressor(transform, _mover, playerTransform);
                    break;
                case EnemyReactionStateBehaviors.Suicide:
                    _reactionStateBehaviorHandler = new Suicide(transform, destroyEffect);
                    break;
            }

            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _playerTracker.Update();

                if (_playerTracker.IsInAgression)
                    _reactionStateBehaviorHandler.UpdateState();
                else
                    _idleStateBehaviorHandler.UpdateState();
            }
        }
    }
}
