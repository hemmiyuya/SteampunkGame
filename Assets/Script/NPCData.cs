using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCData : MonoBehaviour
{
    [SerializeField]
    private string _npcName = default;

    [SerializeField]
    private Conversation[] conversations = new Conversation[7];

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
        
        return conversations[_orderSystem.NowOrder];
        
    }

    [SerializeField]
    private QuestData _questData;


}
