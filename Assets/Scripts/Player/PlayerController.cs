using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ChatController))]
public class PlayerController : NetworkBehaviour {

    public GameObject playerAxis;

    private MouseController _mouse;
    private CameraController _camera;
    private ChatController _chat;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            Destroy(playerAxis);
            Destroy(gameObject.transform.Find("Canvas").gameObject);
        }
    }

    public override void OnStartLocalPlayer()
    {
        var playerCamera = playerAxis.GetComponentInChildren<Camera>();
        _mouse = gameObject.AddComponent<MouseController>();
        _mouse.Init(playerCamera);
        _camera = gameObject.AddComponent<CameraController>();
        _camera.Init(playerCamera);
    }
}
