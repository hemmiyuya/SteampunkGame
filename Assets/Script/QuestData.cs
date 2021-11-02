using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �N�G�X�g�Ɋւ�������i�[�����e�N���X
/// �N�G�X�g�N���A�����͕K������
/// �N�G�X�g���s�����̓N�G�X�g���ƂɌ��߂�
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
    /// �N�G�X�g�̕�V
    /// </summary>
    [SerializeField]
    private int _questClereReward = 0;
    [SerializeField]
    private int _questFailedReward = 0;

    /// <summary>
    /// �N�G�X�g�̖��O
    /// </summary>
    [SerializeField]
    private string _questName = "";

    [SerializeField]
    private string _questContent = "";

    private bool _questClereFlag = false;


    /// <summary>
    /// �N�G�X�g�N���A
    /// </summary>
    public virtual void QuestClere()
    {
        //�N�G�X�g�N���A��ʕ\��
        _questClereNameText.text = _questName;
        _questClere.SetActive(true);
        _progressQuest.SetActive(false);

    }

    /// <summary>
    /// �N�G�X�g�󂯂邩�ǂ����̑I����ʕ\��
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
