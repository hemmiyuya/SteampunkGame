using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCName : MonoBehaviour
{
    [SerializeField]
    private string npcName = default;

    public string Name
    {
        get { return npcName; }
    }
}
