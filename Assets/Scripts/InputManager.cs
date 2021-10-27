using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }
    public float GetVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public bool GetRunButton()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public bool GetJumpButton()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool GetMouseLeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool GetSwitchWeaponButton()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}
