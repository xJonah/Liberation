using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTimeLimit : MonoBehaviour
{

    //Field
    public static int timeLimit;

    // Store value depending on time limit chosen by user. This is used for the game countdown timer.
    public void HandleInputData(int val) {
        if (val == 0) {
            timeLimit = val;
        } else if (val == 1) {
            timeLimit = val;
        }
        else if (val == 2) {
            timeLimit = val;
        }
        else if (val == 3) {
            timeLimit = val;
        }
        else if (val == 4) {
            timeLimit = val;
        }
        else if (val == 5) {
            timeLimit = val;
        }
        else if (val == 6) {
            timeLimit = val;
        }
    }
}
