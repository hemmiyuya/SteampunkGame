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
                iconImage.sprite = iconSprites[0];
                break;

            case CanCheckObjTag:
                iconImage.sprite = iconSprites[1];
                break;
        }
    }

}
