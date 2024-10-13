using UnityEngine;

public class HandleController
{
    private Handle _handle;
    private MouseHitDetector _mouseHitDetector;
    private LayerMask _draggableLayer;
    private LayerMask _groundLayer;

    private float _defaultHandleDistance = 10;
    private float _handleOffsetY;
    private Vector3 _defaultPosition;

    public HandleController(Handle handle, MouseHitDetector mouseHitDetector, LayerMask draggableLayer, LayerMask groundLayer)
    {
        _handle = handle;
        _defaultPosition = Camera.main.transform.position;
        _mouseHitDetector = mouseHitDetector;
        _draggableLayer = draggableLayer;
        _groundLayer = groundLayer;
    }

    public bool TryPick()
    {
        RaycastHit hit;

        if (_mouseHitDetector.TryHit(_draggableLayer, out hit) == false)
            return false;

        _handle.Move(hit.transform.position);
        _handle.Pick(hit.transform);

        if (_mouseHitDetector.TryHit(_groundLayer, out hit))
            _handleOffsetY = hit.point.y - hit.transform.position.y;

        return true;
    }

    public void Update()
    {
        if (_handle.IsEmpty) return;

        Vector3 newHandlePosition;

        if (_mouseHitDetector.TryHit(_groundLayer, out RaycastHit hit))
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
