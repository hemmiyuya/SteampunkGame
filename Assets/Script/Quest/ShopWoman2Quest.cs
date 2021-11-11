using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class ShopWoman2Quest : QuestData
{
    private ShopWoman2Sisters shopWoman2Sisters=new ShopWoman2Sisters();

    [SerializeField]
    private Transform _enemySpawn;
    [SerializeField]
    private GameObject _enemys;

    public override void QuestStart()
    {
        SetProgressQuest();
        shopWoman2Sisters.StartQuest(_enemySpawn,_enemys);
    }

    public override void QuestClere()
    {
        GetComponent<ShopWoman2>()._questEndFlag = true;
        GetComponent<ShopWoman2>().QuestHaveFlag = false;
        base.QuestClere();
    }
}
