using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
 
public abstract class Tile : MonoBehaviour {

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public BaseUnit OccupiedUnit;

    public bool Empty => OccupiedUnit == null;

    //Allow override in order for Tiles to have checkerboard pattern or not
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

    //Grid square is occupied when a unit is on it
    public void SetUnit(BaseUnit unit) {

        if (unit.OccupiedTile != null) {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    //On mouse down highlight attackable tiles surrounding player
    void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.HumanTurn) return;
        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Hero)
            {
                UnitManager.Instance.SelectedHuman((BaseHuman)OccupiedUnit);
            }
            else
            {
                if (UnitManager.Instance.SelectedHuman != null)
                {
                    var orc = (BaseOrc)OccupiedUnit;
                    Destroy(orc.gameObject);
                    UnitManager.Instance.SelectedHuman(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHuman != null)
            {
                SetUnit(UnitManager.Instance.SelectedHuman);
                UnitManager.Instance.SelectedHuman(null);
            }
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
        }


    }


}

