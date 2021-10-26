using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    [SerializeField]
    private string npcName = default;

    [SerializeField]
    private Conversation conversation = null;

    public string Name
    {
        get { return npcName; }
    }

    //　Conversionスクリプトを返す
    public Conversation GetConversation()
    {
        return conversation;
    }
}
