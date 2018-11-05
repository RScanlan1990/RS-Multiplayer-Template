using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ChatController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(InventoryController))]
public class PlayerController : NetworkBehaviour {

    public GameObject cameraParent;
    private MouseController _mouse;
    private CameraController _camera;
    private AnimationController _animation;
    private InventoryController _inventory;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            Destroy(cameraParent);
            gameObject.transform.Find("Canvas").gameObject.SetActive(false);
        }
    }

    public override void OnStartLocalPlayer()
    {
        var playerCamera = cameraParent.GetComponentInChildren<Camera>();
        _mouse = gameObject.AddComponent<MouseController>();
        _mouse.Init(playerCamera, gameObject.GetComponent<MovementController>());
        _camera = gameObject.AddComponent<CameraController>();
        _camera.Init(playerCamera);
        _animation = gameObject.AddComponent<AnimationController>();
        _animation.Init();
    }
}
