using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaziyaOyazi : NPCData
{
    public override Conversation GetConversation()
    {
        

        return base.GetConversation();
    }



    private void FixedUpdate()
    {
        if (_orderSystem.NowOrder >= 5)
        {
            QuestHaveFlag = true;
        }
    }
}
