using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndestroyQuestClere : MonoBehaviour
{
    [SerializeField]
    private QuestData questData = default;

    private void Awake()
    {
        questData = GameObject.Find("RangerIdle02").transform.GetChild(3).GetComponent<QuestData>();
    }

    private void OnDestroy()
    {
        Debug.Log("É{ÉXì|ÇµÇΩ");
        questData.QuestClere();
    }
}
