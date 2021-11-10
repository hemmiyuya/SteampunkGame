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
    }
    public string Name
    {
        get { return _npcName; }
    }

    //　Conversionスクリプトを返す
    public virtual Conversation GetConversation()
    {
        
        if (QuestHaveFlag)
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
    private QuestData _questData;

    public void SetOrder(int orderPoint)
    {
        _orderSystem.AddOrder(orderPoint);
    }

    public virtual void EndTalk()
    {
        
    }
}
