using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random; 

public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _grassTile, _desertTile;
    [SerializeField] private Transform _cam;

    public GameObject humanPrefab;
    public GameObject orcPrefab;
    private Dictionary<Vector2, Tile> _tiles;
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
        _tiles = new Dictionary<Vector2, Tile>();

        //Grass tile spawn
         for (int x = 0; x < _width / 2; x++) {
            for (int y = 0; y < _height; y++) {

                var spawnedGrassTiles = Instantiate(_grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"Tile {x} {y}";
               
                spawnedGrassTiles.Init(x, y);
                _tiles[new Vector2(x, y)] = spawnedGrassTiles;

             }
        }

        //Desert tile spawn
        for (int x = 8; x < _width ; x++) {
            for (int y = 0; y < _height; y++) {

                var spawnedDesertTiles = Instantiate(_desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"Tile {x} {y}";

                spawnedDesertTiles.Init(x, y);
               _tiles[new Vector2(x, y)] = spawnedDesertTiles;

             }
        }
        
        var spawnHuman = Instantiate(humanPrefab, new Vector3(_width / 4, _height / 2, -1), Quaternion.identity);
        spawnHuman.name = $"Human 1";
        var spawnOrc = Instantiate(orcPrefab, new Vector3(_width - (_width / 4), _height / 2, -1), Quaternion.identity);
        spawnOrc.name = $"Orc 1";

        //Camera setup
        _cam.transform.position = new Vector3((float)_width/2 -0.5f, (float)_height / 2 - 0.5f,-10);

        //Active next game state
        //GameManager.Instance.ChangeState(GameState.HumanTurn);
    }

    //Get tile position from dictionary
     public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    } 

}