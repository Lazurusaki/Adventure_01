using UnityEngine;

namespace ADV_09
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private IdleBehaviors _idleBehavior;
        [SerializeField] private ReactionBehaviors _reactionBehavior;

        public IdleBehaviors IdleBehavior => _idleBehavior;
        public ReactionBehaviors ReactionBehavior => _reactionBehavior;
    }
}
