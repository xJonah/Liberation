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

        int spawnCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount;
       
        SpawnHumans(spawnCount);
        SpawnOrcs(spawnCount);

        GameManager.Instance.ChangeState(GameState.HumanTurn);
        
    }

    public void SpawnHumans(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

                var randomHumanSpawnTile = GridManager.Instance.GetSpawnTileVector();
                var randomHumanPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
                var spawnedHuman = PhotonNetwork.Instantiate(randomHumanPrefab.name, randomHumanSpawnTile, Quaternion.identity);
                //randomHumanSpawnTile.SetUnit(spawnedHuman);
            }
    }

    public void SpawnOrcs(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

                var randomOrcSpawnTile = GridManager.Instance.GetSpawnTileVector();
                var randomOrcPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
                var spawnedOrc = PhotonNetwork.Instantiate(randomOrcPrefab.name, randomOrcSpawnTile, Quaternion.identity);
                //randomOrcSpawnTile.SetUnit(spawnedOrc);           
            }
    }

    // Grab random prefab model of a certain faction
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
}
