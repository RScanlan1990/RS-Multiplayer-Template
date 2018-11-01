using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ChatController))]
public class PlayerController : NetworkBehaviour {

    public GameObject cameraParent;

    private MouseController _mouse;
    private CameraController _camera;
    private ChatController _chat;
    private AnimationController _animation;

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
        _mouse.Init(playerCamera);
        _camera = gameObject.AddComponent<CameraController>();
        _camera.Init(playerCamera);
        _animation = gameObject.AddComponent<AnimationController>();
        _animation.Init();
    }
}
