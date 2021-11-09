using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class OrderMaxQuest : QuestData
{
    private OrderMaxBoss maxBoss = new OrderMaxBoss();

    public override void QuestStart()
    {
        SetProgressQuest();
        
    }
}
