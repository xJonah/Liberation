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
using System;
using System.Timers;

public class ChatManager : MonoBehaviour, IChatClientListener
{

    //Fields
    private ChatClient chatClient;
    [SerializeField] private string playerName;
    public TMP_Text messageArea;
    public TMP_InputField messageInput;
    private List<DateTime> messageTimestamps;
    public UnityEngine.UI.Button sendButton;

    #region Methods

    //Get player name
    private void Awake()
    {
        playerName = PhotonNetwork.LocalPlayer.NickName;
    }

    //Connect to chat room
    private void Start()
    {
        chatClient = new ChatClient(this)
        {
            ChatRegion = "EU"
        };

        messageTimestamps = new List<DateTime>();

        GetConnected();
    }

    //Connect using Photon Settings
    public void GetConnected()
    {
        Debug.Log("Connecting");
        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(playerName);
        ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();

        chatClient.ConnectUsingSettings(chatSettings);
    }

    void Update()
    {

        //Message listener
        if (chatClient != null) 
        {
            chatClient.Service();
        }

        //******Anti spam********

        //Time list Management
        foreach (DateTime dt in messageTimestamps)
        {
            var dateNow = DateTime.Now;
            var secondsBetweenLastMessage = (int)(dateNow - dt).TotalSeconds;

            if (secondsBetweenLastMessage > 2)
            {
                messageTimestamps.Remove(dt);
            }
        }

        //Enable/Disable send button
        if (messageTimestamps.Count < 5)
        {
            sendButton.gameObject.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Return)) { SendMsg(); }
        }
        else
        {
            sendButton.gameObject.SetActive(false);
        }

    }

    //On connected join world chat
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

    //Receive messages and format them
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            messageArea.text += senders[i] + ": " + messages[i] + "\n";
        }
    }

    public void ListCheck()
    {

    }


    //Send message to world Chat
    public void SendMsg()
    {
        string message = messageInput.text;

        //Message Validation (Cybersecurity)
        if (message == "") {
            return;
        } 
        else if (message.Length > 250) {
            chatClient.PublishMessage("World", playerName + "'s message is too long!");
        }
        else { 

            if (messageTimestamps.Count < 5)
            {
                //Send message
                chatClient.PublishMessage("World", message);
                var dateNow = DateTime.Now;
                messageTimestamps.Add(dateNow);
            }
            else
            {
                Debug.Log("Avoid spamming!");
            }
        }
        //Reset input field after send
        messageInput.text = "";
    }

    #endregion


    #region Unused Interface Methods
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
