using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    //Game manager Instance
    void Awake()
    {
        Instance = this;
    }

    //Start first GameState
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    //Game state logic and switch function
    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;

            case GameState.HumanSpawn:
                UnitManager.Instance.SpawnHumans();
                break;

            case GameState.OrcSpawn:
                UnitManager.Instance.SpawnOrcs();
                break;

            case GameState.HumanTurn:
                break;

            case GameState.OrcTurn:
                break;                                

            //etc..

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

//'Flow' of game
public enum GameState
{
    GenerateGrid,
    HumanSpawn,
    OrcSpawn,
    HumanTurn,
    OrcTurn
}
