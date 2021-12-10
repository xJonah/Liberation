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

    public void SetUnit(BaseUnit unit) {

        if (unit.OccupiedTile != null) {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }


    void OnMouseDown() {
        
    }

}

