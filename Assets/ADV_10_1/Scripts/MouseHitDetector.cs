using UnityEngine;

public class MouseHitDetector
{
    private float _maxRayDistance = 1000;

    public bool TryHit(LayerMask layerMask, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, _maxRayDistance, layerMask))
            return true;

        return false;
    }
}
