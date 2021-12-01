using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Text.RegularExpressions;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    [SerializeField] private GameObject validationText;

    //Create lobby using text key + key validation (CyberSecurity)
    public void CreateRoom() {
        var input = createInput.text;
        var hasNumber = new Regex(@"[0-9]+");
        var hasMinimum6Chars = new Regex(@".{6,}");
     
        if (hasMinimum6Chars.IsMatch(input) && hasNumber.IsMatch(input)) {
            PhotonNetwork.CreateRoom(createInput.text);
        } else {
            validationText.SetActive(true);
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
