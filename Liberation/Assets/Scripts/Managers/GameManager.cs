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
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

//'Flow' of game
public enum GameState
{
    GenerateGrid,
    HumanTurn,
    SpawnHumans,
    OrcTurn,
    SpawnOrcs
}
