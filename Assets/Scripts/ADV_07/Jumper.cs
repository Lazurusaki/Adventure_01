using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpPower;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);
    }
}
