using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLobby : MonoBehaviour
{
    // Allows user to load the lobby scene from main menu after clicking 'start game' button
    public void JoinLobby() {
        SceneManager.LoadScene("Lobby");
    }
}
