using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;
using SystemHashtable = System.Collections.Hashtable;

public class CountdownTimerNetwork : MonoBehaviour
{
    public TMP_Text textDisplay;
    private int dropdownValue;
    public float roundTime;

    // Get and Set Time on game start
    void Start() {
        GetRoundTime();
        SetRoundTime();
    }

    // Countdown in real time
    void Update() {
        if (roundTime > 0) {
            roundTime -= Time.deltaTime;
        } 
        else {
            roundTime = 0;
        }
        DisplayTime(roundTime);
    }

    // Display time in Minutes and Seconds
    void DisplayTime(float timeToDisplay) {
        if (timeToDisplay < 0) {
            timeToDisplay = 0;
        } 
        else if (timeToDisplay > 0) {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textDisplay.text = string.Format("{000:00}:{001:00}", minutes, seconds);
    }

    // Function to get time value from static storage
    void GetRoundTime() {
        dropdownValue = StoreTimeLimit.timeLimit;
    }

    // Set time value depending on the time limit the user chose in the lobby
    void SetRoundTime() {
        if (dropdownValue == 1) {
            roundTime = 30*60;
        }
        else if (dropdownValue == 2) {
            roundTime = 45*60;
        }
        else if (dropdownValue == 3) {
            roundTime = 60*60;
        }
        else if (dropdownValue == 4) {
            roundTime = 90*60;
        }
        else if (dropdownValue == 5) {
            roundTime = 120*60;
        }
        else if (dropdownValue == 6) {
            roundTime = 999*60;
        }
    }
}
