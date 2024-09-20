using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _rollPower;
    [SerializeField] private float _maxAngularVelocity;
    [SerializeField] private float _JumpPower;

    private InputDetector _inputDetector;
    private Vector2 _currentAxisInput;
    private Rigidbody _rigidBody;
    private float _axisInputDeadZone = 0.01f;  
    private bool _isGrounded;
    private bool _isJumping = false;
    private float _groundedDeadZone = 0.02f;
    private float _maxGroundAngle = 45f;

    private void Awake()
    {
        _rigidBody =GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.maxAngularVelocity = _maxAngularVelocity;
    }

    private void Update()
    {  
        if (_inputDetector != null)
        {
            _currentAxisInput = _inputDetector.AxisInput.magnitude > _axisInputDeadZone ? _inputDetector.AxisInput : Vector2.zero;

            if (_inputDetector.IsJumping && _isGrounded)
            {
                _isJumping = true;
            }
        }        
    }

    private void FixedUpdate()
    {
        if (_currentAxisInput != Vector2.zero)
        {
            Roll();
            _currentAxisInput = Vector2.zero;
        }

        if (_isJumping)
        {   
            Jump();
            _isJumping = false;
        }
    }
    
    private void Jump()
    {      
        _rigidBody.AddForce(Vector3.up * _JumpPower, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Ground"))
            {
                float angle = Vector3.Angle(contact.normal, Vector3.up);

                if (angle <= _maxGroundAngle)
                {
                    _isGrounded = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void Roll()
    {
        Vector3 torque = new Vector3(_currentAxisInput.y, 0, -_currentAxisInput.x) * _rollPower;
        _rigidBody.AddTorque(torque, ForceMode.Acceleration);
    }

    public void SetInputDetector(InputDetector inputDetector)
    { 
        _inputDetector = inputDetector; 
    }

    public void Freeze()
    {
        _rigidBody.isKinematic = true;
    }

    public void Unfreeze()
    {
        _rigidBody.isKinematic = false;
    }    
}

