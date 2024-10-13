using UnityEngine;

public class Handle : MonoBehaviour
{
    private Transform _object;

    public bool IsEmpty => _object == null;

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public void Pick(Transform transform)
    {
        _object = transform;
        _object.position = this.transform.position;
        _object.SetParent(this.transform);

        if (_object.TryGetComponent(out Rigidbody rigidBody))
            rigidBody.isKinematic = true;
    }

    public void Release()
    {
        if (IsEmpty) return;

        _object.SetParent(null);

        if (_object.TryGetComponent(out Rigidbody rigidBody))
            rigidBody.isKinematic = false;

        _object = null;
    }
}
