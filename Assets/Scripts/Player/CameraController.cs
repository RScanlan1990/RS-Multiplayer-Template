using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {

    private float maxZoom = 35.0f;
    private float minZoom = 5.0f;
    private float _zoomValue;
    private Camera _camera;
    private GameObject _player;
    private GameObject _cameraAxis;
    private Vector3 _playerPos;
    private ChatController _chat;

    public void Init(Camera camera)
    {
        _camera = camera;
        _player = gameObject;
        _cameraAxis = gameObject.transform.Find("CameraAxis").gameObject;
        _cameraAxis.transform.parent = null;
        _chat = gameObject.GetComponent<ChatController>();
    }

    void Update()
    {
        FollowPlayer();
        LookAtPlayer();
        ZoomCamera(Input.GetAxis("Zoom"));
        if (!_chat.ChatFocused)
        {
            VerticalRotation(Input.GetAxis("Vertical"));
            HorizontalRotation(Input.GetAxis("Horizontal"));
        }
    }

    private void FollowPlayer()
    {
        _playerPos = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.0f, _player.transform.position.z);
        _cameraAxis.transform.position = _playerPos;
    }

    private void LookAtPlayer()
    {
        _camera.transform.LookAt(_cameraAxis.transform.position);
    }

    private void ZoomCamera(float input)
    {
        var localForward = _camera.transform.worldToLocalMatrix.MultiplyVector(_camera.transform.forward);
        var zoomVector = localForward * input;

        if (input > 0.0f)
        {
            ZoomIn(zoomVector); 
        }

        if (input < 0.0f)
        {
            ZoomOut(zoomVector);
        }
    }

    private void ZoomIn(Vector3 zoomVector)
    {
        if(AboveMinZoom())
        {
            _camera.transform.Translate(zoomVector);
        }  
    }

    private bool AboveMinZoom()
    {
        return Vector3.Distance(_camera.transform.position, _playerPos) > minZoom;
    }

    private void ZoomOut(Vector3 zoomVector)
    {
        if(BelowMaxZoom())
        {
            _camera.transform.Translate(zoomVector);
        }
    }

    private bool BelowMaxZoom()
    {
        return Vector3.Distance(_camera.transform.position, _playerPos) < maxZoom;
    }

    private void VerticalRotation(float input)
    {
        var direction = transform.up * input;
        if(direction.y > 0.0f)
        {
            if (_camera.transform.eulerAngles.x < 60.0f)
            {
                _camera.transform.Translate(direction);
            }
        }

        if (direction.y < 0.0f)
        {
            if (_camera.transform.eulerAngles.x > 20.0f)
            {
                _camera.transform.Translate(direction);
            }
        }
    }

    private void HorizontalRotation(float input)
    {
        _cameraAxis.transform.Rotate(_cameraAxis.transform.up * (input * 5.0f));
    }
}
