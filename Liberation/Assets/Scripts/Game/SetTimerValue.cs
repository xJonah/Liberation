using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SetTimerValue : MonoBehaviour
{

    //Fields
    private int dropdownValue;
    private float time;

    //Set round time in custom properties of the room
    void Start() {
         
         if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            GetRoundTime();
            CalculateRoundTime();

            Hashtable hash = new Hashtable
            {
                { "Time", time }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
    }
    
    // Retrieve chosen time value from static variable
    void GetRoundTime() {
        dropdownValue = StoreTimeLimit.timeLimit;
    }

    // Calculate time flaot value depending on the time limit the user chose in the lobby
    void CalculateRoundTime() {
        if (dropdownValue == 1) {
            time = 30*60;
        }
        else if (dropdownValue == 2) {
            time = 45*60;
        }
        else if (dropdownValue == 3) {
            time = 60*60;
        }
        else if (dropdownValue == 4) {
            time = 90*60;
        }
        else if (dropdownValue == 5) {
            time = 120*60;
        }
        else if (dropdownValue == 6) {
            time = 999*60;
        }
        else {
            Debug.Log("Invalid Time");
        }
    }
}
