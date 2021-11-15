using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWoman2 : NPCData
{
    public override Conversation GetConversation()
    {
        


        return base.GetConversation();
    }

    private void FixedUpdate()
    {
        if ((int)_orderSystem.NowOrder >= 4 && !_questEndFlag)
        {
            QuestHaveFlag = true;
            _talkAudioSource.clip = _questAudioClip;
        }
        else
        {
            _talkAudioSource.clip = _audioClip;
        }
    }

    [SerializeField]
    private AudioClip _questAudioClip;

    [SerializeField]
    private AudioClip _audioClip;
    
}
