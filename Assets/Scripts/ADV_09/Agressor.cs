using ADV_09;
using UnityEngine;

public class Agressor : IBehavior
{
    private Transform _transform;
    private Transform _targetTransform;
    private Mover _mover;
    private Vector3 _currentDirection;

    public Agressor(Transform transform, Mover mover, Transform targetTransform)
    {
        _transform = transform;
        _mover = mover;
        _targetTransform = targetTransform;
    }

    private void ChangeDirection(Vector3 targetPosition)
    {
        _currentDirection = (targetPosition - _transform.position).normalized;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        ChangeDirection(_targetTransform.position);
        _mover.Move(_currentDirection);
    }
}
