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
                    enemy.Initialize(_playerTransform, spawnPoint.IdleBehavior, spawnPoint.ReactionBehavior, _patrolPointsContainer, _destroyEffectPrefab);
                }
            }
        }
    }
}
