using UnityEngine;

public class LookAtRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    public void LookAtDirection(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float step = _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
    }
}
