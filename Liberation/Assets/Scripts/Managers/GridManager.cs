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

    void Start() {
        GenerateGrid();
    }
 
    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
         for (int x = 0; x < _width / 2; x++) {
            for (int y = 0; y < _height; y++) {

                var spawnedGrassTiles = Instantiate(_grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"Tile {x} {y}";
               
                spawnedGrassTiles.Init(x, y);
                _tiles[new Vector2(x, y)] = spawnedGrassTiles;

             }
        }

        for (int x = 8; x < _width ; x++) {
            for (int y = 0; y < _height; y++) {

                var spawnedDesertTiles = Instantiate(_desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"Tile {x} {y}";

                spawnedDesertTiles.Init(x, y);
               _tiles[new Vector2(x, y)] = spawnedDesertTiles;

             }
        }
        var spawnHuman = Instantiate(humanPrefab, new Vector3(_width / 4, _height / 2, -1), Quaternion.identity);
        var spawnOrc = Instantiate(orcPrefab, new Vector3(_width - (_width / 4), _height / 2, -1), Quaternion.identity);

        _cam.transform.position = new Vector3((float)_width/2 -0.5f, (float)_height / 2 - 0.5f,-10);
    }

     public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    } 
}