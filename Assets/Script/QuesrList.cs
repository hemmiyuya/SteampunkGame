using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace QuestList
{
    /// <summary>
    /// 街人3人と話す
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

            Debug.Log(nowTalkCount.Value+"なうとーくかうんとりせっと");
        }

    }

    /// <summary>
    /// 治安レベルがマックスのときに出現ボス
    /// </summary>
    public class OrderMaxBoss : MonoBehaviour
    {
        
        /// <summary>
        /// ボス出現させる
        /// </summary>
        /// <param name="transform">ボスの出現場所</param>
        public void StartQuestOrderMaxBoss(Transform spawnTrs,GameObject bossObj)
        {
            Instantiate(bossObj, spawnTrs);
        }
    }

    /// <summary>
    /// 治安レベルが最低のときに出現ボス
    /// </summary>
    public class OrderZeroBoss : MonoBehaviour
    {

    }

    /// <summary>
    /// トーマスのティーポットを探す
    /// </summary>
    public class TomasTea : MonoBehaviour
    {

        public void StartQuestTomasTea()
        {
            
        }

    }

    /// <summary>
    /// 妹の店の近くのごろつきを追い払う
    /// </summary>
    public class ShopWoman2Sisters : MonoBehaviour
    {
        public void StartQuest(Transform spawnTrs, GameObject enemyobj)
        {
            Instantiate(enemyobj, spawnTrs);
        }
    }
    
}
