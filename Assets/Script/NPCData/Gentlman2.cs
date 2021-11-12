using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentlman2 : NPCData
{
    public override Conversation GetConversation()
    {
        if (TomasTeaGet)
        {
            GetComponent<TomasTeaQuest>().QuestClere();
            TomasTeaGet = false;
            TomasTeaQuestEnd = true;
            return GetComponent<TomasTeaQuest>()._questEndSerifu;

        }

        if (_orderSystem.NowOrder >= 4&& !TomasTeaQuestEnd)
        {
            QuestHaveFlag = true;
            if(CanQuestCheck())
            {
                _talkAudioSource.clip = _questvoice;
                _talkAudioSource.Play();
            }
            _questData = GetComponent<TomasTeaQuest>();
        }

        if (TomasTeaQuestEnd)
        {
            QuestHaveFlag = false;
            return _afterQuestSerihu;
        }


        return base.GetConversation();
    }

    /// <summary>
    /// トーマスのティーポットを拾ったときにtrue
    /// </summary>
    public bool TomasTeaGet = false;

    private bool TomasTeaQuestEnd = false;

    [SerializeField]
    private AudioClip _questvoice;

    [SerializeField]
    private Conversation _afterQuestSerihu;
}
