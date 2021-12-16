using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class CountdownTimerNetwork : MonoBehaviour
{
    public TMP_Text timerUI;
    private int dropdownValue;
    
    private float roundTime;
    public float timer;
    private bool gameStart = false;

    // Get and Set Time on game start
    void Start() {

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            GetRoundTime();
            SetRoundTime();
            timer = roundTime;
            gameStart = true;
            Hashtable ht = new Hashtable() {{"Time", timer}};
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        }
        else {
            timer = (float) PhotonNetwork.CurrentRoom.CustomProperties["Time"];
            gameStart = true;
        }
        
    }

    // Countdown in real time
    void Update() {

        if (!gameStart) {
            return;
        }

        timer -= Time.deltaTime;
        Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;
        ht.Remove("Time");
        ht.Add("Time", timer);
        PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        DisplayTime(timer);

        if(timer <= 0) {
            PhotonNetwork.DestroyAll();
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
        

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timerUI.text = $"{minutes}:{seconds}";
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
