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
    /// ターゲットのトランスフォームをセットして名前を取得して返す
    /// </summary>
    /// <param name="target">会話するNPCのトランスフォーム</param>
    public string SetTarget(Transform targetTrs)
    {
        targetTransform = targetTrs;
        targetNow = true;
        return targetTrs.GetComponent<NPCName>().Name;
    }
}
