using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndestroyQuestClereShopWoman1 : MonoBehaviour
{
    [SerializeField]
    private QuestData questData = default;

    private void Awake()
    {
        questData = GameObject.Find("売り込み女の子1").transform.GetChild(11).GetComponent<QuestData>();
    }

    private void OnDestroy()
    {
        questData.QuestClere();
    }
}
