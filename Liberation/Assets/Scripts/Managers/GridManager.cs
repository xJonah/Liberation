using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random; 

public class GridManager : MonoBehaviour {
    [SerializeField] public int width, height;
    [SerializeField] private Tile grassTile, desertTile, mountainTile, oceanTile, snowTile;
    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    private void Start() {
        GenerateGrid();
    }
 
    public void GenerateGrid() {

        tiles = new Dictionary<Vector2, Tile>();

        //Grass tile spawn (Top left)
         for (int x = 0; x < width / 2; x++) {
            for (int y = 5; y < height; y++) {

                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"GrassTile {x} {y}";
                spawnedGrassTiles.Init(x, y);
             }
        }

        //Desert tile spawn (Top right)
        for (int x = 8; x < width; x++) {
            for (int y = 5; y < height; y++) {

                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"DesertTile {x} {y}";
                spawnedDesertTiles.Init(x, y);
             }
        }

        //Mountain tile spawn (Bottom left)
        for (int x = 0; x < width / 2; x++) {
            for (int y = 0; y < height / 2; y++) {
                var spawnedMountainTiles = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                spawnedMountainTiles.name = $"MountainTile {x} {y}";
                spawnedMountainTiles.Init(x, y);
             }
        }

        //Ocean tile spawn (Bottom right)
        for (int x = 8; x < width; x++) {
            for (int y = 0; y < height / 2; y++) {
                var spawnedOceanTiles = Instantiate(oceanTile, new Vector3(x, y), Quaternion.identity);
                spawnedOceanTiles.name = $"OceanTile {x} {y}";
                spawnedOceanTiles.Init(x, y);
             }
        }

                //Snow tile spawn (Middle)
        for (int x = 5; x < (width / 2 + 3); x++) {
            for (int y = 3; y < (height / 2 + 2) ; y++) {
                var spawnedSnowTiles = Instantiate(snowTile, new Vector3(x, y), Quaternion.identity);
                spawnedSnowTiles.name = $"SnowTile {x} {y}";
                spawnedSnowTiles.Init(x, y);
                //Need to find a way to delete the Tiles under the snow Tiles
             }
        }

        //Camera setup
        cam.transform.position = new Vector3((float) width / 2 - 0.5f, (float) height / 2 - 0.5f, -10);

    }

}