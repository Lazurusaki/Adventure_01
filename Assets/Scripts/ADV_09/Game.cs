using ADV_08;
using UnityEngine;
using System;

namespace ADV_09
{
    [RequireComponent(typeof(EnemySpawner))]

    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private InputDetector _inputDetector;
        private EnemySpawner _enemySpawner;

        private void Awake()
        {
            if (_playerController == null)
                throw new NullReferenceException("PlayerController is not set");

            _enemySpawner = GetComponent<EnemySpawner>();
            _inputDetector = new InputDetector();
            _playerController.SetInputDetector(_inputDetector);
        }

        private void Start()
        {
            _enemySpawner.SpawnEnemies();
        }

        private void Update()
        {
            _inputDetector.Update();
        }
    }
}
