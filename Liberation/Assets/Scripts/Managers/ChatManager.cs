using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Photon.Chat.Demo;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    [SerializeField] private string playerName;

    public TMP_Text messageArea;
    public TMP_InputField messageInput;

    #region Methods

    private void Awake()
    {
        playerName = PhotonNetwork.LocalPlayer.NickName;
    }

    void Start()
    {
        chatClient = new ChatClient(this)
        {
            ChatRegion = "EU"
        };

        GetConnected();

    }

    public void GetConnected()
    {
        Debug.Log("Connecting");
        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(playerName);
        ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();

        chatClient.ConnectUsingSettings(chatSettings);
    }

    void Update()
    {
        chatClient.Service();

        if (Input.GetKeyUp(KeyCode.Return)) { SendMsg(); }
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
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
            messageArea.text += senders[i] + ": " + messages[i] + "\n";
        }
    }

    public void SendMsg()
    {
        string message = messageInput.text;
        chatClient.PublishMessage("World", message);
        messageInput.text = "";
    }

    #endregion


    #region Interface Methods
    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }


    public void OnPrivateMessage(string sender, object message, string channelName)
    {
       
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    #endregion

}
