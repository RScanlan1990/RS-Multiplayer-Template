using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : ChatMessageBase
{
    private string _message;

    public void SetMessage(string message)
    {
        _message = message;
        GetComponentInChildren<Text>().text = message;
    }
}
