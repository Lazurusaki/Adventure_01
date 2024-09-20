using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity;
    [SerializeField] private Transform _target;
    
    private InputDetector _inputDetector;

    public void SetInputDetector(InputDetector inputDetector)
    {
        _inputDetector = inputDetector;
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float RotationX = -_inputDetector.MouseAxisInput.y * _mouseSensetivity;
        float RotationY = _inputDetector.MouseAxisInput.x * _mouseSensetivity;
        transform.Rotate(RotationX , RotationY, 0f);
    }
}
