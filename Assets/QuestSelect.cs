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
    AudioManager audioManager = default;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            QuestAccept();
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            QuestCancel();
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
        }
        

    }

    private void QuestAccept()
    {
        audioManager.SEOn(2);
        talkIvent.target.GetComponent<QuestData>().QuestStart();
        talkIvent.target.GetComponent<NPCData>().QuestHaveFlag=false;
        
        gameObject.SetActive(false);
    }

    private void QuestCancel()
    {
        audioManager.SEOn(3);
        gameObject.SetActive(false);
    }

}
