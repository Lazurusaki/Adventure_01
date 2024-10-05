using ADV_09;
using UnityEngine;

public class Coward : IEnemyStateHandler
{
    private Transform _transform;
    private Transform _targetTransform;
    private Mover _mover;
    private Vector3 _currentDirection;

    public Coward(Transform transform, Mover mover, Transform targetTransform)
    {
        _transform = transform;
        _mover = mover;
        _targetTransform = targetTransform;
    }

    private void ChangeDirection(Vector3 targetPosition)
    {
        _currentDirection = (_transform.position - targetPosition).normalized;
    }

    public void EnterState()
    {
    }

    public void UpdateState()
    {
        ChangeDirection(_targetTransform.position);
        _mover.Move(_transform, _currentDirection);
    }
}