using ADV_09;
using UnityEngine;

public class Patrol : IEnemyStateHandler
{
    private float _targetReachDeadZone = 0.1f;
    private Transform _transform;
    private Mover _mover;
    private Vector3 _currentDirection;
    private Vector3[] _patrolPoints;
    private int _currentPatrolPointIndex;

    public Patrol(Transform transform, Mover mover, Transform patrolPointsContainer)
    {
        _transform = transform;
        _mover = mover;
        _patrolPoints = new Vector3[patrolPointsContainer.childCount];

        for (int i = 0; i < patrolPointsContainer.childCount; i++)
        {
            _patrolPoints[i] = patrolPointsContainer.GetChild(i).position;
        }

        _currentPatrolPointIndex = 0;
        ChangeDirection(_patrolPoints[_currentPatrolPointIndex]);
    }

    private void ChangeDirection(Vector3 targetPosition)
    {
        _currentDirection = (targetPosition - _transform.position).normalized;
    }

    private void ChangePatrolPoint()
    {
        _currentPatrolPointIndex++;

        if (_currentPatrolPointIndex == _patrolPoints.Length)
            _currentPatrolPointIndex = 0;

        ChangeDirection(_patrolPoints[_currentPatrolPointIndex]);
    }

    public void UpdateState()
    {
        if (Vector3.Distance(_transform.position, _patrolPoints[_currentPatrolPointIndex]) <= _targetReachDeadZone)
            ChangePatrolPoint();

        _mover.Move(_transform, _currentDirection);
    }
}
