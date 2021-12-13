using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System.Linq;
using Photon.Pun;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    private int width, height, tileArea;

    //Load content of Units folder inside Resources folder
    void Awake() {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    //Get the area of the grid from gridmanager
    public void GetTileAmount() {
        width = GridManager.Instance.getWidth();
        height = GridManager.Instance.getHeight();
        tileArea = width * height;
    }

    //Spawn units evenly on grid between the number of clans
    public void SpawnUnits() {
        //Divide grid up using the amount of players in the game
        //var humanCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount; 
        var spawnCount = 8;


        for(int i = 0; i < spawnCount; i++) {

            var randomHumanSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomHumanPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedHuman = Instantiate(randomHumanPrefab);
            randomHumanSpawnTile.SetUnit(spawnedHuman);

            var randomOrcSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomOrcPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
            var spawnedOrc = Instantiate(randomOrcPrefab);
            randomOrcSpawnTile.SetUnit(spawnedOrc);           
        }
        GameManager.Instance.ChangeState(GameState.HumanTurn);
    }

    //Grab random prefab model of a certain faction
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
=======

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    private List<ScriptableUnit> units;
    public BaseHuman selectedHuman;


    void Awake()
    {
        instance = this;
        units.Resouruces.LoadAll<ScriptableUnit>("Unit").ToList();
    }
    public void SpawnHuman()
    {
        var humanCount = 1;
        for (int i = 0; i < humanCount; i++) { 
            var randomPreFab = GetRandomUnit<BaseHuman>(Faction.Human);
        var spawnedHuman = Instantiate(randomPrefab);
        var randomSpawnTile = GridManager.Instane.GetHeroSpawnTile(); 
        spawnedHuman.transform.position = randomSpawnTile.transform.position;
        randomSpawnTile.OccupiedUnit = spawnedHuman;
        }
        GameManager.Instance.changeState(spawnOrcs);
    }


    public void SpawnHuman()
    {
        var humanCount = 1;
        for (int i = 0; i < humanCount; i++)
        {
            var randomPreFab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedOrcs = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instane.GetOrcsSpawnTile();
            spawnedHuman.transform.position = randomSpawnTile.transform.position;
            randomSpawnTile.OccupiedUnit = spawnedHuman;
        }
        GameManager.Instance.changeState(spawnedHuman);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

>>>>>>> a1c4b55bb0af492a22e836d03362aa9d6dc85439
}
