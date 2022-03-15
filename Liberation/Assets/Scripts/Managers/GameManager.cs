using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
//, IPunObservable, IOnEventCallback
{
    public static GameManager Instance;
    public GameState GameState;

    // Game manager Instance
    void Awake()
    {
        Instance = this;
    }

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
                break; 

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

    #region PhotonEvents

    //public void OnEvent(EventData photonEvent) {

    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

    //}

    //private void DetectWinner() {

    //}

    #endregion

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



