using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;
    [SerializeField] private ParticleSystem _explodeEffectPrefab;
    [SerializeField] private LayerMask _affectLayer;

    private void Awake()
    {
        if (_explodeEffectPrefab == null)
            throw new System.NullReferenceException("Explode effect prefab is not set");
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _affectLayer);

        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                Vector3 explodeDirection = transform.position - collider.transform.position;
                if (collider.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.AddExplosionForce(_power, transform.position, _radius);
            }
        }

        if (_explodeEffectPrefab != null)
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity, null);
    }
}
