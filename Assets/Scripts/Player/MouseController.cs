using UnityEngine;
using UnityEngine.Networking;

public class MouseController : NetworkBehaviour  {

    private Camera _camera;
    private MovementController _movement;

    public void Init(Camera camera)
    {
        _camera = camera;
        _movement = gameObject.AddComponent<MovementController>();
        _movement.Init();
    }

    void Update ()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            DoClick();
        }
    }

    private void DoClick()
    {
        RayCast rayCast = new RayCast(_camera); 
        RaycastHit hit = rayCast.CameraToMousePosition();
        if (hit.transform != null)
        {
            switch(hit.transform.tag)
            {
                case "Walkable":
                    _movement.CalculatePathAndMove(hit.point);
                    break;
            }
        }
    }
}