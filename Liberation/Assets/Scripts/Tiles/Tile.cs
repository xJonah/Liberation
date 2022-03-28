using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.Events;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using Random = UnityEngine.Random;

public abstract class Tile : MonoBehaviourPunCallbacks, IOnEventCallback
{
    #region Fields
    //Fields
    [SerializeField] protected SpriteRenderer _renderer;
    public GameObject _highlight;
    public BaseUnit OccupiedUnit;
    public string tileName;
    public UnityEvent<Tile> Clicked;
    public const byte BATTLE_WIN = 1, BATTLE_LOST = 2, CHANGE_STATE = 3;
    public bool Empty => OccupiedUnit == null;
    Dictionary<Vector2, Tile> tiles;
    ArrayList tileVectors;
    public GameState myTurn;

    #endregion 

    private void Start()
    {
        tiles = GridManager.Instance.GetTiles();
        tileVectors = GridManager.Instance.GetSurroundingTiles(this);
        myTurn = FindMyTurn();
    }

    #region Highlight Functions/Tile Colours
    // Allow override in order for Tiles to have checkerboard pattern or not
    public virtual void Init(int x, int y)
    {

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

    //Surrounding tiles no longer highlighted
    void OnMouseUp()
    {
        foreach (Vector2 v in tileVectors)
        {
            var tile = tiles[v];
            tile._highlight.SetActive(false);
        }
    }

    #endregion

    #region SetUnit Functionality
    // Grid square is occupied when a unit is on it
    public void SetUnit(BaseUnit unit) { //Task - Assign counter to each unit/tile as well

        if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            if (unit.OccupiedTile != null) {
                unit.OccupiedTile.OccupiedUnit = null;
            }
            //Only master client moves the initial position of spawned units
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
         else
        {
            //Remote players set positions
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
        
    }
    #endregion

    #region OnEvent


    //Master client can make move for other players

    private new void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private new void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {

        switch (photonEvent.Code)
        {
            case BATTLE_WIN:
                    
                object[] winData = (object[])photonEvent.CustomData;
                    
                //Get enemy + Destroy
                Vector2 enemyPosition1 = (Vector2)winData[0];
                Tile enemyTile1 = GridManager.Instance.GetTileValue(enemyPosition1);
                BaseUnit enemy1 = enemyTile1.OccupiedUnit;
                PhotonNetwork.Destroy(enemy1.gameObject);

                //Spawn my unit on enemy tile
                Vector2 selectedUnitPosition1 = (Vector2)winData[0];
                Tile selectedUnitTile1 = GridManager.Instance.GetTileValue(selectedUnitPosition1);
                BaseUnit selectedUnit1 = selectedUnitTile1.OccupiedUnit;
                UnitToSpawn(selectedUnit1, enemyTile1);
                    
                break;

            case BATTLE_LOST:

                object[] loseData = (object[])photonEvent.CustomData;

                //Get my unit + Destroy
                Vector2 selectedUnitPosition2 = (Vector2)loseData[0];
                Tile selectedUnitTile2 = GridManager.Instance.GetTileValue(selectedUnitPosition2);
                BaseUnit selectedUnit2 = selectedUnitTile2.OccupiedUnit;
                PhotonNetwork.Destroy(selectedUnit2.gameObject);

                //Spawn enemy on my unit's tile
                Vector2 enemyPosition2 = (Vector2)loseData[1];
                Tile enemyTile2 = GridManager.Instance.GetTileValue(enemyPosition2);
                BaseUnit enemy2 = enemyTile2.OccupiedUnit;
                UnitToSpawn(enemy2, selectedUnitTile2);

                break;

            case CHANGE_STATE:

                int[] stateData = (int[])photonEvent.CustomData;
                int stateID = (int)stateData[0];
                GameState turn = GameManager.Instance.GetTurnStateByInteger(stateID);
                GameManager.Instance.ChangeState(turn);
                    
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(photonEvent.Code), photonEvent.Code, null);

            
        }
    }

    #endregion

    #region Main Functionality
    
    private void OnMouseDown()
    {
        //Store each player's unique turn linked to GameState

        //Game has ended
        if (GameManager.Instance.GameState == GameState.EndGame)
        {
            Debug.Log("Game has ended");
            return;
        }
        //Not my turn
        else if (myTurn != GameManager.Instance.GameState)
        {
            MenuManager.Instance.ShowNotYourTurnText();
            return;
        }

        #region Human Turn

        //HUMAN TURN
        if (GameManager.Instance.GameState == GameState.HumanTurn && myTurn == GameState.HumanTurn)
        {

            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Human)
                {
                    //Set selected Human
                    UnitManager.Instance.SetSelectedHuman((BaseHuman)OccupiedUnit);

                    //Highlight surrounding enemy tiles
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
                        //Select enemy
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();

                        //Dice win
                        if (result1)
                        {
                            MenuManager.Instance.ShowWinText();

                            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.Destroy(enemy.gameObject);
                                UnitManager.Instance.SpawnNewHuman(enemy.OccupiedTile);
                                UnitManager.Instance.SetSelectedHuman(null);
                                GameManager.Instance.ChangeState(GameState.OrcTurn);
                            }
                            else
                            {
                                //Event sends tile vector positions of selected unit and enemy
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                object[] battleContent = new object[] { GridManager.Instance.GetTileVector(enemy.OccupiedTile), GridManager.Instance.GetTileVector(UnitManager.Instance.SelectedHuman.OccupiedTile) };

                                PhotonNetwork.RaiseEvent(
                                    BATTLE_WIN,
                                    battleContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );

                                UnitManager.Instance.SetSelectedHuman(null);

                                int[] stateContent = new int[] { 2 };

                                //Event to change state

                                PhotonNetwork.RaiseEvent(
                                    CHANGE_STATE,
                                    stateContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );
                            }
                        }
                        //Dice lost
                        else if (!result1)
                        {
                            MenuManager.Instance.ShowLoseText();

                            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.Destroy(UnitManager.Instance.SelectedHuman.gameObject);
                                UnitManager.Instance.SelectedHuman.OccupiedTile.UnitToSpawn(UnitManager.Instance.SelectedHuman, enemy.OccupiedTile);
                                UnitManager.Instance.SetSelectedHuman(null);
                                GameManager.Instance.ChangeState(GameState.OrcTurn);

                            }
                            else
                            {
                                //Event sends tile vector positions of selected unit and enemy
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                object[] battleContent = new object[] { GridManager.Instance.GetTileVector(UnitManager.Instance.SelectedHuman.OccupiedTile), GridManager.Instance.GetTileVector(enemy.OccupiedTile) };

                                PhotonNetwork.RaiseEvent(
                                    BATTLE_LOST,
                                    battleContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );

                                UnitManager.Instance.SetSelectedHuman(null);

                                int[] stateContent = new int[] { 2 };

                                //Event to change state

                                PhotonNetwork.RaiseEvent(
                                    CHANGE_STATE,
                                    stateContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );
                            }
                        }
                    }
                }
            }
        }
        #endregion 

        #region OrcTurn

        if (GameManager.Instance.GameState == GameState.OrcTurn && myTurn == GameState.OrcTurn)
        {

            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Orc)
                {
                    //Set selected Orc
                    UnitManager.Instance.SetSelectedOrc((BaseOrc)OccupiedUnit);

                    //Highlight surrounding enemy tiles
                    foreach (Vector2 v in tileVectors)
                    {
                        var tile = tiles[v];
                        if (tile.OccupiedUnit.Faction != Faction.Orc)
                        {
                            tile._highlight.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (UnitManager.Instance.SelectedOrc != null)
                    {
                        var enemy = OccupiedUnit;
                        var result1 = DiceBattle();

                        //Dice win
                        if (result1)
                        {

                            MenuManager.Instance.ShowWinText();

                            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.Destroy(enemy.gameObject);
                                UnitManager.Instance.SpawnNewOrc(enemy.OccupiedTile);
                                UnitManager.Instance.SetSelectedOrc(null);
                                GameManager.Instance.ChangeState(GameState.HumanTurn);
                            }
                            else
                            {
                                //Event sends tile vector positions of selected unit and enemy
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                object[] battleContent = new object[] { GridManager.Instance.GetTileVector(enemy.OccupiedTile), GridManager.Instance.GetTileVector(UnitManager.Instance.SelectedOrc.OccupiedTile) };

                                PhotonNetwork.RaiseEvent(
                                    BATTLE_WIN,
                                    battleContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );

                                UnitManager.Instance.SetSelectedOrc(null);

                                int[] stateContent = new int[] { 1 };

                                //Event to change state
                                PhotonNetwork.RaiseEvent(
                                    CHANGE_STATE,
                                    stateContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );
                            }
                        }
                        //Dice loss
                        else if (!result1)
                        {
                            MenuManager.Instance.ShowLoseText();

                            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.Destroy(UnitManager.Instance.SelectedOrc.gameObject);
                                UnitManager.Instance.SelectedHuman.OccupiedTile.UnitToSpawn(UnitManager.Instance.SelectedOrc, enemy.OccupiedTile);
                                UnitManager.Instance.SetSelectedOrc(null);
                                GameManager.Instance.ChangeState(GameState.HumanTurn);

                            }
                            else
                            {
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                object[] battleContent = new object[] { GridManager.Instance.GetTileVector(UnitManager.Instance.SelectedOrc.OccupiedTile), GridManager.Instance.GetTileVector(enemy.OccupiedTile) };

                                //Event sends tile vector positions of selected unit and enemy

                                PhotonNetwork.RaiseEvent(
                                    BATTLE_LOST,
                                    battleContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );

                                UnitManager.Instance.SetSelectedOrc(null);

                                int[] stateContent = new int[] { 1 };

                                //Event to change state 

                                PhotonNetwork.RaiseEvent(
                                    CHANGE_STATE,
                                    stateContent,
                                    raiseEventOptions,
                                    SendOptions.SendReliable
                                    );
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    #endregion

    #region Functionality Methods
    //Dice Battle
    public bool DiceBattle() {

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

    public void UnitToSpawn(BaseUnit unit, Tile tileToSpawn)
    {
        if (unit.Faction == Faction.Human)
        {
            UnitManager.Instance.SpawnNewHuman(tileToSpawn);
        }
        else if (unit.Faction == Faction.Orc)
        {
            UnitManager.Instance.SpawnNewOrc(tileToSpawn);
        }
        else if (unit.Faction == Faction.Elf)
        {
            UnitManager.Instance.SpawnNewElf(tileToSpawn);
        }
        else if (unit.Faction == Faction.Demon)
        {
            UnitManager.Instance.SpawnNewDemon(tileToSpawn);
        }
        else if (unit.Faction == Faction.Dwarf)
        {
            UnitManager.Instance.SpawnNewDwarf(tileToSpawn);
        }
        else
        {
            Debug.Log("Invalid Faction");
        }
    }


    #endregion

    #region FindTurn

    public GameState FindMyTurn()
    {

        if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(1))
        {
            myTurn = GameState.HumanTurn;
            return myTurn;
        }
        else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(2))
        {
            myTurn = GameState.OrcTurn;
            return myTurn;
        }
        else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(3))
        {
            myTurn = GameState.ElfTurn;
            return myTurn;
        }
        else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(4))
        {
            myTurn = GameState.DemonTurn;
            return myTurn;
        }
        else if (PhotonNetwork.LocalPlayer == GameManager.Instance.GetPlayers(5))
        {
            myTurn = GameState.DwarfTurn;
            return myTurn;
        }
        else
        {
            return GameState.EndGame;
        }
    }

    #endregion
}

