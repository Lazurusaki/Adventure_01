using UnityEngine;

public class HandleController
{
    private Handle _handle;
    private LayerMask _groundLayer;

    private float _defaultHandleDistance = 10;
    private float _maxRayDistance = 1000;
    private float _handleOffsetY;
    private Vector3 _defaultPosition;

    public HandleController(Handle handle, LayerMask groundLayer)
    {
        _handle = handle;
        _defaultPosition = Camera.main.transform.position;
        _groundLayer = groundLayer;
    }

    public bool TryPick(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxRayDistance))
        {
            if (hit.collider.TryGetComponent(out IPickable pickable) == false)
                return false;

            _handle.Move(hit.transform.position);
            _handle.Pick(pickable);     
        }

        if (Physics.Raycast(ray, out hit, _maxRayDistance, _groundLayer))
            _handleOffsetY = hit.point.y - hit.transform.position.y;

        return true;
    }

    public void Update()
    {
        if (_handle.IsEmpty) return;
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 newHandlePosition;

        if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _groundLayer))
            newHandlePosition = new Vector3(hit.point.x, hit.point.y + _handleOffsetY, hit.point.z);
        else
            newHandlePosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(_defaultHandleDistance);

        _handle.Move(newHandlePosition);
    }

    public void Release()
    {
        _handle.Release();
    }
}
