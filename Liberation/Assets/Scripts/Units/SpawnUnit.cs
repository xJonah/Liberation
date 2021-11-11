using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnUnit : MonoBehaviour
{

    public GameObject unitPrefab;
    public int minX, maxX, minY, maxY;

    private void Start() {
        Vector2 randomTilePosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY , maxY));
        PhotonNetwork.Instantiate(unitPrefab.name, randomTilePosition, Quaternion.identity);
    }

}
