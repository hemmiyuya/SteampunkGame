using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;
    [Header("�G�t�F�N�g�����ꏊ")]
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
    /// ����{�U��
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
    /// �e��{�U��
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
            //�͈͊O�Ȃ�X�L�b�v
            if (Vector3.Distance(_player.transform.position, obj.transform.position) > range) continue;

            //�G�ւ̌���
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
        int finAdjust = 1; // �ŏI�Y�����̒����l
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
            if (!loopSwap) // Swap����x�����s����Ȃ������ꍇ�̓\�[�g�I��
            {
                isEnd = true;
            }
            finAdjust++;
        }

        return enemyObj;
    }

}
