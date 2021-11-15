using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameTag;
using UnityEngine.UI;

public abstract class NPCData : MonoBehaviour
{
    [SerializeField]
    private string _npcName = default;

    [SerializeField]
    private Conversation[] conversations = new Conversation[7];

    [SerializeField]
    private GameObject _questAcceptNow;

    private GameObject _player;

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

    protected virtual void Start()
    {
        _questAcceptNow = GameObject.FindGameObjectWithTag(Tags.Canvas).transform.GetChild(4).GetChild(1).GetChild(3).gameObject;
        if (transform.tag == Tags.NPC)
        {
            _questIconRectTrs = _questIconUI.GetComponent<RectTransform>();
            _questIconSizeDefault = _questIconRectTrs.sizeDelta;
            _player = GameObject.FindGameObjectWithTag(Tags.Player);
        }
        
    }
    public string Name
    {
        get { return _npcName; }
    }

    [SerializeField]
    private GameObject _questIconUI;
    private RectTransform _questIconRectTrs;
    private Vector2 _questIconSizeDefault;
    private Vector3 offset = new Vector3(0, 1.2f, 0);

    private bool OnBecameNow = default;
    private void Update()
    {
        //カメラに写ってるときクエストアイコン表示
        if (transform.tag == Tags.NPC)
        {
            if (CanQuestCheck()&& OnBecameNow)
            {
                _questIconUI.SetActive(true);
                _questIconRectTrs.position= RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position+ offset);
                _questIconRectTrs.sizeDelta = _questIconSizeDefault / (Vector3.Distance(_player.transform.position, transform.position)*0.2f);
                if (_questIconRectTrs.sizeDelta.x >= _questIconSizeDefault.x)
                {
                    _questIconRectTrs.sizeDelta = _questIconSizeDefault;
                }
            }
            else _questIconUI.SetActive(false);
        }
    }

    //セリフ
    public virtual Conversation GetConversation()
    {
        //クエスト持ってるときはクエストのセリフ
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

    private void OnBecameInvisible()
    {
        if(transform.tag==Tags.NPC) _questIconUI.SetActive(false);
        OnBecameNow = false;
    }

    private void OnBecameVisible()
    {
        OnBecameNow = true;
    }
}
