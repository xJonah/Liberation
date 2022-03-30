using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random; 
using System.Linq;

public class GridManager : MonoBehaviour 
{

    //Fields
    public int width, height;
    public Tile grassTile, desertTile, mountainTile, oceanTile, snowTile;
    public Transform cam;
    public static GridManager Instance;
    private Dictionary<Vector2, Tile> tiles;
    private List<Tile> tileList, grassTiles, desertTiles, mountainTiles, oceanTiles, snowTiles;


    //GridManager instance
    void Awake() {
        Instance = this;
    }
 
    //Spawn map
    public void GenerateGrid() {

        // Initialise Tile Dictionary
        tiles = new Dictionary<Vector2, Tile>();
        tileList = new List<Tile>();
        grassTiles = new List<Tile>(); 
        desertTiles = new List<Tile>(); 
        mountainTiles = new List<Tile>(); 
        oceanTiles = new List<Tile>(); 
        snowTiles = new List<Tile>(); 

        // Grass tile spawn (Top left - higher)
        for (int y = 6; y < height; y++)
        {
            for (int x = 0; x < width / 2; x++) {      
                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"GrassTile {x} {y}";
                spawnedGrassTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedGrassTiles;
                tileList.Add(spawnedGrassTiles);
                grassTiles.Add(spawnedGrassTiles);
             }
        }

        // Desert tile spawn (Top right - higher)
        for (int y = 6; y < height; y++)
        {
            for (int x = width / 2; x < width; x++)
        {
                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"DesertTile {x} {y}";
                spawnedDesertTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedDesertTiles;
                tileList.Add(spawnedDesertTiles);
                desertTiles.Add(spawnedDesertTiles);
            }
        }


        // Mountain tile spawn (Bottom left - lower)
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < width / 2; x++)
        {
                var spawnedMountainTiles = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                spawnedMountainTiles.name = $"MountainTile {x} {y}";
                spawnedMountainTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedMountainTiles;
                tileList.Add(spawnedMountainTiles);
                mountainTiles.Add(spawnedMountainTiles);
            }
        }

        // Ocean tile spawn (Bottom right - lower)
        for (int y = 0; y < 2; y++)
        {
            for (int x = width / 2; x < width; x++)
        {
                var spawnedOceanTiles = Instantiate(oceanTile, new Vector3(x, y), Quaternion.identity);
                spawnedOceanTiles.name = $"OceanTile {x} {y}";
                spawnedOceanTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedOceanTiles;
                tileList.Add(spawnedOceanTiles);
                oceanTiles.Add(spawnedOceanTiles);
            }
        }

        // Snow tile spawn (Middle)
        for (int y = 2; y < height - 2; y++)
        {
            for (int x = 4; x < width - 4; x++)
        {
                var spawnedSnowTiles = Instantiate(snowTile, new Vector3(x, y), Quaternion.identity);
                spawnedSnowTiles.name = $"SnowTile {x} {y}";
                spawnedSnowTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedSnowTiles;
                tileList.Add(spawnedSnowTiles);
                snowTiles.Add(spawnedSnowTiles);
            }
        }


        // Grass tile spawn (Top left - lower)
        for (int y = height / 2; y < height - 2; y++)
        {
            for (int x = 0; x < 4; x++)
        {
                var spawnedGrassTiles = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedGrassTiles.name = $"GrassTile {x} {y}";
                spawnedGrassTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedGrassTiles;
                tileList.Add(spawnedGrassTiles);
                grassTiles.Add(spawnedGrassTiles);
            }
        }

        // Desert tile spawn (Top right - lower)
        for (int y = 4; y < height - 2; y++)
        {
            for (int x = 8; x < width; x++) {
                var spawnedDesertTiles = Instantiate(desertTile, new Vector3(x, y), Quaternion.identity);
                spawnedDesertTiles.name = $"DesertTile {x} {y}";
                spawnedDesertTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedDesertTiles;
                tileList.Add(spawnedDesertTiles);
                desertTiles.Add(spawnedDesertTiles);
            }
        }


        // Mountain tile spawn (Bottom left - higher)
        for (int y = 2; y < height / 2; y++)
        {
            for (int x = 0; x < 4; x++) {
                var spawnedMountainTiles = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                spawnedMountainTiles.name = $"MountainTile {x} {y}";
                spawnedMountainTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedMountainTiles;
                tileList.Add(spawnedMountainTiles);
                mountainTiles.Add(spawnedMountainTiles);
            }
        }


        // Ocean tile spawn (Bottom right - higher)
        for (int y = 2; y < height / 2; y++)
        {
            for (int x = 8; x < width; x++) {
                var spawnedOceanTiles = Instantiate(oceanTile, new Vector3(x, y), Quaternion.identity);
                spawnedOceanTiles.name = $"OceanTile {x} {y}";
                spawnedOceanTiles.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedOceanTiles;
                tileList.Add(spawnedOceanTiles);
                oceanTiles.Add(spawnedOceanTiles);
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

    //Return a tile's vector position
    public Vector2 GetTileVector(Tile tile)
    {
        var value = tiles.FirstOrDefault(x => x.Value == tile).Key;
        return value;
    }

    //Return a list of tiles
    public List<Tile> GetTileList()
    {
        return tileList;
    }

    //Return list of grass tiles
    public List<Tile> GetGrassTiles()
    {
        return grassTiles;
    }

    //Return list of desert tiles
    public List<Tile> GetDesertTiles()
    {
        return desertTiles;
    }

    //Return list of mountain tiles
    public List<Tile> GetMountainTiles()
    {
        return mountainTiles;
    }

    //Return list of ocean tiles
    public List<Tile> GetOceanTiles()
    {
        return oceanTiles;
    }

    //Return list of snow tiles
    public List<Tile> GetSnowTiles()
    {
        return snowTiles;
    }

    //Return the tile in the position of a vector
    public Tile GetTileValue(Vector2 key)
    {
        return tiles[key];
    }

    //Function to retrieve the vectors of surrounding tiles in an arraylist
    public ArrayList GetSurroundingTiles(Tile tile) {
        Vector2 surroudingTileVector = tiles.FirstOrDefault(xy => xy.Value == tile).Key;
        
        Vector2 tileLeft = new Vector2(surroudingTileVector.x - 1, surroudingTileVector.y);
        Vector2 tileRight = new Vector2(surroudingTileVector.x + 1, surroudingTileVector.y);
        Vector2 tileUp = new Vector2(surroudingTileVector.x, surroudingTileVector.y + 1);
        Vector2 tileDown = new Vector2(surroudingTileVector.x, surroudingTileVector.y - 1);

        ArrayList tileVectors = new ArrayList
        {
            tileLeft,
            tileRight,
            tileUp,
            tileDown
        };

        return tileVectors;
    }

    // Return the numbers of territories
    public int GetTileArea() {
        return width * height;
    }

}