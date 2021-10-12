using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;
    [Header("エフェクト発生場所")]
    public EffectInfo[] Effects;

    
    private float _gunRevisionRange = 10;
    
    private float _sowdRevisionRange = 5;
    [SerializeField]
    private float _degree = 45;

    [System.Serializable]

    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
    }
    public void InitialSet(GameObject player, GameObject camera)
    {
        _player = player;
        _camera = camera;
        _anim = player.GetComponent<Animationmanager>();
    }
    /// <summary>
    /// 剣基本攻撃
    /// </summary>
    public void PlayerSwordAttack(int combo)
    {
        if (combo == 1)
        {
            Revision(_sowdRevisionRange);
        }
        _anim.SwordAttackStart(combo);
    }
    /// <summary>
    /// 銃基本攻撃
    /// </summary>
    public void PlayerGunAttack(int combo)
    {
        if (combo == 1)
        {
            Revision(_gunRevisionRange);
        }

        _anim.GunAttackStart(combo);
    }

    private void Revision(float range)
    {
        GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");

        enemyObjs = Sort(enemyObjs);

        foreach (GameObject obj in enemyObjs)
        {
            //範囲外ならスキップ
            if (Vector3.Distance(_player.transform.position, obj.transform.position) > range) continue;

            //敵への向き
            Vector3 enemyVec = obj.transform.position - _player.transform.position;

            float dot = Vector3.Dot(_player.transform.forward, enemyVec.normalized);

            if (Mathf.Cos(_degree * Mathf.PI / 180) < dot)
            {
                _player.transform.forward = new Vector3(enemyVec.x, _player.transform.forward.y, enemyVec.z);
                break;
            }
        }

    }
    private GameObject[] Sort(GameObject[] enemyObj)
    {
        bool isEnd = false;
        int finAdjust = 1; // 最終添え字の調整値
        while (!isEnd)
        {
            bool loopSwap = false;
            for (int i = 0; i < enemyObj.Length - finAdjust; i++)
            {
                if (Vector3.Distance(_player.transform.position,enemyObj[i].transform.position)
                    > Vector3.Distance(_player.transform.position, enemyObj[i+1].transform.position))
                {
                    GameObject obj = enemyObj[i];
                    enemyObj[i] = enemyObj[i + 1];
                    enemyObj[i + 1] = obj;
                    loopSwap = true;
                }
            }
            if (!loopSwap) // Swapが一度も実行されなかった場合はソート終了
            {
                isEnd = true;
            }
            finAdjust++;
        }

        return enemyObj;
    }

}
