using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クエストに関する情報を格納した親クラス
/// クエストクリア条件は必ず実装
/// クエスト失敗条件はクエストごとに決める
/// </summary>
public abstract class QuestData : MonoBehaviour
{
    private NPCData NPCData=default;
    private GameObject _canvas = default;
    [SerializeField]
    private GameObject _questWindow = default;
    private Text _questWindowNameText = default;
    private Text _questContents = default;
    [SerializeField]
    private GameObject _questClere = default;
    private Text _questClereNameText = default;

    [SerializeField]
    protected GameObject _progressQuest;
    [SerializeField]
    protected TextMeshProUGUI _progressQuestName;
    [SerializeField]
    protected TextMeshProUGUI _progressQuestContent;

    private void Awake()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _questWindow=_canvas.transform.Find("QuestWindow").gameObject;
        _questClere = _canvas.transform.Find("QuestClere").gameObject;
        _questWindowNameText = _questWindow.transform.Find("QuestName").GetComponent<Text>();
        _questContents = _questWindow.transform.Find("Questnaiyou").GetComponent<Text>();
        _questClereNameText = _questClere.transform.Find("Window").Find("Name").GetComponent<Text>();
        _progressQuest = _canvas.transform.Find("NowProgressQuest").gameObject;
        _progressQuestName = _progressQuest.transform.Find("QuestName").GetComponent<TextMeshProUGUI>();
        _progressQuestContent = _progressQuest.transform.Find("Questnaiyou").GetComponent<TextMeshProUGUI>();

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
    private string _questName = "";

    [SerializeField]
    private string _questContent = "";

    private bool _questClereFlag = false;


    /// <summary>
    /// クエストクリア
    /// </summary>
    public virtual void QuestClere()
    {
        //クエストクリア画面表示
        _questClereNameText.text = _questName;
        _questClere.SetActive(true);
        _progressQuest.SetActive(false);

    }

    /// <summary>
    /// クエスト受けるかどうかの選択画面表示
    /// </summary>
    public virtual void QuestSelect()
    {
        _questWindowNameText.text = _questName;
        _questContents.text = _questContent;
        _questWindow.SetActive(true);
        
    }

    protected void SetProgressQuest()
    {
        _progressQuestName.text = _questName;
        _progressQuestContent.text = _questContent;
        _progressQuest.SetActive(true);
    }

    public abstract void QuestStart();
}
