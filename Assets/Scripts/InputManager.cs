using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
