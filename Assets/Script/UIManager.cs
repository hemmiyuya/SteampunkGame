using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各UIの表示非表示内容変更を司る最強のクラス
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _talkUI = default;
    [SerializeField]
    private GameObject _checkIconUI = default;
    [SerializeField]
    private GameObject _namePanel = default;
    [SerializeField]
    private Text _npcName = default;
    [SerializeField]
    private Text _namePanelName = default;
    [SerializeField]
    private GameObject _canvas = default;
    [SerializeField]
    private GameObject _questWindow = default;
    [SerializeField]
    private Text _questWindowNameText = default;
    [SerializeField]
    private Text _questContents = default;
    [SerializeField]
    private GameObject _questClere = default;
    [SerializeField]
    private Text _questClereNameText = default;
    [SerializeField]
    protected GameObject _progressQuest;
    [SerializeField]
    protected TextMeshProUGUI _progressQuestName;
    [SerializeField]
    protected TextMeshProUGUI _progressQuestContent;
    [SerializeField]
    private Image[] _orderStarsUI = new Image[5];
    [SerializeField]
    private Sprite _orderStarsisColor = default;
    [SerializeField]
    private Sprite _orderStarsNotColor = default;
    [SerializeField]
    private Sprite _orderStarsRainbow = default;

    [SerializeField]
    private GameObject _stateGage;

    [SerializeField]
    private CharacontrolManager _charaMamaneger;


    /// <summary>
    /// 会話中などのイベント中に非表示にするUI
    /// </summary>
    [SerializeField]
    private GameObject iventDisappearUI;

    [SerializeField]
    private GameObject QuestDisappearUI;

    public void TalkUISetActive(bool set)
    {
        _talkUI.SetActive(set);
    }

    public void NamePanelSetActive(bool set)
    {
        _namePanel.SetActive(set);
    }

    public void QuestWindowSetActive(bool set)
    {
        _questWindow.SetActive(set);
    }

    public void QuestClereWindowSetActive(bool set)
    {
        _questClere.SetActive(set);
    }

    public void ProgressQuestUISetActive(bool set)
    {
        _progressQuest.SetActive(set);
    }

    public void QuestWindowNameSet(string questName)
    {
        _questWindowNameText.text = questName;
    }

    public void QuestClereNameSet(string questName)
    {
        _questClereNameText.text = questName;
    }

    public void QuestWindowContentsSet(string questName)
    {
        _questContents.text = questName;
    }

    public void ProgressQuestNameSet(string questName)
    {
        _progressQuestName.text = questName;
    }

    public void ProgressQuestContentSet(string contentText)
    {
        _progressQuestContent.text = contentText;
    }

    public void NPCNameSet(string npcName)
    {
        _npcName.text = npcName;
    }

    public void NamePanelSet(string npcName)
    {
        _namePanelName.text = npcName;
    }

    public void CheckIconSetActive(bool set)
    {
        _checkIconUI.SetActive(set);
    }

    /// <summary>
    /// イベント中に不必要なUIを非表示に
    /// </summary>
    public void IventNow()
    {
        iventDisappearUI.SetActive(false);
        _charaMamaneger.moveFlag = false;
    }

    public void IventEnd()
    {
        iventDisappearUI.SetActive(true);
        _charaMamaneger.moveFlag = true;
    }

    public void QuestClereNow()
    {
        QuestDisappearUI.SetActive(false);
    }

    public void QuestClereEnd()
    {
        QuestDisappearUI.SetActive(true);
    }

    public void SetOrderStars(float orderPoint)
    {
        if (orderPoint == 500)
        {
            for(int i = _orderStarsUI.Length-1; i >= 0; i--)
            {
                _orderStarsUI[i].sprite = _orderStarsRainbow;
            }
        }

        else
        {
            if (orderPoint >= 400)
            {
                _orderStarsUI[4].sprite = _orderStarsisColor;
            }
            else
            {
                _orderStarsUI[4].sprite = _orderStarsNotColor;
            }


            if (orderPoint >= 300)
            {
                _orderStarsUI[3].sprite = _orderStarsisColor;
            }
            else
            {
                _orderStarsUI[3].sprite = _orderStarsNotColor;
            }


            if (orderPoint >= 200)
            {
                _orderStarsUI[2].sprite = _orderStarsisColor;
            }
            else
            {
                _orderStarsUI[2].sprite = _orderStarsNotColor;
            }

            if (orderPoint >= 100)
            {
                _orderStarsUI[1].sprite = _orderStarsisColor;
            }
            if (orderPoint > 0)
            {
                _orderStarsUI[0].sprite = _orderStarsisColor;
            }
            if (orderPoint == 0)
            {

            }
        }
        
    }

}
