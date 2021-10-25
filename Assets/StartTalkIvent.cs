using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTalkIvent : MonoBehaviour
{
    private Transform targetTransform = default;

    [SerializeField]
    private GameObject player = default;

    [SerializeField]
    private GameObject talkUI = default;
    [SerializeField]
    private GameObject namePanel = default;
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
                namePanel.SetActive(true);
                namePanel.transform.GetChild(0).GetComponent<Text>().text = targetTransform.GetComponent<NPCData>().Name;
            }
        }
    }

    public void EndTalkIvent()
    {
        namePanel.SetActive(false);
        talkUI.SetActive(false);
        player.GetComponent<NPCTalkLength>().NPCTalkEnd();
    }

    public Transform target
    {
        set { targetTransform = value; }
    }

}
