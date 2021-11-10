using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIconChange : MonoBehaviour
{
    [SerializeField]
    private Sprite[] iconSprites = new Sprite[5];

    [SerializeField]
    private Image iconImage = default;

    private const string NPCTag = "NPC";
    private const string CanCheckObjTag = "CanCheckObj";

    private Transform targetTransform = default;

    private StartTalkIvent talkIvent = default;

    [SerializeField]
    private Text CheckIconText;

    private const string Talk = "˜b‚·";
    private const string Check = "’²‚×‚é";

    private void Awake()
    {
        talkIvent = GetComponent<StartTalkIvent>();
    }

    public void ChangeIcon(Transform checkObj)
    {
        targetTransform = checkObj;
        talkIvent.target = checkObj;
        Debug.Log(checkObj.gameObject);
        Debug.Log(checkObj.tag);
        switch (checkObj.tag)
        {
            case NPCTag:
                CheckIconText.text = Talk;
                iconImage.sprite = iconSprites[0];
                break;

            case CanCheckObjTag:
                CheckIconText.text = Check;
                iconImage.sprite = iconSprites[1];
                break;
        }
    }

}
