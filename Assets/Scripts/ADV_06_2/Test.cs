using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigidBody.sleepThreshold = 0;     
    }
}
