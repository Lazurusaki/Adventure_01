using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        _camera.transform.position = _target.position + _offset;   
    }
}
