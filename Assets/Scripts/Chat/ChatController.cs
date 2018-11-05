using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class ChatController : NetworkBehaviour {

    public ChatMessage ChatMessagePrefab;
    
    private InputField _chatInputField;
    private RSNetWorkManager _networkManager;

    void Start()
    {
        _chatInputField = GetComponentInChildren(typeof(InputField), true) as InputField; 
        _networkManager = NetworkManager.singleton.gameObject.GetComponent<RSNetWorkManager>();
        if (isLocalPlayer)
        {
            StartCoroutine(LookForChatMessages());
        }
    }

    private IEnumerator LookForChatMessages()
    { 
        var chatContent = GetComponentInChildren(typeof(VerticalLayoutGroup), true).gameObject;
        while (true)
        {
            var messages = _networkManager.ChatMessages;
            yield return new WaitForSeconds(0.5f);
            _networkManager.ClearMessages();
            foreach (var message in messages)
            {
                var chatMessage = JsonUtility.FromJson<JsonMessage>(message);
                var messagePrefab = Instantiate(ChatMessagePrefab, chatContent.transform);
                messagePrefab.SetMessage(chatMessage.Sender, chatMessage.Message);
            }
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) { return; }
        if(_chatInputField.isFocused)
        {
            if (_chatInputField.text != "" && Input.GetButtonUp("Submit"))
            {
                
                WriteMessage(_chatInputField.text);
            }
        }    
    }

    private void WriteMessage(string currentMessage)
    {
        _chatInputField.DeactivateInputField();
        var message = new JsonMessage(PlayerPrefs.GetString("playerName"), currentMessage);
        var json = JsonUtility.ToJson(message);
        var networkMessage = new StringMessage(json);
        NetworkManager.singleton.client.Send((short)ChatMessage.ChatMessageTypes.CHAT_MESSAGE, networkMessage);
        _chatInputField.text = "";
        _chatInputField.ActivateInputField();
        _chatInputField.Select();
    }

    private class JsonMessage
    {
        public string Sender;
        public string Message;

        public JsonMessage(string sender, string message)
        {
            Sender = sender;
            Message = message;
        }
    }

    public bool IsChatFocused()
    {
        return _chatInputField.isFocused;
    }
}
