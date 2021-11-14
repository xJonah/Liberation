using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
 
public abstract class Tile : MonoBehaviour {

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    //Allow override in order for offset
    public virtual void Init(int x, int y) {
        
    }

    //Highlight Functions
    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
    void onMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.HumanTurn) return;
        if (OccupiedUnit != null) {
            if (OccupiedUnit.Faction == Faction.Human)UnitManager.Instance.SelectedHuman(BaseHuman)OccupiedUnit)
                if (UnitManager.Instance.SelectedHuman != null) { 
                    var enemy = (BaseOrcs)OccupiedUnit;
                Destroy(enemy.gameObject);
                    UnitManager.Instance.SelectedHuman(null);
                        
     
            }
    }


    }
    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.TransformPostion = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
       

}

