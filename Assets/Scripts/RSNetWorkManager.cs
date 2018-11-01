using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class RSNetWorkManager : NetworkManager {

    public List<string> ChatMessages = new List<string>();

    // hook into NetworkManager client setup process
    public override void OnStartClient(NetworkClient mClient)
    {
        base.OnStartClient(mClient);
        mClient.RegisterHandler((short)ChatMessageBase.ChatMessageTypes.CHAT_MESSAGE, OnClientChatMessage);
    }

    // hook into NetManagers server setup process
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler((short)ChatMessageBase.ChatMessageTypes.CHAT_MESSAGE, OnServerChatMessage);
    }

    private void OnServerChatMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<StringMessage>();
        var chatMessage = new StringMessage(msg.value);
        NetworkServer.SendToAll((short)ChatMessageBase.ChatMessageTypes.CHAT_MESSAGE, chatMessage);
    }

    public virtual void OnClientChatMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("New chat message on client: " + msg.value);
        ChatMessages.Add(msg.value);
    }
}
