using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
 
public abstract class Tile : MonoBehaviour {

    [SerializeField] protected SpriteRenderer _renderer;

    public GameObject _highlight;
    public BaseUnit OccupiedUnit;
    public string tileName;
    public bool Empty => OccupiedUnit == null;
    public UnityEngine.UI.Text winText, loseText;

    // Allow override in order for Tiles to have checkerboard pattern or not
    public virtual void Init(int x, int y) {
        
    }
 
    // Highlight Function when mouse hover over tile
    void OnMouseEnter() {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);

    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }


    // Grid square is occupied when a unit is on it
    public void SetUnit(BaseUnit unit) {

        //if(PhotonNetwork.LocalPlayer.IsMasterClient) {
        if (unit.OccupiedTile != null) {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        
        
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
        //}
    }

    

    //On mouse down highlight attackable tiles surrounding player
    void OnMouseDown() {

        if (GameManager.Instance.GameState == GameState.HumanTurn) {
            if (OccupiedUnit != null) {
                if (OccupiedUnit.Faction == Faction.Human) {
                    UnitManager.Instance.SetSelectedHuman((BaseHuman)OccupiedUnit);
                } 
                else {
                    if(UnitManager.Instance.SelectedHuman != null) {
                        var enemy = OccupiedUnit;
                        var result1 = BattleOne();
                        //var result2 = BattleTwo();
                        if (result1) {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            UnitManager.Instance.SetSelectedHuman(null);
                            GameManager.Instance.ChangeState(GameState.OrcTurn);
                        } else if (!result1) {
                            PhotonNetwork.Destroy(UnitManager.Instance.SelectedHuman.gameObject);
                            UnitManager.Instance.SetSelectedHuman(null);
                            GameManager.Instance.ChangeState(GameState.OrcTurn);
                        }

                    }
                }
            }
            else {
                if(UnitManager.Instance.SelectedHuman != null) {
                    SetUnit(UnitManager.Instance.SelectedHuman);
                    UnitManager.Instance.SetSelectedHuman(null);
                }
            }
        }

        else if (GameManager.Instance.GameState == GameState.OrcTurn) {
            if (OccupiedUnit != null) {
                if (OccupiedUnit.Faction == Faction.Orc) {
                    UnitManager.Instance.SetSelectedOrc((BaseOrc)OccupiedUnit);
                } 
            else {
                if(UnitManager.Instance.SelectedOrc != null) {
                    var enemy = OccupiedUnit;
                    var result1 = BattleOne();
                    //var result2 = BattleTwo();
                    if (result1) {
                        PhotonNetwork.Destroy(enemy.gameObject);                     
                        UnitManager.Instance.SetSelectedOrc(null);

                        //Next clan turn. Will be Elf after MVP
                        GameManager.Instance.ChangeState(GameState.HumanTurn);
                    } else if (!result1) {
                        PhotonNetwork.Destroy(UnitManager.Instance.SelectedOrc.gameObject);           
                        UnitManager.Instance.SetSelectedOrc(null);
                        //Next clan turn. Will be Elf after MVP
                        GameManager.Instance.ChangeState(GameState.HumanTurn);
                    }

                }
            }
        }
            else {
                if(UnitManager.Instance.SelectedOrc != null) {
                    SetUnit(UnitManager.Instance.SelectedOrc);
                    UnitManager.Instance.SetSelectedOrc(null);
                }
            }
        }

    }

    //Dice 2vs2 Battles
    public bool BattleOne() {

        bool result = false;

        int playerDiceResult = Random.Range(1, 6);

        int enemyDiceResult = Random.Range(1, 6);

        if (playerDiceResult > enemyDiceResult) {
            result = true;
        } 
        else {
            result = false;
        }


        return result;
    }

    public bool BattleTwo() {
        
        bool result = false;

        int playerDiceResult = Random.Range(1, 6);

        int enemyDiceResult = Random.Range(1, 6);

        if (playerDiceResult > enemyDiceResult) {
            result = true;
        } 
        else {
            result = false;
        }

        return result;
    }

}

