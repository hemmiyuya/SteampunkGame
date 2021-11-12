using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class ShopWoman2Quest2 : QuestData
{
    private ShopWoman1Sisters shopWoman1Sisters =new ShopWoman1Sisters();

    [SerializeField]
    private Transform _enemySpawn;
    [SerializeField]
    private GameObject _enemys;

    public override void QuestStart()
    {
        SetProgressQuest();
        shopWoman1Sisters.StartQuest(_enemySpawn,_enemys);
    }

    public override void QuestClere()
    {
        GetComponent<NPCData>()._questEndFlag = true;
        GetComponent<NPCData>().QuestHaveFlag = false;
        base.QuestClere();
    }
}
