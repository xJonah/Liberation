using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allow creation of scriptable unit in unity
[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]

public class ScriptableUnit : ScriptableObject
{
    //Traits of a scriptable unit
    public Faction Faction;
    public BaseUnit UnitPrefab;
}

// Declare faction types
public enum Faction {
    Human,
    Orc,
    Elf,
    Demon,
    Dwarf
}
