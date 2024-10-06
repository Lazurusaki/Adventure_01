using UnityEngine;

public class Suicide : IBehavior
{
    private Transform _transform;
    private ParticleSystem _destroyEffect;

    public Suicide(Transform transform, ParticleSystem destroyEffect)
    {
        _transform = transform;
        _destroyEffect = destroyEffect;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        Object.Instantiate(_destroyEffect, _transform.position, Quaternion.identity, null);
        Object.Destroy(_transform.gameObject);
    }
}
