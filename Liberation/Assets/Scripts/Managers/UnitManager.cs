using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    private int width, height, tileArea;
    public BaseUnit selectedUnit;

    //Load content of Units folder inside Resources folder
    void Awake() {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void GetTileAmount() {
        width = GridManager.Instance.getWidth();
        height = GridManager.Instance.getHeight();
        tileArea = width * height;
    }

    public void SpawnHumans() {
        //var humanCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount;
        var humanCount = tileArea / 5;

        for(int i = 0; i < humanCount; i++) {
            var randomPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedHuman = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetSpawnTile();

            randomSpawnTile.SetUnit(spawnedHuman);
        }

        GameManager.Instance.ChangeState(GameState.OrcSpawn);
    }

    public void SpawnOrcs() {

        var orcCount = tileArea / 5;

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

    public void SetSelectedUnit(BaseUnit unit) {
        selectedUnit = unit;
    }
}
