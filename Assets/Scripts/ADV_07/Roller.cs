using UnityEngine;

public class Roller : MonoBehaviour
{
    [SerializeField] private float _rollPower;
    [SerializeField] private float _maxAngularVelocity;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.maxAngularVelocity = _maxAngularVelocity;
    }
    public void Roll(Vector3 direction)
    {
        Vector3 torque = direction * _rollPower;
        _rigidBody.AddTorque(torque, ForceMode.Acceleration);
    }
}
