using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject tileInfo, tileUnit, turnInfo;
    public Text winText, loseText;

    void Awake() {
        Instance = this;
    }

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

    public void ShowTurnInfo() {

        if(GameManager.Instance.GameState == GameState.HumanTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Human";
        }
        else if (GameManager.Instance.GameState == GameState.OrcTurn) {
            turnInfo.GetComponentInChildren<Text>().text = "Orc";     
        }
        else {
            turnInfo.GetComponentInChildren<Text>().text = "Waiting";
        }
    }

    public void ShowBattleWin() {
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

        public void ShowBattleLoss() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

}
