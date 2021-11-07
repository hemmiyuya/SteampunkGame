using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCData : NPCData
{

    public override Conversation GetConversation()
    {

        return base.GetConversation();
    }

    private void CheckNowPublicOrder()
    {

    }

    protected override void Awake()
    {
        QuestHaveFlag = true;
        base.Awake();
    }

}
