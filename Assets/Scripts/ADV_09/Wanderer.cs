using ADV_09;
using UnityEngine;

public class Wanderer : IEnemyStateHandler
{
    private float _directionChangeFrequency = 1.0f;
    private float _timer;
    private Transform _transform;
    private Mover _mover;
    private Vector3 _currentDirection;

    public Wanderer(Transform transform, Mover mover)
    {
        _transform = transform;
        _mover = mover;
        DefineNewRandomDirection();
    }

    private void DefineNewRandomDirection()
    {
        _currentDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    public void UpdateState()
    {
        _timer += Time.deltaTime;
        _mover.Move(_transform, _currentDirection);

        if (_timer >= _directionChangeFrequency)
        {
            DefineNewRandomDirection();
            _timer = 0;
        }
    }
}
