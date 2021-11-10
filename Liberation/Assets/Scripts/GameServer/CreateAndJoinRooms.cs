using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    //Create lobby using text key
    public void CreateRoom() {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    //Join lobby using text key
    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    //Send to game scene once lobby is joined or created
    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game");
    }
}
