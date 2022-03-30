using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    //Fields
    public GameObject border1, border2, border3, border4, border5;
    public GameObject turnInfo;

    //Set initial text value
    private void Start()
    {
        turnInfo.GetComponentInChildren<Text>().text = "Player Turn";
    }

    //Check and keep turn text and player borders updated
    private void Update()
    {
        ShowTurnInfo();
        ShowTurnBorders();
    }

    //Turn text
    public void ShowTurnInfo()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Offline";
        }
        else if (GameManager.Instance.GameState == GameState.HumanTurn)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Human";
        }
        else if (GameManager.Instance.GameState == GameState.OrcTurn)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Orc";
        }
        else if (GameManager.Instance.GameState == GameState.ElfTurn)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Elf";
        }
        else if (GameManager.Instance.GameState == GameState.DemonTurn)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Demon";
        }
        else if (GameManager.Instance.GameState == GameState.DwarfTurn)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Dwarf";
        }
        else if (GameManager.Instance.GameState == GameState.EndGame)
        {
            turnInfo.GetComponentInChildren<Text>().text = "Game End";
        }
        else
        {
            turnInfo.GetComponentInChildren<Text>().text = "Waiting";
        }
    }

    //Turn green borders surrounding name
    public void ShowTurnBorders()
    {

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        
        if (GameManager.Instance.GameState == GameState.HumanTurn)
        {
            border1.SetActive(true);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
        else if (GameManager.Instance.GameState == GameState.OrcTurn)
        {
            border1.SetActive(false);
            border2.SetActive(true);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
        else if (GameManager.Instance.GameState == GameState.ElfTurn)
        {
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(true);
            border4.SetActive(false);
            border5.SetActive(false);
        }
        else if (GameManager.Instance.GameState == GameState.DemonTurn)
        { 
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(true);
            border5.SetActive(false);
        }
        else if (GameManager.Instance.GameState == GameState.DwarfTurn)
        {
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(true);
        }
        else if (GameManager.Instance.GameState == GameState.EndGame)
        {
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
        else
        {
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
    }
}
