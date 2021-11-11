using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentlman2 : NPCData
{
    public override Conversation GetConversation()
    {
        if (_orderSystem.NowOrder >= 4)
        {
            QuestHaveFlag = true;
            _questData = GetComponent<TomasTeaQuest>();
        }
        return base.GetConversation();
    }
}
