using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertTile : Tile
{
    public Color _baseColor, _offsetColor;

    // Checkerboard colour pattern
    public override void Init(int x, int y) {
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
