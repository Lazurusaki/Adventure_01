using System.Collections.Generic;
using UnityEngine;

namespace ADV_08
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointsRoot;
        [SerializeField] private List<Item> _items;

        private void Start()
        {
            if (_items == null)
            {
                Debug.LogError("Items is empty");
                return;
            }

            if (_spawnPointsRoot == null)
            {
                Debug.LogError("Spawn Points is empty");
                return;
            }

            Spawn();
        }

        private void Spawn()
        {
            Transform[] spawnPoints = _spawnPointsRoot.GetComponentsInChildren<Transform>();

            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint != _spawnPointsRoot)
                    Instantiate(_items[Random.Range(0, _items.Count)], spawnPoint.position, Quaternion.identity, null);
            }
        }
    }
}
