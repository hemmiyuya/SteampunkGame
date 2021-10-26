using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

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

    [SerializeField]
    private StartTalkIvent talkIvent;


    //�@�\�����郁�b�Z�[�W
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = "�����RPG�ł悭�g���郁�b�Z�[�W�\���@�\����肽���Ǝv���܂��B\n"
            + "���b�Z�[�W���\�������X�s�[�h�̒��߂��\�ł���A���s�ɂ��Ή����܂��B\n"
            + "���P�̗]�n�����Ȃ肠��܂����A               �Œ���̋@�\�͔����Ă���Ǝv���܂��B\n"
            + "���Њ��p���Ă݂Ă��������B\n<>"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������<>"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������<>"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������"
            + "��������������������������������������������������������������������������������������������������������������������������������������";
    //�@�g�p���镪��������
    [SerializeField]
    private string splitString = "<>";
    //�@�����������b�Z�[�W
    private string[] splitMessage;
    //�@�����������b�Z�[�W�̉��Ԗڂ�
    private int messageNum;
    //�@�e�L�X�g�X�s�[�h
    [SerializeField]
    private float textSpeed = 0.05f;
    //�@�o�ߎ���
    private float elapsedTime = 0f;
    //�@�����Ă��镶���ԍ�
    private int nowTextNum = 0;

    //�@1�񕪂̃��b�Z�[�W��\���������ǂ���
    private bool isOneMessage = false;
    //�@���b�Z�[�W�����ׂĕ\���������ǂ���
    private bool isEndMessage = true;

    void Start()
    {
        //clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
        dataText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        dataText.text = "";
        gameObject.SetActive(false);
    }

    void Update()
    {
        //�@���b�Z�[�W���I����Ă��邩�A���b�Z�[�W���Ȃ��ꍇ�͂���ȍ~�������Ȃ�
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //�@1��ɕ\�����郁�b�Z�[�W��\�����Ă��Ȃ�	
        if (!isOneMessage)
        {
            //�@�e�L�X�g�\�����Ԃ��o�߂����烁�b�Z�[�W��ǉ�
            if (elapsedTime >= textSpeed)
            {
                dataText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                //�@���b�Z�[�W��S���\���A�܂��͍s�����ő吔�\�����ꂽ
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //�@���b�Z�[�W�\�����Ƀ}�E�X�̉E�{�^������������ꊇ�\��
            if (Input.GetMouseButtonDown(1))
            {
                //�@�����܂łɕ\�����Ă���e�L�X�g�Ɏc��̃��b�Z�[�W�𑫂�
                dataText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //�@1��ɕ\�����郁�b�Z�[�W��\������
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //�@�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������


            //�@�}�E�X�N���b�N���ꂽ�玟�̕����\������
            if (Input.GetMouseButtonDown(1))
            {
                nowTextNum = 0;
                messageNum++;
                dataText.text = "";

                elapsedTime = 0f;
                isOneMessage = false;

                //�@���b�Z�[�W���S���\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                if (messageNum >= splitMessage.Length)
                {
                    isEndMessage = true;
                    talkIvent.EndTalkIvent();
                }
            }
        }
    }

    void SetMessage(string message)
    {
        this.allMessage = message;
        //�@����������ň��ɕ\�����郁�b�Z�[�W�𕪊�����
        splitMessage = Regex.Split(allMessage, splitString);
        nowTextNum = 0;
        messageNum = 0;
        dataText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }
    //�@���̃X�N���v�g����V�������b�Z�[�W��ݒ�
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
    }
}
