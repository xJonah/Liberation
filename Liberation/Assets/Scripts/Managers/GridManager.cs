using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random; 

public class GridManager : MonoBehaviour {
    [SerializeField] public int width, height;
    [SerializeField] private Tile grassTile, desertTile;
    [SerializeField] private Transform _cam;

    public GameObject orcPrefab;
    public static GridManager Instance;

    /*
    //GridManager instance created
    void Awake() {
        Instance = this;
    }
    */

    private void Start() {
        GenerateGrid();
    }
 
    public void GenerateGrid() {

        //Grass tile spawn
         for (int x = 0; x < width / 2; x++) {
            for (int y = 0; y < height; y++) {

                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"Tile {x} {y}";
                spawnedGrassTiles.Init(x, y);
             }
        }

        //Desert tile spawn
        for (int x = 8; x < width ; x++) {
            for (int y = 0; y < height; y++) {

                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"Tile {x} {y}";
                spawnedDesertTiles.Init(x, y);
             }
        }

        //Camera setup
        _cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 - 0.5f,-10);

    }

}