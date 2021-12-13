using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    // Connect to Photon server during loading scene
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Join the photon lobby
    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    // Send to Main Menu scene once connected to server
    public override void OnJoinedLobby() {
        SceneManager.LoadScene("MainMenu");
    }

    
}
