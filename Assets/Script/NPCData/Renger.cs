using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renger : NPCData
{
    public override Conversation GetConversation()
    {
        
        return base.GetConversation();
    }

    private void FixedUpdate()
    {
        if ((int)_orderSystem.NowOrder == 6)
        {
            QuestHaveFlag = true;
        }
        else
        {
            _talkAudioSource.clip = _audioClip;
        }

    }

    [SerializeField]
    private AudioClip _audioClip;
}
