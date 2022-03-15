using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    private int width, height, tileArea;
    public BaseHuman SelectedHuman;
    public BaseOrc SelectedOrc;
    public BaseElf SelectedElf;
    public BaseDwarf SelectedDwarf;
    public BaseDemon SelectedDemon;
    public BaseUnit SelectedUnit;

    // Load content of Units folder inside Resources folder + hashtable setup
    void Awake() {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    // Get the area of the grid from gridmanager
    public void GetTileAmount() {
        tileArea = GridManager.Instance.GetTileArea();
    }

    // Spawn units evenly on grid between the number of clans
    public void SpawnUnits() {

        int spawnCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount;
        
        /*
            if (PhotonNetwork.CurrentRoom.PlayerCount == 4) {
                SpawnHumans(spawnCount);
                SpawnOrcs(spawnCount);
                SpawnDwarves(spawnCount);
                SpawnElves(spawnCount);
            } else if (PhotonNetwork.CurrentRoom.PlayerCount == 5) { */
                SpawnHumans(spawnCount);
                SpawnOrcs(spawnCount);
                //SpawnDwarves(spawnCount);
                //SpawnElves(spawnCount);
                //SpawnDemons(spawnCount);
            //}
        
        GameManager.Instance.ChangeState(GameState.HumanTurn);
        
    }

    // Function to spawn human units
    public void SpawnHumans(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {
          
            var randomHumanSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomHumanPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedHuman = PhotonNetwork.Instantiate(randomHumanPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedHumanUnit = spawnedHuman.GetComponent<BaseHuman>();
            
            randomHumanSpawnTile.SetUnit(spawnedHumanUnit); 

            //Task - Spawn counter to each unit/tile

        }
    }

    // Function to spawn orc units
    public void SpawnOrcs(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

            var randomOrcSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomOrcPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
            var spawnedOrc = PhotonNetwork.Instantiate(randomOrcPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedOrcUnit = spawnedOrc.GetComponent<BaseOrc>();
            randomOrcSpawnTile.SetUnit(spawnedOrcUnit); 
        }
    }

    // Function to spawn orc units
    public void SpawnElves(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

            var randomElfSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomElfPrefab = GetRandomUnit<BaseElf>(Faction.Elf);
            var spawnedElf = PhotonNetwork.Instantiate(randomElfPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedElfUnit = spawnedElf.GetComponent<BaseElf>();
            randomElfSpawnTile.SetUnit(spawnedElfUnit); 
        }
    }

    // Function to spawn orc units
    public void SpawnDwarves(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

            var randomDwarfSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomDwarfPrefab = GetRandomUnit<BaseDwarf>(Faction.Dwarf);
            var spawnedDwarf = PhotonNetwork.Instantiate(randomDwarfPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedDwarfUnit = spawnedDwarf.GetComponent<BaseOrc>();
            randomDwarfSpawnTile.SetUnit(spawnedDwarfUnit); 
        }
    }

    // Function to spawn orc units
    public void SpawnDemons(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {

            var randomDemonSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomDemonPrefab = GetRandomUnit<BaseDemon>(Faction.Demon);
            var spawnedDemon = PhotonNetwork.Instantiate(randomDemonPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedDemonUnit = spawnedDemon.GetComponent<BaseDemon>();
            randomDemonSpawnTile.SetUnit(spawnedDemonUnit); 
        }
    }

    public void SpawnNewHuman(Tile tile) {
        var randomHumanPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
        var spawnedHuman = PhotonNetwork.Instantiate(randomHumanPrefab.name, new Vector2(0,0), Quaternion.identity);
        var spawnedHumanUnit = spawnedHuman.GetComponent<BaseHuman>();

        tile.SetUnit(spawnedHumanUnit);
    }

    public void SpawnNewOrc(Tile tile) {
        var randomOrcPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
        var spawnedOrc = PhotonNetwork.Instantiate(randomOrcPrefab.name, new Vector2(0,0), Quaternion.identity);
        var spawnedOrcUnit = spawnedOrc.GetComponent<BaseOrc>();

        tile.SetUnit(spawnedOrcUnit);
    }

    public void SpawnNewElf(Tile tile) {
        var randomElfPrefab = GetRandomUnit<BaseElf>(Faction.Elf);
        var spawnedElf = PhotonNetwork.Instantiate(randomElfPrefab.name, new Vector2(0,0), Quaternion.identity);
        var spawnedElfUnit = spawnedElf.GetComponent<BaseElf>();

        tile.SetUnit(spawnedElfUnit);
    }

    public void SpawnNewDwarf(Tile tile) {
        var randomDwarfPrefab = GetRandomUnit<BaseDwarf>(Faction.Dwarf);
        var spawnedDwarf = PhotonNetwork.Instantiate(randomDwarfPrefab.name, new Vector2(0,0), Quaternion.identity);
        var spawnedDwarfUnit = spawnedDwarf.GetComponent<BaseDwarf>();

        tile.SetUnit(spawnedDwarfUnit);
    }

    public void SpawnNewDemon(Tile tile) {
        var randomDemonPrefab = GetRandomUnit<BaseDemon>(Faction.Demon);
        var spawnedDemon = PhotonNetwork.Instantiate(randomDemonPrefab.name, new Vector2(0,0), Quaternion.identity);
        var spawnedDemonUnit = spawnedDemon.GetComponent<BaseDemon>();

        tile.SetUnit(spawnedDemonUnit);
    }

    // Grab random prefab model of a certain faction - only one prefab per faction at the moment
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHuman(BaseHuman human) {
        SelectedHuman = human;
    }
    public void SetSelectedOrc(BaseOrc orc) {
        SelectedOrc = orc;
    }
    public void SetSelectedElf(BaseElf elf) {
        SelectedElf = elf;
    }
    public void SetSelectedDwarf(BaseDwarf dwarf) {
        SelectedDwarf = dwarf;
    }
    public void SetSelectedDemon(BaseDemon demon) {
        SelectedDemon = demon;
    }

}
