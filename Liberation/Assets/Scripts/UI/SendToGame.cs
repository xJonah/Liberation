using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;

public class SendToGame : MonoBehaviourPunCallbacks
{

    //Fields
    public Button startButton;
    public Button waitingButton;
    public Button cancelButton;
    public TMP_Text playerNumber;
    public TMP_Text playerNames;

    // When master client changes scene, other clients follow
    void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        UpdatePlayerList();
    }

    // In waiting screen, wait for players before master client can start game
    void Update() {
        //Set to 4 players min later
        //if(PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 3) {}
        if(PhotonNetwork.LocalPlayer.IsMasterClient) {
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

    //Display player names in waiting room
    public void UpdatePlayerList() {

        playerNames.text = "";

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList) {
            string nickname = p.NickName;
            playerNames.text += nickname + "\n" ;
        }

    }

    //Update player list on player joining the room
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdatePlayerList();
    }

    //Update player list on player joining the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList();
    }

    //Update player list on player leaving the room
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        UpdatePlayerList();
    }

    //Update player list on player leaving the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
    }
}
