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

    // Load content of Units folder inside Resources folder
    void Awake() {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    // Get the area of the grid from gridmanager
    public void GetTileAmount() {
        width = GridManager.Instance.getWidth();
        height = GridManager.Instance.getHeight();
        tileArea = width * height;
    }

    // Spawn units evenly on grid between the number of clans
    public void SpawnUnits() {
        // Divide grid up using the amount of players in the game
        // var humanCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount; 
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

    // Grab random prefab model of a certain faction
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
}
