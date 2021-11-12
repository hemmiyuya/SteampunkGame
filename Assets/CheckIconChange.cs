using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NameTag;

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

    private const string Talk = "òbÇ∑";
    private const string Check = "í≤Ç◊ÇÈ";
    private const string Sleep = "èhâÆ";

    private void Awake()
    {
        talkIvent = GetComponent<StartTalkIvent>();
    }

    public void ChangeIcon(Transform checkObj)
    {
        targetTransform = checkObj;
        talkIvent.target = checkObj;
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

            case Tags.INN:
                CheckIconText.text = Sleep;
                iconImage.sprite = iconSprites[3];
                break;
        }
    }

}
