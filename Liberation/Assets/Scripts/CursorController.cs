using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Texture2D cursor;
    public Texture2D cursorClicked;

    private void ChangeCursor(Texture2D cursorType) {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    private void Awake() {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.None;
    }



}
