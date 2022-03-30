using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public abstract class Tile : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer _renderer;

    public GameObject _highlight;
    public BaseUnit OccupiedUnit;
    public string tileName;
    public bool Empty => OccupiedUnit == null;
    private Dictionary<Vector2, Tile> tiles;


    // Allow override in order for Tiles to have checkerboard pattern or not
    public virtual void Init(int x, int y)
    {

    }

    // Highlight Function when mouse hover over tile
    void OnMouseEnter()
    {
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
    public void SetUnit(BaseUnit unit)
    { //Task - Assign counter to each unit/tile as well

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (unit.OccupiedTile != null)
            {
                unit.OccupiedTile.OccupiedUnit = null;
            }
            //Only master client moves the initial position of spawned units
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
        else
        {
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
    }

    /* Surrounding tiles no longer highlighted
    void OnMouseUp() {
        ArrayList tileVectors = GridManager.Instance.GetSurroundingTiles(this);

        foreach (Vector2 v in tileVectors) {
            var tile = tiles[v];
            tile._highlight.SetActive(false);
        }
    } 
    */

    //On mouse down surrouding tiles are highlighted
    void OnMouseDown()
    {
        tiles = new Dictionary<Vector2, Tile>();
        tiles = GridManager.Instance.GetTiles();
        ArrayList tileVectors = GridManager.Instance.GetSurroundingTiles(this);

        if (GameManager.Instance.GameState == GameState.EndGame)
        {
            //Game has ended
            return;
        }

        else if (PhotonNetwork.IsConnected && !GameManager.Instance.IsMyTurn())
        {
            //Not my turn
            return;
        }

        //HUMAN TURN
        if (GameManager.Instance.GameState == GameState.HumanTurn)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Human)
                {
                    UnitManager.Instance.SetSelectedHuman((BaseHuman)OccupiedUnit);
                    foreach (Vector2 v in tileVectors)
                    {
                        var tile = tiles[v];
                        if (tile.OccupiedUnit.Faction != Faction.Human)
                        {
                            tile._highlight.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (UnitManager.Instance.SelectedHuman != null)
                    {
                        foreach (Vector2 v in tileVectors)
                        {
                            var tile = tiles[v];
                            tile._highlight.SetActive(false);
                        }

                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();
                        if (result1)
                        {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            UnitManager.Instance.SpawnNewHuman(enemy.OccupiedTile);
                            //MenuManager.Instance.ShowBattleWin();
                            UnitManager.Instance.SetSelectedHuman(null);
                            GameManager.Instance.ChangeState(GameState.OrcTurn);
                        }
                        else if (!result1)
                        {
                            //MenuManager.Instance.ShowBattleLoss();
                            UnitManager.Instance.SetSelectedHuman(null);
                            GameManager.Instance.ChangeState(GameState.OrcTurn);
                        }
                    }
                }
            }
        }

        //ORC TURN
        else if (GameManager.Instance.GameState == GameState.OrcTurn)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Orc)
                {
                    UnitManager.Instance.SetSelectedOrc((BaseOrc)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedOrc != null)
                    {
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();
                        if (result1)
                        {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            //MenuManager.Instance.ShowBattleWin();                  
                            UnitManager.Instance.SetSelectedOrc(null);
                            //GameManager.Instance.ChangeState(GameState.ElfTurn); 
                            GameManager.Instance.ChangeState(GameState.HumanTurn); //For Development Process
                        }
                        else if (!result1)
                        {
                            //MenuManager.Instance.ShowBattleLoss();          
                            UnitManager.Instance.SetSelectedOrc(null);
                            //GameManager.Instance.ChangeState(GameState.ElfTurn);
                            GameManager.Instance.ChangeState(GameState.HumanTurn); //For Development Process
                        }
                    }
                }
            }
        }

        //ELF TURN
        else if (GameManager.Instance.GameState == GameState.ElfTurn)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Elf)
                {
                    UnitManager.Instance.SetSelectedElf((BaseElf)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedElf != null)
                    {
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();
                        if (result1)
                        {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            //MenuManager.Instance.ShowBattleWin();                  
                            UnitManager.Instance.SetSelectedElf(null);
                            GameManager.Instance.ChangeState(GameState.DwarfTurn);
                        }
                        else if (!result1)
                        {
                            PhotonNetwork.Destroy(UnitManager.Instance.SelectedElf.gameObject);
                            //MenuManager.Instance.ShowBattleLoss();          
                            UnitManager.Instance.SetSelectedElf(null);
                            GameManager.Instance.ChangeState(GameState.DwarfTurn);
                        }
                    }
                }
            }
        }

        //DWARF TURN
        else if (GameManager.Instance.GameState == GameState.DwarfTurn)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Dwarf)
                {
                    UnitManager.Instance.SetSelectedDwarf((BaseDwarf)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedDwarf != null)
                    {
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();
                        if (result1)
                        {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            // MenuManager.Instance.ShowBattleWin();                  
                            UnitManager.Instance.SetSelectedDwarf(null);
                            GameManager.Instance.ChangeState(GameState.DemonTurn);
                        }
                        else if (!result1)
                        {
                            PhotonNetwork.Destroy(UnitManager.Instance.SelectedDwarf.gameObject);
                            // MenuManager.Instance.ShowBattleLoss();          
                            UnitManager.Instance.SetSelectedDwarf(null);
                            GameManager.Instance.ChangeState(GameState.DemonTurn);
                        }

                    }
                }
            }
        }

        //DEMON TURN
        else if (GameManager.Instance.GameState == GameState.DemonTurn)
        {
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Demon)
                {
                    UnitManager.Instance.SetSelectedDemon((BaseDemon)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedDemon != null)
                    {
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();
                        if (result1)
                        {
                            PhotonNetwork.Destroy(enemy.gameObject);
                            //  MenuManager.Instance.ShowBattleWin();                  
                            UnitManager.Instance.SetSelectedDemon(null);
                            GameManager.Instance.ChangeState(GameState.HumanTurn);
                        }
                        else if (!result1)
                        {
                            PhotonNetwork.Destroy(UnitManager.Instance.SelectedDemon.gameObject);
                            // MenuManager.Instance.ShowBattleLoss();          
                            UnitManager.Instance.SetSelectedDemon(null);
                            GameManager.Instance.ChangeState(GameState.HumanTurn);
                        }
                    }
                }
            }
        }
    }

    //Dice 2vs2 Battles
    public bool DiceBattle()
    {

        bool result = false;

        int playerDiceResult = Random.Range(1, 6);

        int enemyDiceResult = Random.Range(1, 6);

        if (playerDiceResult > enemyDiceResult)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

}
