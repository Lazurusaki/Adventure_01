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
                    Enemy enemy = Instantiate(_enemyPrefab, child.position, Quaternion.identity, null);
                    enemy.Initialize(_playerTransform, spawnPoint.IdleBehavior, spawnPoint.ReactionBehavior);

                    switch (spawnPoint.IdleBehavior)
                    {
                        case EnemyIdleStateBehaviors.Idle:
                            enemy.SetIdleStateBehaviorHandler(new Idler());
                            break;
                        case EnemyIdleStateBehaviors.Patrol:
                            enemy.SetIdleStateBehaviorHandler(new Patrol(enemy.transform, enemy.Mover, _patrolPointsContainer));
                            break;
                        case EnemyIdleStateBehaviors.Wander:
                            enemy.SetIdleStateBehaviorHandler(new Wanderer(enemy.transform, enemy.Mover));
                            break;
                    }

                    switch (spawnPoint.ReactionBehavior)
                    {
                        case EnemyReactionStateBehaviors.RunAway:
                            enemy.SetReactionStateBehaviorHandler(new Coward(enemy.transform, enemy.Mover, _playerTransform));
                            break;
                        case EnemyReactionStateBehaviors.Agress:
                            enemy.SetReactionStateBehaviorHandler(new Agressor(enemy.transform, enemy.Mover, _playerTransform));
                            break;
                        case EnemyReactionStateBehaviors.Suicide:
                            enemy.SetReactionStateBehaviorHandler(new Suicide(enemy.transform, _destroyEffectPrefab));
                            break;
                    }

                    enemy.IsInitialized = true;
                }
            }
        }
    }
}