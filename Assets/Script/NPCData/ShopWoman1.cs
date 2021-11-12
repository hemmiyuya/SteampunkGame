using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWoman1 : NPCData
{
    public override Conversation GetConversation()
    {
        if (!_questEndFlag&&CanQuestCheck())
        {
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

    /// <summary>
    /// ショップウーマン2(姉)のクエストが完了したら解禁
    /// </summary>
    public void QuestFlagON()
    {
        QuestHaveFlag = true;
        _questData = GetComponent<ShopWoman2Quest2>();
    }
}
