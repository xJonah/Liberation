using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable, IOnEventCallback
{
    #region Instances/Constants 
    
    public static GameManager Instance;
    public GameState GameState;
    public const int EVENT_MOVE = 1;
    private int humanCount, orcCount, elfCount, demonCount, dwarfCount = 0;
    private string name1, name2, name3, name4, name5;

    
    void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Gamestates

    // Start first GameState
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }


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

    private void TilePlayed(Tile tile) {
        
    }

    public void OnEvent(EventData photonEvent) {
        if (photonView.IsMine)
        {
            switch (photonEvent.Code)
            {
                case EVENT_MOVE:
                object[] data = (object[]) photonEvent.CustomData;
                object tile = data[0];
                object unit = data[1];
                
                break;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {

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



