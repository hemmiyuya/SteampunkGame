using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class OrderMaxQuest : QuestData
{
    private OrderMaxBoss maxBoss = new OrderMaxBoss();

    [SerializeField]
    private Transform BossSpqwnTrs = default;

    [SerializeField]
    private GameObject _bossObj = default;

    public override void QuestStart()
    {
        SetProgressQuest();
        maxBoss.StartQuestOrderMaxBoss(BossSpqwnTrs, _bossObj);
    }

    
}
