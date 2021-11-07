using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Conversation", menuName = "CreateConversation")]
public class Conversation : ScriptableObject
{
    //�@��b���e
    /*
     * �ő��3�s�܂ŁB
     * ����ȏ�L������ꍇ��<>������ƕ����ł���
     * 
     * �L����
     * ����ɂ��͖l�͑��l����
     * ���̓Q�[��������Ă����
     * �N�͉������Ă����<>���I�H���̊X�ɗ����΂�����Ȃ́I�H
     * ���̊X�͂������񂨓X�������Ċy�����ꏊ����I
     * �F�X���Ă݂�Ƃ�����I
     * 
     * 
     * ���\������
     * ����ɂ��͖l�͑��l����
     * ���̓Q�[��������Ă����
     * �N�͉������Ă����
     * 
     * ���I�H���̊X�ɗ����΂�����Ȃ́I�H
     * ���̊X�͂������񂨓X�������Ċy�����ꏊ����I
     * �F�X���Ă݂�Ƃ�����I
     */
    [SerializeField]
    [Multiline(100)]
    private string message = null;

    //�@��b���e��Ԃ�
    public string GetConversationMessage()
    {
        return message;
    }
}