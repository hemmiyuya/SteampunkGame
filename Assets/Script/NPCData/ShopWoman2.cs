using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWoman2 : NPCData
{
    public override Conversation GetConversation()
    {
        if ((int)_orderSystem.NowOrder >= 5&&!_questEndFlag)
        {
            QuestHaveFlag = true;
            _talkAudioSource.clip = _audioClip;
        }
        else
        {
            
        }

        return base.GetConversation();
    }

    [SerializeField]
    private AudioClip _audioClip;

    public bool _questEndFlag = false;
}
