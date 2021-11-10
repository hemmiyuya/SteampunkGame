using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Cursor.visible = true;
        }
    }

}
