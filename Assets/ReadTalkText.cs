using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadTalkText : MonoBehaviour
{
    private bool CanRead = false;


    [SerializeField]
    private Text dataText;
    //　読む込むテキストが書き込まれている.txtファイル
    [SerializeField]
    private TextAsset textAsset;

    private string loadText;
    private string[] splitText;
    private int textNum;

    private void Update()
    {
        if (CanRead)
            //ページめくり
            if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {

        }
    }

    //NPCの文章を読み込む
    public void ReadText(Transform targetTrs)
    {



        CanRead = true;
    }
}
