using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random; 
using System.Linq;

public class GridManager : MonoBehaviour 
{
    public int width, height;
    public Tile grassTile, desertTile, mountainTile, oceanTile, snowTile;
    public Transform cam;
    public static GridManager Instance;
    private Dictionary<Vector2, Tile> tiles;

    void Awake() {
        Instance = this;
    }
 
    public void GenerateGrid() {

        // Initialise Tile Dictionary
        tiles = new Dictionary<Vector2, Tile>();

        // Grass tile spawn (Top left)
        for (int x = 0; x < width / 2; x++) {
             for (int y = 6; y < height; y++) {
                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"GrassTile {x} {y}";
                spawnedGrassTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedGrassTiles;
             }
        }

        for (int x = 0; x < 4; x++) {
            for (int y = height / 2; y < height - 2; y++) {
                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"GrassTile {x} {y}";
                spawnedGrassTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedGrassTiles;
            }
        }

        // Desert tile spawn (Top right)
        for (int x = width / 2; x < width; x++) {
            for (int y = 6; y < height; y++) {
                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"DesertTile {x} {y}";
                spawnedDesertTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedDesertTiles;
             }
        }

        for (int x = 8; x < width; x++) {
            for (int y = 4; y < height - 2; y++) {
                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"DesertTile {x} {y}";
                spawnedDesertTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedDesertTiles;
             }
        }

        // Mountain tile spawn (Bottom left)
        for (int x = 0; x < width / 2; x++) {
            for (int y = 0; y < 2; y++) {
                var spawnedMountainTiles = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                spawnedMountainTiles.name = $"MountainTile {x} {y}";
                spawnedMountainTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedMountainTiles;
             }
        }

        for (int x = 0; x < 4; x++) {
            for (int y = 2; y < height / 2; y++) {
                var spawnedMountainTiles = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                spawnedMountainTiles.name = $"MountainTile {x} {y}";
                spawnedMountainTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedMountainTiles;
             }
        }        

        // Ocean tile spawn (Bottom right)
        for (int x = width / 2; x < width; x++) {
            for (int y = 0; y < 2; y++) {
                var spawnedOceanTiles = Instantiate(oceanTile, new Vector3(x, y), Quaternion.identity);
                spawnedOceanTiles.name = $"OceanTile {x} {y}";
                spawnedOceanTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedOceanTiles;
             }
        }

        for (int x = 8; x < width; x++) {
            for (int y = 2; y < height / 2; y++) {
                var spawnedOceanTiles = Instantiate(oceanTile, new Vector3(x, y), Quaternion.identity);
                spawnedOceanTiles.name = $"OceanTile {x} {y}";
                spawnedOceanTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedOceanTiles;
             }
        }

        // Snow tile spawn (Middle)
        for (int x = 4; x < width - 4; x++) {
            for (int y = 2; y < height - 2 ; y++) {        
                var spawnedSnowTiles = Instantiate(snowTile, new Vector3(x, y), Quaternion.identity);
                spawnedSnowTiles.name = $"SnowTile {x} {y}";
                spawnedSnowTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedSnowTiles;
             }
        }

        //Camera setup
        cam.transform.position = new Vector3((float) width / 2 - 0.5f, (float) height / 2 - 0.5f, -10);

        //Switch to next game state
        GameManager.Instance.ChangeState(GameState.SpawnUnits);
    }

    // Get random spawn tile that is empty
    public Tile GetSpawnTile() {
        return tiles.Where(t => t.Key.x <= width && t.Value.Empty).OrderBy(tiles => Random.value).First().Value;
    }

    //Function to retrieve dictionary of all tiles
    public Dictionary<Vector2, Tile> GetTiles() {
        return tiles;
    }

    //Function to retrieve the vectors of surrounding tiles in an arraylist
    public ArrayList GetSurroundingTiles(Tile tile) {
        Vector2 surroudingTileVector = tiles.FirstOrDefault(xy => xy.Value == tile).Key;
        
        Vector2 tileLeft = new Vector2(surroudingTileVector.x - 1, surroudingTileVector.y);
        Vector2 tileRight = new Vector2(surroudingTileVector.x + 1, surroudingTileVector.y);
        Vector2 tileUp = new Vector2(surroudingTileVector.x, surroudingTileVector.y + 1);
        Vector2 tileDown = new Vector2(surroudingTileVector.x, surroudingTileVector.y - 1);

        ArrayList tileVectors = new ArrayList();
        tileVectors.Add(tileLeft);
        tileVectors.Add(tileRight);
        tileVectors.Add(tileUp);
        tileVectors.Add(tileDown);

        return tileVectors;
    }

    // Return the numbers of territories
    public int GetTileArea() {
        return width * height;
    }

}