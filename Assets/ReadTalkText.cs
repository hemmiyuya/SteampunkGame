using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadTalkText : MonoBehaviour
{
    private bool CanRead = false;


    [SerializeField]
    private Text dataText;
    //�@�ǂލ��ރe�L�X�g���������܂�Ă���.txt�t�@�C��
    [SerializeField]
    private TextAsset textAsset;

    private string loadText;
    private string[] splitText;
    private int textNum;

    private void Update()
    {
        if (CanRead)
            //�y�[�W�߂���
            if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {

        }
    }

    //NPC�̕��͂�ǂݍ���
    public void ReadText(Transform targetTrs)
    {



        CanRead = true;
    }
}
