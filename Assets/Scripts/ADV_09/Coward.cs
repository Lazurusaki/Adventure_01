using ADV_09;
using UnityEngine;

public class Coward : IBehavior
{
    private Transform _transform;
    private TargetTracker _targetTracker;
    private Mover _mover;
    private Vector3 _currentDirection;

    public Coward(Transform transform, Mover mover, TargetTracker distanceTracker)
    {
        _transform = transform;
        _mover = mover;
        _targetTracker = distanceTracker;
    }

    private void ChangeDirection(Vector3 targetPosition)
    {
        _currentDirection = (_transform.position - targetPosition).normalized;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        ChangeDirection(_targetTracker.TargetTransform.position);
        _mover.Move(_currentDirection);
    }
}
