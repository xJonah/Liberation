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

    void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update() {
        if(PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            waitingButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
        }
        else {
            waitingButton.gameObject.SetActive(true);
        }

        playerNumber.text = PhotonNetwork.CurrentRoom.PlayerCount + " / 5";
    }

    public void LoadGame() {
        PhotonNetwork.LoadLevel("Game");
    }

    public void BackToMenu() {
        PhotonNetwork.LoadLevel("Main Menu");
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }
}
