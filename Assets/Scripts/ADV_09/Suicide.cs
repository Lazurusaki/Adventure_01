using UnityEngine;

public class Suicide : IEnemyStateHandler
{
    private Transform _transform;
    private ParticleSystem _destroyEffect;

    public Suicide(Transform transform, ParticleSystem destroyEffect)
    {
        _transform = transform;
        _destroyEffect = destroyEffect;
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        Object.Instantiate(_destroyEffect, _transform.position, Quaternion.identity, null);
        Object.Destroy(_transform.gameObject);
    }
}
