using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    //Fields
    public Texture2D cursor;
    public Texture2D cursorClicked;

    // Change from default cursor to custom
    private void ChangeCursor(Texture2D cursorType) {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    // Change Cursor on Scene Start
    private void Awake() {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.None;
    }



}
