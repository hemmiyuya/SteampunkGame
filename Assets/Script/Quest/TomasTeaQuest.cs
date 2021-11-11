using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestList;

public class TomasTeaQuest : QuestData
{

    private TomasTea _tomasTea=new TomasTea();

    [SerializeField]
    TomasTeaCup teaCup = default;

    public override void QuestStart()
    {
        SetProgressQuest();
        _tomasTea.StartQuestTomasTea();
        teaCup.QuestFlagON();
    }

}
