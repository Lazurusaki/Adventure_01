using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Transform _sail;
    [SerializeField] private Transform _flag;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _sailTurnSpeed;
    [SerializeField] private float _maxSailAngle;

    private Rigidbody _rigidbody;

    public float Speed { get { return _rigidbody.velocity.magnitude; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Turn(float angle)
    {
        _rigidbody.AddTorque(Vector3.up * angle * _turnSpeed, ForceMode.Impulse);
    }

    public void TurnSail(float angle)
    {
        float newAngle = _sail.localRotation.eulerAngles.y + angle * _sailTurnSpeed;
        newAngle = newAngle > 180 ? newAngle - 360 : newAngle;
        newAngle = Mathf.Clamp(newAngle, -_maxSailAngle, _maxSailAngle);
        _sail.localRotation = Quaternion.Euler(Vector3.up * newAngle);
    }

    public void Move(Vector2 velocuty)
    {
        Vector3 directionConverted = new Vector3(velocuty.x, 0, velocuty.y);
        _rigidbody.AddForce(directionConverted);
    }

    public Vector2 GetSailDirection()
    {
        return new Vector2(_sail.forward.x, _sail.forward.z);
    }

    public void SetFlagDirection(Vector2 direction)
    {
        _flag.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
    }
}
