using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NameTag;
using QuestList;

/// <summary>
/// �N�G�X�g�Ɋւ�������i�[�����e�N���X
/// �N�G�X�g�N���A�����͕K������
/// �N�G�X�g���s�����̓N�G�X�g���ƂɌ��߂�
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
    private  string _questName = "";

    [SerializeField]
    [TextArea(1, 20)]
    private string _questContent = "";

    private bool _questClereFlag = false;

    /// <summary>
    /// �N�G�X�g�N���A
    /// </summary>
    public virtual void QuestClere()
    {
        //�����|�C���g���Z
        NPCData.SetOrder(_questClereReward);
        NPCData._questEndFlag = true;
        //�N�G�X�g�N���A��ʕ\��
        _uiManager.QuestClereNameSet(_questName);
        _uiManager.QuestClereWindowSetActive(true);
        _uiManager.QuestClereNow();
        _uiManager.ProgressQuestUISetActive(false);
        //audioManager.SEOn(0);
    }

    /// <summary>
    /// �N�G�X�g�󂯂邩�ǂ����̑I����ʕ\��
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
