using UnityEngine;

public class BombSpawner
{
    private Bomb _bombPrefab;
    public BombSpawner(Bomb prefab)
    {
        _bombPrefab = prefab;
    }

    public void Spawn(Vector3 position)
    {
        Bomb bomb = Object.Instantiate(_bombPrefab, position, Quaternion.identity, null);
        bomb.Explode();
        Object.Destroy(bomb.gameObject);
    }
}
