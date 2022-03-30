using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class UnitManager : MonoBehaviourPunCallbacks
{

    //Fields
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    private int tileArea;
    public BaseHuman SelectedHuman;
    public BaseOrc SelectedOrc;
    public BaseElf SelectedElf;
    public BaseDwarf SelectedDwarf;
    public BaseDemon SelectedDemon;
    public BaseUnit SelectedUnit;
    public const byte TILE_INFO = 4;
    private int spawnCount;
    private List<Tile> tileList;

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

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Offline");
            return;
        }
        else
        {
            spawnCount = tileArea / PhotonNetwork.CurrentRoom.PlayerCount;
        }

        //For Demo
        SpawnHumans();
        SpawnOrcs();

        /*  For a 4 or 5 player match

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {

            SpawnHumans(spawnCount);
            SpawnOrcs(spawnCount);
            SpawnDwarves(spawnCount);
            SpawnElves(spawnCount);

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            SpawnHumans(spawnCount);
            SpawnOrcs(spawnCount);
            SpawnDwarves(spawnCount);
            SpawnElves(spawnCount);
            SpawnDemons(spawnCount);
        }

        */


        GameManager.Instance.ChangeState(GameState.HumanTurn);
        
    }

    #region Spawn initial units
    // Function to spawn human units
    public void SpawnHumans() 
    {
        tileList = GridManager.Instance.GetTileList();
        for (int t = 0; t <= tileList.Count - 1; t += PhotonNetwork.CurrentRoom.PlayerCount)
        {
            var tileToSpawn = tileList[t];
            var randomHumanPrefab = GetRandomUnit<BaseHuman>(Faction.Human);
            var spawnedHuman = PhotonNetwork.Instantiate(randomHumanPrefab.name, new Vector2(0, 0), Quaternion.identity);
            var spawnedHumanUnit = spawnedHuman.GetComponent<BaseHuman>();
            tileToSpawn.SetUnit(spawnedHumanUnit);
        }               
    }

    //Spawn initial units
    public void SpawnOrcs()
    {
        tileList = GridManager.Instance.GetTileList();

        for (int t = 1; t <= tileList.Count; t += PhotonNetwork.CurrentRoom.PlayerCount)
        {
            var tileToSpawn = tileList[t];
            var randomOrcPrefab = GetRandomUnit<BaseOrc>(Faction.Orc);
            var spawnedOrc = PhotonNetwork.Instantiate(randomOrcPrefab.name, new Vector2(0, 0), Quaternion.identity);
            var spawnedOrcUnit = spawnedOrc.GetComponent<BaseOrc>();
            tileToSpawn.SetUnit(spawnedOrcUnit);         
        }
    }

    public void SpawnElves(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {
            var randomElfSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomElfPrefab = GetRandomUnit<BaseElf>(Faction.Elf);
            var spawnedElf = PhotonNetwork.Instantiate(randomElfPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedElfUnit = spawnedElf.GetComponent<BaseElf>();
            randomElfSpawnTile.SetUnit(spawnedElfUnit); 
        }
    }

    public void SpawnDwarves(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {
            var randomDwarfSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomDwarfPrefab = GetRandomUnit<BaseDwarf>(Faction.Dwarf);
            var spawnedDwarf = PhotonNetwork.Instantiate(randomDwarfPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedDwarfUnit = spawnedDwarf.GetComponent<BaseOrc>();
            randomDwarfSpawnTile.SetUnit(spawnedDwarfUnit); 
        }
    }

    public void SpawnDemons(int spawnCount) {
        for(int i = 0; i < spawnCount; i++) {
            var randomDemonSpawnTile = GridManager.Instance.GetSpawnTile();
            var randomDemonPrefab = GetRandomUnit<BaseDemon>(Faction.Demon);
            var spawnedDemon = PhotonNetwork.Instantiate(randomDemonPrefab.name, new Vector2(0,0), Quaternion.identity);
            var spawnedDemonUnit = spawnedDemon.GetComponent<BaseDemon>();
            randomDemonSpawnTile.SetUnit(spawnedDemonUnit); 
        }
    }

    #endregion

    #region Spawn New Unit
    //Spawn new unit on Battle lost/win

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

    #endregion

    // Grab random prefab model of a certain faction - only one prefab per faction at the moment
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    #region Selected Units
    //Hold the selected unit for players

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

    #endregion

}
