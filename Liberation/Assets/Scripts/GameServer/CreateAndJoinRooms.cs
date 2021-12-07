using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Text.RegularExpressions;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI validationText;

    //Create lobby using text key and time limit + key validation (CyberSecurity)
    public void CreateRoom() {
        var input = createInput.text;
        var hasNumber = new Regex(@"[0-9]+");
        var hasMinimum6Chars = new Regex(@".{6,}");
        var timeLimitSet = dropdown.value;

        if (!hasMinimum6Chars.IsMatch(input)) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Room key must contain 6 characters";
        }
        else if (!hasNumber.IsMatch(input)) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Room key must contain a number";
        }
        else if (timeLimitSet == 0) {
            validationText.gameObject.SetActive(true);
            validationText.text = "Please set a time limit";             
        } 
        else {
            PhotonNetwork.CreateRoom(createInput.text);
        }
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
