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

}

