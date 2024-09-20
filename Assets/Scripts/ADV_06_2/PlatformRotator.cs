using UnityEngine;

public class PlatformRotator: MonoBehaviour 
{
    [SerializeField] private float _sensetivity = 20;
    [SerializeField] private float _maxRotation = 30;

    private float _horizontalRotation;
    private float _verticalRotation;

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    
    public void Rotate(Vector2 axisValues)
    {
        _horizontalRotation = axisValues.y * _sensetivity * Time.deltaTime;
        _verticalRotation = axisValues.x * _sensetivity * Time.deltaTime;

        transform.Rotate(Vector3.forward, _verticalRotation, Space.World);
        transform.Rotate(-Vector3.right, _horizontalRotation, Space.World);

        Clamp();
    }

    private void Clamp()
    {
        Vector3 currentRotation = transform.eulerAngles;

        float clampedXRotation = Mathf.Clamp(currentRotation.x > 180 ? currentRotation.x - 360 : currentRotation.x, -_maxRotation, _maxRotation);
        float clampedZRotation = Mathf.Clamp(currentRotation.z > 180 ? currentRotation.z - 360 : currentRotation.z, -_maxRotation, _maxRotation);

        transform.eulerAngles = new Vector3(clampedXRotation, currentRotation.y, clampedZRotation);
    }
}
