using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviour
{

    //Fields
    public static MenuManager Instance;
    public GameObject tileInfo, tileUnit; 
    public TMP_Text name1, name2, name3, name4, name5;
    public TMP_Text winText, loseText, turnText, winnerText;

    //Instance + call player names function
    void Awake() {
        Instance = this;
        SetUIPlayerNames();
    }

    //Show Photon nicknames in UI
    public void SetUIPlayerNames()
    {

        if (PhotonNetwork.CurrentRoom != null)
        {

            ArrayList names = new ArrayList();

            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                names.Add(p.NickName);
            }

            if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(1))
            {
                name1.text = "You";
                name2.text = (string)names[1];
                name3.text = (string)names[2];
                name4.text = (string)names[3];

                if (PhotonNetwork.CurrentRoom.PlayerCount > 4)
                {
                    name5.gameObject.SetActive(true);
                    name5.text = (string)names[4];
                }
            }
            else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(2))
            {
                name1.text = (string)names[0];
                name2.text = "You";
                name3.text = (string)names[2];
                name4.text = (string)names[3];

                if (PhotonNetwork.CurrentRoom.PlayerCount > 4)
                {
                    name5.gameObject.SetActive(true);
                    name5.text = (string)names[4];
                }
            }
            else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(3))
            {
                name1.text = (string)names[0];
                name2.text = (string)names[1];
                name3.text = "You";
                name4.text = (string)names[3];

                if (PhotonNetwork.CurrentRoom.PlayerCount > 4)
                {
                    name5.gameObject.SetActive(true);
                    name5.text = (string)names[4];
                }
            }
            else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(4))
            {
                name1.text = (string)names[0];
                name2.text = (string)names[1];
                name3.text = (string)names[2];
                name4.text = "You";

                if (PhotonNetwork.CurrentRoom.PlayerCount > 4)
                {
                    name5.gameObject.SetActive(true);
                    name5.text = (string)names[4];
                }
            }

            else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(4))
            {
                name1.text = (string)names[0];
                name2.text = (string)names[1];
                name3.text = (string)names[2];
                name4.text = (string)names[3];

                if (PhotonNetwork.CurrentRoom.PlayerCount > 4)
                {
                    name5.gameObject.SetActive(true);
                    name5.text = "You";
                }
            }
        }
    }

    //UI element to show the name of the biome and unit in a specific tile
    public void ShowTileInfo(Tile tile) {

        if (PhotonNetwork.CurrentRoom == null)
        {
            tileInfo.GetComponentInChildren<Text>().text = "Offline";
            tileUnit.GetComponentInChildren<Text>().text = "Offline";
            return;
        }

        if (tile == null) {
            tileInfo.SetActive(false);
            tileUnit.SetActive(false);
        }
        
        
        tileInfo.GetComponentInChildren<Text>().text = tile.tileName;
        tileInfo.SetActive(true);

        if (tile.OccupiedUnit) {
        tileUnit.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
        tileUnit.SetActive(true);            
        }
    }

    //Show text if last battle was won
    public void ShowWinText()
    {
        turnText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

    //Show text if last battle was lost
    public void ShowLoseText()
    {
        turnText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

    //Show text if player tries to play when its not their turn
    public void ShowNotYourTurnText()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        turnText.gameObject.SetActive(true);
    }

    //Show name of the game winner
    public void ShowWinnerText(string winner)
    {
        winnerText.text = winner + "has won the match!";
        winnerText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        turnText.gameObject.SetActive(false);
    }
}
