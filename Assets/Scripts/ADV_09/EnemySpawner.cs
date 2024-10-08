using System;
using UnityEngine;

namespace ADV_09
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _spawnPointsContainer;
        [SerializeField] private Transform _patrolPointsContainer;
        [SerializeField] private ParticleSystem _destroyEffectPrefab;

        public void SpawnEnemies()
        {
            if (_enemyPrefab == null)
                throw new NullReferenceException("Enemy prefab is not set");

            if (_playerTransform == null)
                throw new NullReferenceException("Player transform is not set");

            if (_spawnPointsContainer == null)
                throw new NullReferenceException("Spawn points container is not set");

            if (_patrolPointsContainer == null)
                throw new NullReferenceException("Patrol points container is not set");

            if (_destroyEffectPrefab == null)
                throw new NullReferenceException("Destroy effect prefab is not set");

            foreach (Transform child in _spawnPointsContainer)
            {
                if (child.TryGetComponent(out EnemySpawnPoint spawnPoint))
                {
                    IBehavior idleBehavior;
                    IBehavior reactionBehavior;

                    Enemy enemy = Instantiate(_enemyPrefab, child.position, Quaternion.identity, null);
                    TargetTracker targetTracker = new TargetTracker(enemy.transform, _playerTransform);

                    switch (spawnPoint.IdleBehavior)
                    {
                        case IdleBehaviors.Idle:
                            idleBehavior = new Idler();
                            break;
                        case IdleBehaviors.Patrol:
                            idleBehavior = new Patrol(enemy.transform, enemy.GetComponent<Mover>(), _patrolPointsContainer);
                            break;
                        case IdleBehaviors.Wander:
                            idleBehavior = new Wanderer(enemy.transform, enemy.GetComponent<Mover>());
                            break;
                        default:
                            idleBehavior = null;
                            break;
                    }

                    switch (spawnPoint.ReactionBehavior)
                    {
                        case ReactionBehaviors.RunAway:
                            reactionBehavior = new Coward(enemy.transform, enemy.GetComponent<Mover>(), targetTracker);
                            break;
                        case ReactionBehaviors.Agress:
                            reactionBehavior = new Agressor(enemy.transform, enemy.GetComponent<Mover>(), targetTracker);
                            break;
                        case ReactionBehaviors.Suicide:
                            reactionBehavior = new Suicide(enemy.transform, _destroyEffectPrefab);
                            break;
                        default:
                            reactionBehavior = null;
                            break;
                    }

                    enemy.Initialize(targetTracker, idleBehavior, reactionBehavior);
                }
            }
        }
    }
}