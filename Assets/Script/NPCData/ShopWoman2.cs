using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWoman2 : NPCData
{
    public override Conversation GetConversation()
    {
        if ((int)_orderSystem.NowOrder >= 3&&!_questEndFlag)
        {
            QuestHaveFlag = true;
            _talkAudioSource.clip = _questAudioClip;
        }
        else
        {
            _talkAudioSource.clip = _audioClip;
        }

        return base.GetConversation();
    }

    [SerializeField]
    private AudioClip _questAudioClip;

    [SerializeField]
    private AudioClip _audioClip;
    
}
