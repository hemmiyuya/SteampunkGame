using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renger : NPCData
{
    public override Conversation GetConversation()
    {
        if ((int)_orderSystem.NowOrder == 6)
        {
            QuestHaveFlag = true;
        }
        

        return base.GetConversation();
    }

    [SerializeField]
    private AudioClip _audioClip;
}
