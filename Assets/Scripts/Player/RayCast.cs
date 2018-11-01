using UnityEngine;

public class RayCast {

    private Camera _camera;

    public RayCast(Camera camera)
    {
        _camera = camera;
    }

    internal RaycastHit CameraToMousePosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _camera.farClipPlane))
        {
            return hit;
        }
        return new RaycastHit();
    }
}
