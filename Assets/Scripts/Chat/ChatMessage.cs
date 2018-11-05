using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : ChatMessageBase
{
    public void SetMessage(string sender, string message)
    {
        GetComponent<Text>().text = sender + " : " + message;
    }
}
