using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Allows user  to return to main menu from lobby
    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
