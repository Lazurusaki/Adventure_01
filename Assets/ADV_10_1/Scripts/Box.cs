using UnityEngine;

public class Box : MonoBehaviour, IPickable, IExplodable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        TryGetComponent(out _rigidbody);
    }

    public Transform GetTransform()
    { 
        return transform; 
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    public void Explode(Vector3 position, float explosionPower, float explosionRadius)
    {
        if (_rigidbody == false)
            return;

        _rigidbody.AddExplosionForce(explosionPower, position, explosionRadius);
    }
}
