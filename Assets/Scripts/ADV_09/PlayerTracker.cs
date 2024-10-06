using UnityEngine;

public class PlayerTracker
{
    private const float AgressionDistance = 3f;

    private Transform _transform;
    private Transform _playerTransform;
    private float _currentDistance;

    public bool IsReact => _currentDistance <= AgressionDistance;

    public PlayerTracker(Transform transform, Transform playerTransform)
    {
        _transform = transform;
        _playerTransform = playerTransform;
    }

    public void Update()
    {
        _currentDistance = Vector3.Distance(_transform.position, _playerTransform.position);
    }
}
