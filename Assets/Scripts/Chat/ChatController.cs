﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ChatController : NetworkBehaviour {

    public ChatMessage ChatMessagePrefab;

    private InputField _inputField;
    private GameObject _chatContent;
    private List<string> _chatMessages = new List<string>();


    void Start()
    {
        _inputField = GetComponentInChildren(typeof(InputField), true) as InputField;
        _chatContent = GetComponentInChildren(typeof(GridLayoutGroup), true).gameObject;
        if(isLocalPlayer)
        {
            StartCoroutine(CheckChatMessage());
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if(_inputField.isFocused && _inputField.text != "" && Input.GetButtonUp("Submit"))
            {
                WriteMessage(_inputField.text);
            }
            
        }  
    }

    private IEnumerator CheckChatMessage()
    {
        while (true)
        {
            var messages = NetworkManager.singleton.gameObject.GetComponent<RSNetWorkManager>().ChatMessages;
            yield return new WaitForSeconds(0.25f);
            var newMessages = messages.Except(_chatMessages).ToList();
            if(newMessages.Count > 0)
            {
                foreach (var message in newMessages)
                {
                    _chatMessages.Add(message);
                    var messagePrefab = Instantiate(ChatMessagePrefab, _chatContent.transform);
                    messagePrefab.transform.SetAsFirstSibling();
                    messagePrefab.SetMessage(message);
                }
            }
        }
    }

    private void WriteMessage(string currentMessage)
    {
        var message = new StringMessage(currentMessage);
        NetworkManager.singleton.client.Send((short)ChatMessage.ChatMessageTypes.CHAT_MESSAGE, message);
        _inputField.text = "";
        _inputField.ActivateInputField();
        _inputField.Select();
    }
}