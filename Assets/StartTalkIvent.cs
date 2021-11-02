using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuestList;

public class StartTalkIvent : MonoBehaviour
{
    private Transform targetTransform = default;

    [SerializeField]
    private GameObject player = default;

    [SerializeField]
    private GameObject talkUI = default;
    [SerializeField]
    private GameObject namePanel = default;

    public bool QuestTalkNow {
        get; set;
    } = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.GetComponent<NPCTalkLength>().NPCTalkNow();
            talkUI.SetActive(true);
            talkUI.GetComponent<ReadTalkText>().SetMessagePanel(targetTransform.GetComponent<NPCData>().GetConversation().GetConversationMessage());
            if (targetTransform.tag == "NPC")
            {
                //NPC‚È‚ç–¼‘O˜g‚à•\Ž¦
                No1.TalkCountUp();
                Debug.Log(No1.nowTalkCount.Value+"‚ ‚ ‚¢‚¤‚¦");
                namePanel.SetActive(true);
                namePanel.transform.GetChild(0).GetComponent<Text>().text = targetTransform.GetComponent<NPCData>().Name;
                QuestTalkNow = targetTransform.GetComponent<NPCData>().QuestHaveFlag;
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
        else
        {
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
        }
    }

    public Transform target
    {
        get { return targetTransform; }
        set { targetTransform = value; }
        
    }

}
