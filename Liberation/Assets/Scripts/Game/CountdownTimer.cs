using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text textDisplay;
    private int dropdownValue;
    public float timeValue;

    // Get and Set Time on game start
    void Start() {
        GetTimeValue();
        SetTimeValue();
    }

    // Countdown in real time
    void Update() {
        if (timeValue > 0) {
            timeValue -= Time.deltaTime;
        } 
        else {
            timeValue = 0;
        }
        DisplayTime(timeValue);
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
    void GetTimeValue() {
        dropdownValue = StoreTimeLimit.timeLimit;
    }

    // Set time value depending on the time limit the user chose in the lobby
    void SetTimeValue() {
        if (dropdownValue == 1) {
            timeValue = 30*60;
        }
        else if (dropdownValue == 2) {
            timeValue = 45*60;
        }
        else if (dropdownValue == 3) {
            timeValue = 60*60;
        }
        else if (dropdownValue == 4) {
            timeValue = 90*60;
        }
        else if (dropdownValue == 5) {
            timeValue = 120*60;
        }
        else if (dropdownValue == 6) {
            timeValue = 999*60;
        }
    }
}
