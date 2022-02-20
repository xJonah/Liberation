using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
 
public abstract class Tile : MonoBehaviour {

    [SerializeField] protected SpriteRenderer _renderer;

    public GameObject _highlight;
    public BaseUnit OccupiedUnit;
    public string tileName;
    public bool Empty => OccupiedUnit == null;
    
    private Dictionary<Vector2, Tile> tiles;

    public Tile() {
        tiles = new Dictionary<Vector2, Tile>();
        tiles = GridManager.Instance.GetTiles();
    }

    // Allow override in order for Tiles to have checkerboard pattern or not
    public virtual void Init(int x, int y) {
        
    }
 
    // Highlight Function when mouse hover over tile
    void OnMouseEnter() {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    // Remove highlight when no longer hovering over a specfic tile
    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }


    // Grid square is occupied when a unit is on it
    public void SetUnit(BaseUnit unit) { //Task - Assign counter to each unit/tile

        if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            if (unit.OccupiedTile != null) {
                unit.OccupiedTile.OccupiedUnit = null;
            }

            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
        //Only master client moves the initial position of spawned units
        else {
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
    }

    //Surrounding tiles no longer highlighted
    void OnMouseUp() {
        ArrayList tileVectors = GridManager.Instance.GetSurroundingTiles(this);

        foreach (Vector2 v in tileVectors) {
            var tile = tiles[v];
            tile._highlight.SetActive(false);
        }
    }

    //On mouse down surrouding tiles are highlighted
    void OnMouseDown() {
        ArrayList tileVectors = GridManager.Instance.GetSurroundingTiles(this);

        _highlight.SetActive(false);

        foreach (Vector2 v in tileVectors) {
            var tile = tiles[v];
            tile._highlight.SetActive(true);
        }

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
                            MenuManager.Instance.ShowBattleWin();
                            UnitManager.Instance.SetSelectedHuman(null);
                            GameManager.Instance.ChangeState(GameState.OrcTurn);
                        } else if (!result1) {
                            PhotonNetwork.Destroy(UnitManager.Instance.SelectedHuman.gameObject);
                            MenuManager.Instance.ShowBattleLoss();
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
                        MenuManager.Instance.ShowBattleWin();                  
                        UnitManager.Instance.SetSelectedOrc(null);

                        //Next clan turn. Will be Elf after MVP
                        GameManager.Instance.ChangeState(GameState.HumanTurn);
                    } else if (!result1) {
                        PhotonNetwork.Destroy(UnitManager.Instance.SelectedOrc.gameObject); 
                        MenuManager.Instance.ShowBattleLoss();          
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

