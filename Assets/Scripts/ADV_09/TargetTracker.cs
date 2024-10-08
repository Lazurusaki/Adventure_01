using UnityEngine;

public class TargetTracker
{
    private const float AgressionDistance = 3f;

    private Transform _transform;   
    private float _currentDistance;

    public bool IsReact => _currentDistance <= AgressionDistance;
    public Transform TargetTransform { get; private set;}

    public TargetTracker(Transform transform, Transform targetTransform)
    {
        _transform = transform;
        TargetTransform = targetTransform;
    }

    public void Update()
    {
        _currentDistance = Vector3.Distance(_transform.position, TargetTransform.position);
    }
}
