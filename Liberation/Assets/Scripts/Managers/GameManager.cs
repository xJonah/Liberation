using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable, IOnEventCallback
{
    #region Instances/Constants 
    
    public static GameManager Instance;
    public GameState GameState, myTurn, winner;
    public const int EVENT_MOVE = 1;
    private int humanCount, orcCount, elfCount, demonCount, dwarfCount = 0;
    private string name1, name2, name3, name4, name5;
    private Player player1, player2, player3, player4, player5;

    
    void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Turns

    // Start first GameState + Turns
    void Start()
    {
        ChangeState(GameState.GenerateGrid);

        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.LocalPlayer == GetOpponents(1))
            {
                myTurn = GameState.HumanTurn;
            }
            else if (PhotonNetwork.LocalPlayer == GetOpponents(2))
            {
                myTurn = GameState.OrcTurn;
            }
            else if (PhotonNetwork.LocalPlayer == GetOpponents(3))
            {
                myTurn = GameState.ElfTurn;
            }
            else if (PhotonNetwork.LocalPlayer == GetOpponents(4))
            {
                myTurn = GameState.DemonTurn;
            }
            else if (PhotonNetwork.LocalPlayer == GetOpponents(5))
            {
                myTurn = GameState.DwarfTurn;
            }
        }
    }

    public bool IsMyTurn()
    {
        if (PhotonNetwork.LocalPlayer == GetOpponents(1) && myTurn == GameState.HumanTurn)
        {
            return true;
        } 
        else if (PhotonNetwork.LocalPlayer == GetOpponents(2) && myTurn == GameState.OrcTurn)
        {
            return true;
        }
        else if (PhotonNetwork.LocalPlayer == GetOpponents(3) && myTurn == GameState.ElfTurn)
        {
            return true;
        }
        else if (PhotonNetwork.LocalPlayer == GetOpponents(4) && myTurn == GameState.DemonTurn)
        {
            return true;
        }
        else if (PhotonNetwork.LocalPlayer == GetOpponents(5) && myTurn == GameState.DwarfTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region GameState

    // Game state logic and switch function
    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;

            case GameState.SpawnUnits:
                UnitManager.Instance.GetTileAmount();
                UnitManager.Instance.SpawnUnits();
                break;

            case GameState.HumanTurn:
                break;

            case GameState.OrcTurn:
                break;                                

            case GameState.ElfTurn:
                break;
                
            case GameState.DemonTurn:
                break;                                                                

            case GameState.DwarfTurn:
                break;            

            case GameState.EndGame:
            string winner = DetermineWinner();
            //Winner text
                break; 

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    #endregion

    #region PhotonEvents/Synchronisation

    
    public void OnEvent(EventData photonEvent) {
        if (photonView.IsMine)
        {
            switch (photonEvent.Code)
            {
                case EVENT_MOVE:
                object[] data = (object[]) photonEvent.CustomData;
                
                
                break;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(this.GameState);
        }
        else
        {
            this.GameState = (GameState)stream.ReceiveNext();
            ChangeState(this.GameState);
        }
    }
    
    

    #endregion

    #region Winner

    //Determine Winner
    public string DetermineWinner() {

        if (PhotonNetwork.CurrentRoom == null)
        {
            return " ";
        }

        Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
        tiles = GridManager.Instance.GetTiles();
        var tileValues = tiles.Values;

        foreach (Tile t in tileValues) {

            if (t.OccupiedUnit.Faction == Faction.Human) {
                humanCount++;
            } else if (t.OccupiedUnit.Faction == Faction.Orc) {
                orcCount++;
            } else if (t.OccupiedUnit.Faction == Faction.Elf) {
                elfCount++;
            } else if (t.OccupiedUnit.Faction == Faction.Demon) {
                demonCount++;
            } else {
                dwarfCount++;
            }
        }

        ArrayList names = new ArrayList();

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList) {
            names.Add(p.NickName);
        }

        name1 = (string)names[0];
        name2 = (string)names[1];
        name3 = (string)names[2];
        name4 = (string)names[3];

        if (PhotonNetwork.CurrentRoom.PlayerCount > 4) {
            name5 = (string)names[4];
        }

        if (humanCount > orcCount && humanCount > elfCount && humanCount > demonCount && humanCount > dwarfCount) {
            return name1;
        } else if (orcCount > humanCount && orcCount > elfCount && orcCount > demonCount && orcCount > dwarfCount) {
            return name2;
        } else if (elfCount > humanCount && elfCount > orcCount && elfCount > demonCount && elfCount > dwarfCount) {
            return name3;
        } else if (demonCount > humanCount && demonCount > orcCount && demonCount > elfCount && demonCount > dwarfCount) {
            return name4;
        } else if (dwarfCount > humanCount && dwarfCount > orcCount && dwarfCount > elfCount && dwarfCount > demonCount) {
            return name5;
        } else {
            return "Game ended in a draw";
        }
    }
    #endregion

    #region GetOpponents

    public Player GetOpponents(int player)
    {
        ArrayList opponents = new ArrayList();

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            opponents.Add(p);
        }

        if (player == 1)
        {
            player1 = (Player)opponents[0];
            return player1;
        }
        else if (player == 2)
        {
            player2 = (Player)opponents[1];
            return player2;
        }
        else if (player == 3)
        {
            player3 = (Player)opponents[2];
            return player3;
        }
         else if (player == 4)
        {
            player4 = (Player)opponents[3];
            return player4;
        }
         else if (player == 5)
        {
            player5 = (Player)opponents[4];
            return player5;
        }
        else
        {
            Debug.Log("Invalid Opponent Index");
            return null;
        }
    }

    #endregion

}


// 'Flow' of game
public enum GameState
{
    GenerateGrid,
    SpawnUnits,
    HumanTurn,
    OrcTurn,
    ElfTurn,
    DemonTurn,
    DwarfTurn,
    EndGame

}




