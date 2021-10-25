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

    //�@Conversion�X�N���v�g��Ԃ�
    public Conversation GetConversation()
    {
        return conversation;
    }
}
