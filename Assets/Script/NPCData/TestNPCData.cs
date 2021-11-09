using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCData : NPCData
{
    private bool _firstTalkFlag = true;

    [SerializeField]
    TimeManager _timeManager = default;


    public override Conversation GetConversation()
    {
        
        
        float nowTime = _timeManager.WorldTime;
        //クエスト優先
        if (QuestHaveFlag)
        {
            Debug.Log("クエストボイス");
            _talkAudioSource.clip = _questvoice;
            _talkAudioSource.Play();
        }
        //よる
        else if (nowTime >= 30 && nowTime < 75)
        {
            _talkAudioSource.clip = _audioClips[0];
        }
        //朝
        else if(nowTime >= 75 && nowTime < 95)
        {
            _talkAudioSource.clip = _audioClips[1];
        }
        //昼
        else
        {
            _talkAudioSource.clip = _audioClips[2];
        }

        Conversation conversation = base.GetConversation();
        return conversation;
    }
    
    [SerializeField]
    private AudioClip[] _audioClips=new AudioClip[3];
    [SerializeField]
    private AudioClip _questvoice;
    private void CheckNowPublicOrder()
    {

    }

    public override void EndTalk()
    {
        if (_firstTalkFlag)
        {
            QuestHaveFlag = true;
            _firstTalkFlag = false;
        }
        base.EndTalk();

    }

}
