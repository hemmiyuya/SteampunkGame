using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameTag;

public abstract class NPCData : MonoBehaviour
{
    [SerializeField]
    private string _npcName = default;

    [SerializeField]
    private Conversation[] conversations = new Conversation[7];

    [SerializeField]
    private GameObject _questAcceptNow;

    protected enum Gender
    {
        man=0,
        woman=1,
    }
    
    [SerializeField]
    protected AudioSource _talkAudioSource = default;

    [SerializeField]
    public bool QuestHaveFlag { get; set; } = false;

    public PublicOrderSystem _orderSystem;

    protected virtual void Awake()
    {
        _orderSystem = GameObject.FindGameObjectWithTag("OrderSytem").GetComponent<PublicOrderSystem>();
        _questAcceptNow = GameObject.FindGameObjectWithTag(Tags.Canvas).transform.GetChild(4).GetChild(1).GetChild(3).gameObject;
    }
    public string Name
    {
        get { return _npcName; }
    }

    //　Conversionスクリプトを返す
    public virtual Conversation GetConversation()
    {
        //クエストフラグがたっており、他に進行中のクエストがなかったら
        if (QuestHaveFlag&& !_questAcceptNow.activeSelf)
        {
            return _questData._questSelectSerifu;
        }

        if (gameObject.tag == Tags.NPC)
        {
            _talkAudioSource.Play();
        }
        return conversations[_orderSystem.NowOrder];
        
    }

    [SerializeField]
    public QuestData _questData;

    public bool _questEndFlag = false;

    public void SetOrder(int orderPoint)
    {
        _orderSystem.AddOrder(orderPoint);
    }

    public virtual void EndTalk()
    {
        
    }

    public bool CanQuestCheck()
    {
        if (QuestHaveFlag && !_questAcceptNow.activeSelf) return true;
        else return false;
    }
}
