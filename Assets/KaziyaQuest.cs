using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class KaziyaQuest : QuestData
{
    [SerializeField]
    private Transform _enemySpawn;
    [SerializeField]
    private GameObject _enemys;

    private KaziyaQuest1 _kaziyaQuest1 = new KaziyaQuest1();

    public override void QuestStart()
    {
        SetProgressQuest();
        _kaziyaQuest1.StartQuest(_enemySpawn, _enemys);
    }

    public override void QuestClere()
    {
        base.QuestClere();
    }
}
