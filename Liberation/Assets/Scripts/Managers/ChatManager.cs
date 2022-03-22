using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    private string userId;
    
    public InputField messageInput;
    public Text messageArea;

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }


    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    void Start() {
        Application.runInBackground = true;

        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat)) {
            Debug.Log("No AppID provided");
            return;
        } 
        else
        {
            GetConnected();
        }
    }

    public void GetConnected()
    {
        chatClient = new ChatClient(this)
        {
            ChatRegion = "EU"
        };

        Debug.Log("Connecting");
        userId = PhotonNetwork.LocalPlayer.NickName;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userId));
    }

    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "World" });
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnecting");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            messageArea.text += senders[i] + ": " + messages[i] + ".\n";
        }
    }

    public void SendMsg()
    {
        string message = messageInput.text;
        chatClient.PublishMessage("World", message);
    }
}
