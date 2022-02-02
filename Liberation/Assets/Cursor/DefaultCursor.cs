using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCursor : MonoBehaviour
{
    // Default cursor for scenes that are not "Game" Scene
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
