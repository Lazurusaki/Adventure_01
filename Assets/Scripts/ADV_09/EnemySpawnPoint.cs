using UnityEngine;

namespace ADV_09
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyIdleStateBehaviors _idleBehavior;
        [SerializeField] private EnemyReactionStateBehaviors _reactionBehavior;

        public EnemyIdleStateBehaviors IdleBehavior => _idleBehavior;
        public EnemyReactionStateBehaviors ReactionBehavior => _reactionBehavior;
    }
}
