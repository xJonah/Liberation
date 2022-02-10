using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerUI;
    private int dropdownValue;
    public float time;
    private bool startTime = false;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            GetRoundTime();
            CalculateRoundTime();

            Hashtable hash = new Hashtable();
            startTime = true;
            hash.Add("Time", time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

        }
        else {
            //time = (float) PhotonNetwork.CurrentRoom.CustomProperties["Time"];
            time = float.Parse(PhotonNetwork.CurrentRoom.CustomProperties["Time"].ToString());

            if (time != 0) {
                startTime = true;
            }
        }    
    }

    void Update()
    {
        if (!startTime) {
            return;
        }

        if (time > 0) {
            time -= Time.deltaTime;
        }
    
        if (time <= 0) {
            //End game and declare winner
        }

        DisplayTime(time);
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
    }

    
    // Function to get time value from static storage
    void GetRoundTime() {
        dropdownValue = StoreTimeLimit.timeLimit;
    }

    // Set time value depending on the time limit the user chose in the lobby
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
