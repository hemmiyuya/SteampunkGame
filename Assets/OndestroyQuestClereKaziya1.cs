using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndestroyQuestClereKaziya1 : MonoBehaviour
{
    [SerializeField]
    private QuestData questData = default;

    private void Awake()
    {
        questData = GameObject.Find("kaziya1").transform.GetChild(7).GetComponent<QuestData>();
    }

    private void OnDestroy()
    {
        questData.QuestClere();
    }
}
