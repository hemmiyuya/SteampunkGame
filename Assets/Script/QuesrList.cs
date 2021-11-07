using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace QuestList
{
    /// <summary>
    /// �X�l3�l�Ƙb��
    /// </summary>
    public class No1 : MonoBehaviour
    {
        
        [SerializeField]
        public static IntReactiveProperty nowTalkCount = new IntReactiveProperty(2);
        public static void TalkCountUp()
        {
            nowTalkCount.Value += 1;
        } 
        private int needTalkCount = 3;
        public void StartQuestNo1(Transform npcTrs)
        {
            nowTalkCount.Value = 0;

            nowTalkCount
                .DistinctUntilChanged()
                .Where(_ => nowTalkCount.Value == needTalkCount)
                .Subscribe(_ => { 
                    npcTrs.GetComponent<QuestData>().QuestClere();
                    nowTalkCount.Dispose();
                } )
                ;

            Debug.Log(nowTalkCount.Value+"�Ȃ��Ɓ[��������Ƃ肹����");
        }

        private void Update()
        {
            Debug.Log(nowTalkCount.Value);
        }

    }
    
}
