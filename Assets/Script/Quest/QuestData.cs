using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NameTag;
using QuestList;

/// <summary>
/// クエストに関する情報を格納した親クラス
/// クエストクリア条件は必ず実装
/// クエスト失敗条件はクエストごとに決める
/// </summary>
public abstract class QuestData : MonoBehaviour
{
    [SerializeField]
    private NPCData NPCData=default;

    private UIManager _uiManager = default;
    private AudioManager audioManager = default;

    private void Awake()
    {

        _uiManager = GameObject.FindGameObjectWithTag(Tags.UIManager).GetComponent<UIManager>();
        audioManager = GameObject.FindGameObjectWithTag(Tags.Audio).GetComponent<AudioManager>();
        NPCData = GetComponent<NPCData>();
    }

    [SerializeField]
    public Conversation _questSelectSerifu;

    [SerializeField]
    public Conversation _questClereSerifu;

    /// <summary>
    /// クエストの報酬
    /// </summary>
    [SerializeField]
    private int _questClereReward = 0;
    [SerializeField]
    private int _questFailedReward = 0;

    /// <summary>
    /// クエストの名前
    /// </summary>
    [SerializeField]
    private  string _questName = "";

    [SerializeField]
    [TextArea(1, 20)]
    private string _questContent = "";

    private bool _questClereFlag = false;

    /// <summary>
    /// クエストクリア
    /// </summary>
    public virtual void QuestClere()
    {
        //治安ポイント加算
        NPCData.SetOrder(_questClereReward);
        NPCData._questEndFlag = true;
        //クエストクリア画面表示
        _uiManager.QuestClereNameSet(_questName);
        _uiManager.QuestClereWindowSetActive(true);
        _uiManager.QuestClereNow();
        _uiManager.ProgressQuestUISetActive(false);
        //audioManager.SEOn(0);
    }

    /// <summary>
    /// クエスト受けるかどうかの選択画面表示
    /// </summary>
    public virtual void QuestSelect()
    {
        _uiManager.QuestWindowNameSet(_questName);
        _uiManager.QuestWindowContentsSet(_questContent);
        _uiManager.QuestWindowSetActive(true);
        _uiManager.CanRepeatQuestSetActive(_repeat);
        audioManager.SEOn(1);
    }

    protected void SetProgressQuest()
    {
        _uiManager.ProgressQuestNameSet(_questName);
        _uiManager.ProgressQuestContentSet(_questContent);
        _uiManager.ProgressQuestUISetActive(true);
    }

    public abstract void QuestStart();

    public bool _repeat = false;
}
