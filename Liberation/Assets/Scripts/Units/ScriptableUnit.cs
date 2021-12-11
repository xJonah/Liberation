using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allow creation of scriptable unit in unity
[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]

public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;
}

//Faction List
public enum Faction {
    Human,
    Orc
}
