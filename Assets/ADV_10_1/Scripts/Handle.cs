using UnityEngine;

public class Handle : MonoBehaviour
{
    private IPickable _object;

    public bool IsEmpty => _object == null;

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public void Pick(IPickable pickable)
    {
        _object = pickable;
        Transform pickableTransform = pickable.GetTransform();
        pickableTransform.position = this.transform.position;
        pickableTransform.SetParent(this.transform);
        
        _object.GetRigidbody().isKinematic = true;
    }

    public void Release()
    {
        if (IsEmpty) return;

        _object.GetTransform().SetParent(null);
        _object.GetRigidbody().isKinematic = false;
        _object = null;
    }
}
