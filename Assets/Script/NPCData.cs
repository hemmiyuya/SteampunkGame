using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    [SerializeField]
    private string npcName = default;

    [SerializeField]
    private Conversation[] conversations = new Conversation[7];

    public PublicOrderSystem _orderSystem;

    private void Awake()
    {
        _orderSystem = GameObject.FindGameObjectWithTag("OrderSytem").GetComponent<PublicOrderSystem>();
    }
    public string Name
    {
        get { return npcName; }
    }

    //　Conversionスクリプトを返す
    public Conversation GetConversation()
    {
        return conversations[_orderSystem.NowOrder];
    }
}
