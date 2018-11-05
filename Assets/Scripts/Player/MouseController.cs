using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class MouseController : NetworkBehaviour  {

    private Camera _camera;
    private MovementController _movement;

    public void Init(Camera camera, MovementController movement)
    {
        _camera = camera;
        _movement = movement;
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
        if (hit.transform != null && !EventSystem.current.IsPointerOverGameObject())
        {
            switch(hit.transform.tag)
            {
                case "Walkable":
                    _movement.CmdRequestMove(hit.point);
                    break;
                case "Interactable":
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this.gameObject);
                    break;
            }
        }
    }
}