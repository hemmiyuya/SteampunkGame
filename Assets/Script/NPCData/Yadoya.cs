using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yadoya : NPCData
{
    [SerializeField]
    private TimeManager _timeManager;

    public override Conversation GetConversation()
    {
        return base.GetConversation();
    }
}
