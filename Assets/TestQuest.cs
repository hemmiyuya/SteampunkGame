using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class TestQuest : QuestData
{

    private No1 no1 = new No1();

    
    public override void QuestStart()
    {
        SetProgressQuest();
        no1.StartQuestNo1(transform);
    }

}
