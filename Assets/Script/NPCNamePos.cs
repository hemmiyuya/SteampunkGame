using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNamePos : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    private RectTransform myRectTransform;
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    private bool targetNow = false;

    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetNow)
        {
            myRectTransform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position + offset);
        }
        
    }

    /// <summary>
    /// �^�[�Q�b�g�̃g�����X�t�H�[�����Z�b�g���Ė��O���擾���ĕԂ�
    /// </summary>
    /// <param name="target">��b����NPC�̃g�����X�t�H�[��</param>
    public string SetTarget(Transform targetTrs)
    {
        targetTransform = targetTrs;
        targetNow = true;
        return targetTrs.GetComponent<NPCName>().Name;
    }
}
