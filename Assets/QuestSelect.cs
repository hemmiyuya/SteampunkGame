using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelect : MonoBehaviour
{
    [SerializeField]
    StartTalkIvent talkIvent;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject progressQuest;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            QuestAccept();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            QuestCancel();
        }
        player.GetComponent<NPCTalkLength>().NPCTalkEnd();

    }

    private void QuestAccept()
    {
        talkIvent.target.GetComponent<QuestData>().QuestStart();
        talkIvent.target.GetComponent<NPCData>().QuestHaveFlag=false;
        
        gameObject.SetActive(false);
    }

    private void QuestCancel()
    {
        gameObject.SetActive(false);
    }

}
