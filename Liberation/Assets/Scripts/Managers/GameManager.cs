using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Instances/Constants 
    
    //Fields
    public static GameManager Instance;
    public GameState GameState;
    private int humanCount, orcCount, elfCount, demonCount, dwarfCount = 0;
    private string name1, name2, name3, name4, name5;
    private Player player1, player2, player3, player4, player5;

    //GameManager Instance
    void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Start

    void Start()
    {
        // Start first GameState
        ChangeState(GameState.GenerateGrid);

    }



    #endregion

    #region GameState

    // Function to change game state
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
                //Winner text
                string winner = DetermineWinner();
                MenuManager.Instance.ShowWinnerText(winner);
                break; 

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    //Calculate a gamestate by integer. Used for Raise Event
    public GameState GetTurnStateByInteger(int state)
    {
        if (state == 1)
        {
            return GameState.HumanTurn;
        }
        else if (state == 1)
        {
            return GameState.OrcTurn;
        }
        else if (state == 1)
        {
            return GameState.ElfTurn;
        }
        else if (state == 1)
        {
            return GameState.DemonTurn;
        }
        else if (state == 1)
        {
            return GameState.DwarfTurn;
        }
        else
        {
            return GameState.EndGame;
        }
    }

    #endregion

    #region GameState Synchronisation



    //Sync Gamestate
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

    //Determine Winner through how many tiles a faction owns. Return string being the nickname of the winner.
    public string DetermineWinner() {

        if (PhotonNetwork.CurrentRoom == null)
        {
            return " ";
        }


        List<Tile> tiles = GridManager.Instance.GetTileList();

        foreach (Tile t in tiles) {

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

    #region GetPlayers

    //Find opponents from array
    public Player GetPlayers(int player)
    {

        if (PhotonNetwork.IsConnected)
        {
            ArrayList players = new ArrayList();


            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                players.Add(p);
            }

            if (player == 1)
            {
                player1 = (Player)players[0];
                return player1;
            }
            else if (player == 2)
            {
                player2 = (Player)players[1];
                return player2;
            }
            else if (player == 3)
            {
                player3 = (Player)players[2];
                return player3;
            }
            else if (player == 4)
            {
                player4 = (Player)players[3];
                return player4;
            }
            else if (player == 5)
            {
                player5 = (Player)players[4];
                return player5;
            }
            else
            {
                Debug.Log("Invalid Opponent Index");
                return null;
            }
        }
        else
        {
            return null;
        }
        
    }

    

    #endregion

 
}

#region GameState Enum

//Declare gamestates
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

#endregion




