using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Connect to Photon server during loading scene
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Join the photon lobby
    public override void OnConnectedToMaster() {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion + " Server!");
        PhotonNetwork.JoinLobby();
    }

    // Load main menu after connecting to server
    public override void OnJoinedLobby() {
        SceneManager.LoadScene("MainMenu");
    }
}
