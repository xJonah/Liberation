using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
