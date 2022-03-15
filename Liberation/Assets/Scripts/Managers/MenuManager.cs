using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject tileInfo, tileUnit, turnInfo;
    public Text winText, loseText;
    public TMP_Text name1, name2, name3, name4, name5;
    public GameObject border1, border2, border3, border4, border5;

    void Awake() {
        Instance = this;
        SetUIPlayerNames();
    }

    public void SetUIPlayerNames() {

        ArrayList names = new ArrayList();

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList) {
            names.Add(p.NickName);
        }
        
        name1.text = (string) names[0];
        name2.text = (string) names[1];
        name3.text = (string) names[2];
        name4.text = (string) names[3];

        if (PhotonNetwork.CurrentRoom.PlayerCount > 4) {
            name5.gameObject.SetActive(true);
            name5.text = (string) names[4];
        }
    }

    //UI element to show the name of the biome and unit in a specific tile
    public void ShowTileInfo(Tile tile) {


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

    void Update() {
        ShowTurnInfo();
    }

    //UI element stating which faction's turn it is
    public void ShowTurnInfo() {

        if(GameManager.Instance.GameState == GameState.HumanTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Human";
            border1.SetActive(true);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
        else if (GameManager.Instance.GameState == GameState.OrcTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Orc";
            border1.SetActive(false);
            border2.SetActive(true);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);     
        }
        else if (GameManager.Instance.GameState == GameState.ElfTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Elf"; 
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(true);
            border4.SetActive(false);
            border5.SetActive(false);    
        }
        else if (GameManager.Instance.GameState == GameState.DemonTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Demon"; 
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(true);
            border5.SetActive(false);    
        }
        else if (GameManager.Instance.GameState == GameState.DwarfTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Dwarf";     
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(true);
        }
        else {
            turnInfo.GetComponentInChildren<Text>().text = "Waiting";
            border1.SetActive(false);
            border2.SetActive(false);
            border3.SetActive(false);
            border4.SetActive(false);
            border5.SetActive(false);
        }
    }

    //UI element to show a win or loss for battles
    public void ShowBattleWin() {
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

        public void ShowBattleLoss() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

}
