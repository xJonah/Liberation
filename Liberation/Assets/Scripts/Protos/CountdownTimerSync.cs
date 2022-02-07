using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CountdownTimerSync : MonoBehaviour
{
    public TMP_Text timerUI;
    private int dropdownValue;
    private double roundTime, startTime;
    private bool startTimer = false;
    public float decTimer;
    private double incTimer;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            GetRoundTime();
            CalculateRoundTime();
            
            Hashtable hash = new Hashtable();
            hash.Add("RoundTime", roundTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            Hashtable ht = new Hashtable();
            ht.Add("StartTime", PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);

            startTimer = true;

        } else {
            roundTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["RoundTime"].ToString());
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
    }

    void Update()
    {
        if (!startTimer) return;

        incTimer = PhotonNetwork.Time - startTime;
        decTimer = (float)(roundTime - incTimer);
        DisplayTime(decTimer);

        if (decTimer <= 0) {
            //End game and declare winner
        }
    }

        // Display time in Minutes and Seconds
    void DisplayTime(float timeToDisplay) {

        
        if (timeToDisplay < 0) {
            timeToDisplay = 0;
        } 
        
        else if (timeToDisplay > 0) {
            timeToDisplay += 1;
        }
        
        double minutes = Mathf.FloorToInt(timeToDisplay / 60);
        double seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timerUI.text = $"{minutes}:{seconds}";
    }
    

    // Function to get time value from static storage
    void GetRoundTime() {
        dropdownValue = StoreTimeLimit.timeLimit;
    }

    // Set time value depending on the time limit the user chose in the lobby
    void CalculateRoundTime() {
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
