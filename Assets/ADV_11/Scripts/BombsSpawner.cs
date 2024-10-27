using UnityEngine;

namespace ADV_11
{
    public class BombsSpawner
    {
        Transform _bombsContainer;
        Bomb _bombPrefab;
        AudioSource _audioSource;

        public BombsSpawner(Transform bombsContainer, Bomb bombPrefab, AudioSource audioSource)
        {
            _bombPrefab = bombPrefab;
            _bombsContainer = bombsContainer;  
            _audioSource = audioSource;
        }

        public void SpawnBombs()
        {
            foreach (Transform spawnPoint in _bombsContainer)
            {
                if (spawnPoint != _bombsContainer)
                {
                    Bomb bomb = Object.Instantiate(_bombPrefab, spawnPoint.position, Quaternion.identity, null);
                    bomb.Initialize(_audioSource);
                }
            }
        }
    }
}
