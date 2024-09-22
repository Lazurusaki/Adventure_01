using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputDetector _inputDetector;
    private CharacterController _characterController;

    private float _axisInputDeadZone = 0.01f;
    private Vector3 _axisDerection;
    private Vector2 _axisInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_inputDetector != null)
        {
            _axisInput = _inputDetector.AxisInput.magnitude > _axisInputDeadZone ? _inputDetector.AxisInput : Vector2.zero;
            _characterController.Roll(_axisDerection);

            if (_inputDetector.IsJumping)
            {
                _characterController.Jump();
            }
        }
        else
        {
            Debug.Log("InputDetector is not assigned");
        }
    }

    public void SetInputDetector(InputDetector inputDetector)
    {
        _inputDetector = inputDetector;
    }

    public void SetAxisDirection(float rotation)
    {
        _axisDerection = Quaternion.Euler(Vector3.up * rotation) * new Vector3(_axisInput.y, 0, -_axisInput.x);
    }
    public void Disable()
    {
        _characterController.Freeze();
    }

    public void Enable()
    {
        _characterController.Unfreeze();
    }
}
