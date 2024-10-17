using UnityEngine;

public class ExplosionShooter
{
    private ParticleSystem _explosionPrefab;
    private float _explosionRadius;
    private float _explosionPower;
    private float _maxRayDistance = 1000;

    public ExplosionShooter(ParticleSystem expsionPrefab, float explosionPower, float explosionRadius)
    {
        _explosionPrefab = expsionPrefab;
        _explosionPower = explosionPower;
        _explosionRadius = explosionRadius;  
    }

    public void Shoot(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxRayDistance))
        {
            Collider[] colliders = Physics.OverlapSphere(hit.point, _explosionRadius);

            if (colliders.Length > 0)
                foreach (Collider collider in colliders)
                    if (collider.TryGetComponent(out IExplodable explodable))
                        if (collider.TryGetComponent(out Rigidbody rigidbody))
                            rigidbody.AddExplosionForce(_explosionPower, hit.point, _explosionRadius);

            Object.Instantiate(_explosionPrefab, hit.point, Quaternion.identity, null);
        }
    }
}
