using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ChatController : NetworkBehaviour {

    public ChatMessage ChatMessagePrefab;
    public bool ChatFocused;
    
    private InputField _chatInputField;
    private GameObject _chatContent;
    private List<string> _chatMessages = new List<string>();
    private RSNetWorkManager _networkManager;

    void Start()
    {
        _chatInputField = GetComponentInChildren(typeof(InputField), true) as InputField;
        _chatContent = GetComponentInChildren(typeof(VerticalLayoutGroup), true).gameObject;
        _networkManager = NetworkManager.singleton.gameObject.GetComponent<RSNetWorkManager>();
        if (isLocalPlayer)
        {
            StartCoroutine(LookForChatMessages());
        }
    }

    private IEnumerator LookForChatMessages()
    { 
        while (true)
        {
            var messages = _networkManager.ChatMessages;
            yield return new WaitForSeconds(0.5f);
            var newMessages = messages.Except(_chatMessages).ToList();
            if (newMessages.Count > 0)
            {
                foreach (var message in newMessages)
                {
                    _chatMessages.Add(message);
                    var chatMessage = JsonUtility.FromJson<JsonMessage>(message);
                    var messagePrefab = Instantiate(ChatMessagePrefab, _chatContent.transform);
                    messagePrefab.SetMessage(chatMessage.Sender, chatMessage.Message);
                }
            }
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) { return; }
        
        if(_chatInputField.isFocused)
        {
            ChatFocused = true;
            if (_chatInputField.text != "" && Input.GetButtonUp("Submit"))
            {
                WriteMessage(_chatInputField.text);
            }
        } else
        {
            ChatFocused = false;
        }    
    }

    private void WriteMessage(string currentMessage)
    {
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
}
