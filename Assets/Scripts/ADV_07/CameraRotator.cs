using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity;
    [SerializeField] private float minYAngle = -20;
    [SerializeField] private float maxYAngle = 90;

    private InputDetector _inputDetector;
    private float rotationX;
    private float rotationY;

    public void SetInputDetector(InputDetector inputDetector)
    {
        _inputDetector = inputDetector;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        rotationX -= _inputDetector.MouseAxisInput.y * _mouseSensetivity;
        rotationY += _inputDetector.MouseAxisInput.x * _mouseSensetivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }

    public float GetCamraRotationY()
    {
        return transform.rotation.eulerAngles.y;
    }
}
