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
            //オブジェが非アクティブなら使い回し
            if (!bullet.gameObject.activeSelf)
            {
                bullet.SetPositionAndRotation(pos, Quaternion.identity);
                bullet.gameObject.SetActive(true);
                return bullet.gameObject;
            }
        }
        //非アクティブなオブジェクトがないなら生成
        return Instantiate(obj, pos, Quaternion.identity, pool);
    }
}