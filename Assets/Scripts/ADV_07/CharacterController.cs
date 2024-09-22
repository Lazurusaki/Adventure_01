using UnityEngine;

[RequireComponent(typeof(Roller))]
[RequireComponent(typeof(Jumper))]
public class CharacterController : MonoBehaviour
{
    private Roller _roller;
    private Jumper _jumper;
    private Rigidbody _rigidBody;

    private float _maxGroundAngle = 45f;
    private bool _isGrounded;
    private bool _isJumping = false;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _roller = GetComponent<Roller>();
        _jumper = GetComponent<Jumper>();
    }

    private void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
        {
            _roller.Roll(_moveDirection);
            _moveDirection = Vector3.zero;
        }

        if (_isJumping)
        {
            _jumper.Jump();
            _isJumping = false;
        }
    }

    private bool IsContainsGroundComponent(Transform transform)
    {
        return transform.TryGetComponent<Ground>(out _);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsContainsGroundComponent(collision.transform))
        {
            foreach (ContactPoint contact in collision.contacts)
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
        if (IsContainsGroundComponent(collision.transform))
        {
            _isGrounded = false;
        }
    }

    public void Jump()
    {
        if (_isGrounded)
            _isJumping = true;
    }
    public void Roll(Vector3 direction)
    {
        _moveDirection = direction;
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
