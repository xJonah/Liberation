using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Text.RegularExpressions;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    //Fields
    public InputField createInput;
    public InputField joinInput;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI validationText;
    public TextMeshProUGUI validationNameText;
    public GameObject namePanel;
    [SerializeField] private InputField playerName;

    // Create lobby
    public void CreateRoom() {
        var input = createInput.text;
        var hasNumber = new Regex(@"[0-9]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        var timeLimitSet = dropdown.value;

        //Room Options
        RoomOptions roomOptions = new RoomOptions() {
            IsOpen = true,
            MaxPlayers = (byte) 5,
            PublishUserId = true
        };

        //key validation(CyberSecurity)

        if (!hasMinimum8Chars.IsMatch(input)) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Must contain 8 characters and a number";
        }
        else if (!hasNumber.IsMatch(input)) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Must contain a number";
        }
        else if (timeLimitSet == 0) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Please set a time limit";             
        } 
        else {
            PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);
        }
    }

    //Choose nickname for players to see
    public void ChooseName() {

        if (playerName.text != "") {
            PhotonNetwork.NickName = playerName.text;
            
            namePanel.SetActive(false);
            validationNameText.gameObject.SetActive(false);
        }
        else {
            validationNameText.gameObject.SetActive(true);
            validationNameText.text = "Please choose a name!";
        }
    }    

    // Join lobby using text key
    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    // Send to game scene once lobby is joined or created
    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("WaitingRoom");
    }

}
