using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : ChatMessageBase
{
    private string Sender;
    private string Message;

    public void SetMessage(string sender, string message)
    {
        Sender = sender;
        Message = message;
        GetComponentInChildren<Text>().text = sender + " : " + message;
    }
}
