using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalkLength : MonoBehaviour
{
    [SerializeField]
    private GameObject checkIcon = default;
    private Image iventIcon = default;
    [SerializeField]
    private GameObject talkTargetName = default;

    private Text targetName = default;

    private const string NPCTag = "NPC";
    private const string CanCheckObjTag = "CanCheckObj";

    /// <summary>
    /// 0=Normal 1=Talk,
    /// </summary>
    private enum TalkState
    {
        CanTalk=0,
        TalkNow=1,
    }

    TalkState _talkState = (TalkState)0;

    private void Awake()
    {
        targetName = talkTargetName.transform.GetChild(1).GetComponent<Text>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (_talkState == 0)
        {


            if(other.tag== NPCTag || other.tag== CanCheckObjTag)
            {
                checkIcon.SetActive(true);
                checkIcon.GetComponent<CheckIconChange>().ChangeIcon(other.transform);
                switch (other.tag)
                {
                    case NPCTag:
                        talkTargetName.SetActive(true);
                        targetName.text = talkTargetName.GetComponent<NPCNamePos>().SetTarget(other.transform);
                        break;
                    case CanCheckObjTag:
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == NPCTag || other.tag == CanCheckObjTag)
        {
            //プレイヤーが出たときアイコンを非表示に
            checkIcon.SetActive(false);

            switch (other.tag)
            {
                case NPCTag:
                    talkTargetName.SetActive(false);
                    break;
                case CanCheckObjTag:
                    break;
            }
        }
    }

    public void NPCTalkNow ()
    {
        _talkState = (TalkState)1;

        checkIcon.SetActive(false);
        switch (gameObject.tag)
        {
            case NPCTag:
                talkTargetName.SetActive(false);
                break;
            case CanCheckObjTag:
                break;
        }
    }

    public void NPCTalkEnd()
    {
        _talkState = (TalkState)0;
    }
}
