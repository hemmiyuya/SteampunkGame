using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomasTeaCup : NPCData
{
    private bool _questNow = false;


    [SerializeField]
    private GameObject _parentObj;

    public void QuestFlagON()
    {
        _questNow = true;
    }

    [SerializeField]
    private Conversation QuestNowSerihu;

    public override Conversation GetConversation()
    {
        if (_questNow)
        {
            GetTea();
            return QuestNowSerihu;
        }
        return base.GetConversation();
    }

    private void GetTea()
    {
        _parentObj.SetActive(false);
    }
    
}
