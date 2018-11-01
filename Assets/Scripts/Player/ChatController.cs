using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChatController : NetworkBehaviour {

    private InputField _inputField;

    void Start()
    {
        _inputField = GetComponentInChildren<InputField>();
    }

    private void Update()
    { 
        if(isLocalPlayer && _inputField.isFocused && _inputField.text != "" && Input.GetButtonUp("Submit"))
        {
            CmdWriteMessage(_inputField.text);
        }        
    }

    [Command]
    private void CmdWriteMessage(string msg)
    {
        RpcReceiveChat(msg);
        _inputField.text = "";
        _inputField.ActivateInputField();
        _inputField.Select();
    }

    [ClientRpc]
    private void RpcReceiveChat(string msg)
    {
        Debug.Log("Got chat message: " + msg);
    }
}
