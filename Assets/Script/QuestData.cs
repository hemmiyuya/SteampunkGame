using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �N�G�X�g�Ɋւ�������i�[�����e�N���X
/// �N�G�X�g�N���A�����͕K������
/// �N�G�X�g���s�����̓N�G�X�g���ƂɌ��߂�
/// </summary>
public abstract class QuestData : MonoBehaviour
{
    private NPCData NPCData=default;



    /// <summary>
    /// �N�G�X�g�̕�V
    /// </summary>
    [SerializeField]
    private int _questClereReward = 0;
    [SerializeField]
    private int _questFailedReward = 0;

    /// <summary>
    /// �N�G�X�g�̖��O
    /// </summary>
    [SerializeField]
    private string _questName = "";

    /// <summary>
    /// �N�G�X�g�N���A
    /// </summary>
    public virtual void QuestClere()
    {
        //�N�G�X�g�N���A��ʕ\��
        
    }
}
