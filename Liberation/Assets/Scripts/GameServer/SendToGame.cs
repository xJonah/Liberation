using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class SendToGame : MonoBehaviourPunCallbacks
{
    public Button startButton;
    public Button waitingButton;
    public Button cancelButton;
    public TMP_Text playerNumber;

    // When master client changes scene, other clients follow
    void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // In waiting screen, wait for players before master client can start game
    void Update() {
        //Set to 4 players min later
        if(PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 1) {
            waitingButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
        }
        else {
            waitingButton.gameObject.SetActive(true);
        }

        playerNumber.text = PhotonNetwork.CurrentRoom.PlayerCount + " / 5";
    }

    // Load Game Scene
    public void LoadGame() {
        PhotonNetwork.LoadLevel("Game");
        
    }

    // Allow master client to close the room and return to main menu
    public void BackToMenu() {
        PhotonNetwork.LoadLevel("Main Menu");
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }
}
