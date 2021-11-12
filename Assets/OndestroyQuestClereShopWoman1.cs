using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndestroyQuestClereShopWoman1 : MonoBehaviour
{
    [SerializeField]
    private QuestData questData = default;

    private void Awake()
    {
        questData = GameObject.Find("îÑÇËçûÇ›èóÇÃéq1").transform.GetChild(11).GetComponent<QuestData>();
    }

    private void OnDestroy()
    {
        questData.QuestClere();
    }
}
