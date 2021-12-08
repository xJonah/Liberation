using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    public int width, height, tileArea;

    //Load content of Units folder inside Resources folder
    void Awake() {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void getTileAmount() {
        //width = GridManager.Instance.getWidth();
        //height = GridManager.Instance.getHeight();
        //tileArea = width * height;
    }

    public void SpawnHumans() {
        //var humanCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount;
        //var humanCount = tileArea / 4;
        var humanCount = 4;

        for(int i = 0; i < humanCount; i++) {
            var randomPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedHuman = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetSpawnTile();

            randomSpawnTile.SetUnit(spawnedHuman);
        }

        GameManager.Instance.ChangeState(GameState.OrcSpawn);
    }

    public void SpawnOrcs() {

        var orcCount = 4;

        for(int i = 0; i < orcCount; i++) {
            var randomPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
            var spawnedOrc = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetSpawnTile();

            randomSpawnTile.SetUnit(spawnedOrc);
        }

        GameManager.Instance.ChangeState(GameState.HumanTurn);
    }

    //Grab random prefab model of a certain faction
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
}
