using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuestList;
using NameTag;

public class StartTalkIvent : MonoBehaviour
{
    private Transform targetTransform = default;

    [SerializeField]
    private GameObject player = default;

    [SerializeField]
    private GameObject talkUI = default;
    [SerializeField]
    private GameObject namePanel = default;

    [SerializeField]
    private UIManager _uiManager;

    public bool QuestTalkNow {
        get; set;
    } = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.GetComponent<NPCTalkLength>().NPCTalkNow();
            _uiManager.TalkUISetActive(true);
            _uiManager.IventNow();
            talkUI.SetActive(true);
            talkUI.GetComponent<ReadTalkText>().SetMessagePanel(targetTransform.GetComponent<NPCData>().GetConversation().GetConversationMessage());
            if (targetTransform.tag == "NPC")
            {
                //NPCÇ»ÇÁñºëOògÇ‡ï\é¶
                No1.TalkCountUp();
                _uiManager.NamePanelSetActive(true);
                _uiManager.NamePanelSet(targetTransform.GetComponent<NPCData>().Name);
                if (!_uiManager._questClereFlag)
                {
                    QuestTalkNow = targetTransform.GetComponent<NPCData>().CanQuestCheck();
                }
            }
            else QuestTalkNow = false;

            //èhâÆÇ»ÇÁÅAãxåeÇ∑ÇÈÇ©Ç«Ç§Ç©ï∑Ç≠
            if (targetTransform.tag == Tags.INN)
            {

            }
        }
    }

    public void EndTalkIvent()
    {
        namePanel.SetActive(false);
        talkUI.SetActive(false);
        if (QuestTalkNow)
        {
            targetTransform.GetComponent<QuestData>().QuestSelect();
        } 
        else if (targetTransform.tag == Tags.INN)
        {
            _uiManager.INNWindowSetActive(true);
        }
        else
        {
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
            target.GetComponent<NPCData>().EndTalk();
            if (_uiManager._questClereFlag)
            {
                player.GetComponent<CharacontrolManager>().moveFlag = false;
            }
        }
    }

    public Transform target
    {
        get { return targetTransform; }
        set { targetTransform = value; }
    }

}
