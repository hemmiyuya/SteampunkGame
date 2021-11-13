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

        }

    }

    /// <summary>
    /// �������x�����}�b�N�X�̂Ƃ��ɏo���{�X
    /// </summary>
    public class OrderMaxBoss : MonoBehaviour
    {
        
        /// <summary>
        /// �{�X�o��������
        /// </summary>
        /// <param name="transform">�{�X�̏o���ꏊ</param>
        public void StartQuestOrderMaxBoss(Transform spawnTrs,GameObject bossObj)
        {
            Instantiate(bossObj, spawnTrs);
        }
    }

    /// <summary>
    /// �������x�����Œ�̂Ƃ��ɏo���{�X
    /// </summary>
    public class OrderZeroBoss : MonoBehaviour
    {

    }

    /// <summary>
    /// �g�[�}�X�̃e�B�[�|�b�g��T��
    /// </summary>
    public class TomasTea : MonoBehaviour
    {

        public void StartQuestTomasTea()
        {
            
        }

    }

    /// <summary>
    /// ���̓X�̋߂��̂������ǂ�����
    /// </summary>
    public class ShopWoman2Sisters : MonoBehaviour
    {
        GameObject _enemys;
        public void StartQuest(Transform spawnTrs, GameObject enemyobj)
        {
            _enemys = enemyobj;
            Instantiate(_enemys, spawnTrs);
        }


    }

    /// <summary>
    /// �S���c�L�����̕��K��j�~����
    /// </summary>
    public class ShopWoman1Sisters : MonoBehaviour
    {
        public void StartQuest(Transform spawnTrs, GameObject enemyobj)
        {
            Instantiate(enemyobj, spawnTrs);
        }
    }

    public class KaziyaQuest1 : MonoBehaviour
    {
        public void StartQuest(Transform spawnTrs, GameObject enemyobj)
        {
            Instantiate(enemyobj, spawnTrs);
        }
    }
    
}
