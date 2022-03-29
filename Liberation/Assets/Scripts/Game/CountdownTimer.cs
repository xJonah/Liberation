using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CountdownTimer : MonoBehaviour
{

    //Fields
    public TMP_Text timerUI;
    public float time;
    private bool startTime = false;

    //Fetch round time from custom properties
    void Start()
    {
        startTime = true;

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Offline");
            time = 30 * 60;
            return;
        }
        else
        {
            time = (float)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        }
    }

    //Update timer throughout game
    void Update()
    {
        //Double check if start() has been executed
        if (!startTime) {
            return;
        }

        //Decrease timer
        if (time > 0) {
            time -= Time.deltaTime;
        }
    
        //End game on timer completion
        if (time <= 0) {
            GameManager.Instance.ChangeState(GameState.EndGame);
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
        
        //Format string
        double minutes = Mathf.FloorToInt(timeToDisplay / 60);
        double seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
