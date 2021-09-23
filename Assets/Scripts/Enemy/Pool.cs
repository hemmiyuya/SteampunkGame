using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Transform bulletPool = null;

    private void Start()
    {
        bulletPool = new GameObject("bulletPool").transform;
    }

    public GameObject SetBullet(GameObject obj, Vector3 pos)
    {
        Transform pool = bulletPool;

        foreach (Transform bullet in pool)
        {
            //�I�u�W�F����A�N�e�B�u�Ȃ�g����
            if (!bullet.gameObject.activeSelf)
            {
                bullet.SetPositionAndRotation(pos, Quaternion.identity);
                bullet.gameObject.SetActive(true);
                return bullet.gameObject;
            }
        }
        //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��Ȃ琶��
        return Instantiate(obj, pos, Quaternion.identity, pool);
    }
}