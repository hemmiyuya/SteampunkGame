using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle = null;

    private Transform target = null;

    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private float bulletSpeed = 100f;

    /// <summary>
    /// trueÌ@U·é
    /// </summary>
    [SerializeField]
    private bool buttle = true;

    private Pool pool = null;

    void Start()
    {
        target = GameObject.FindWithTag("Target").transform;

        pool = GameObject.FindWithTag("BulletPool").GetComponent<Pool>();
        StartCoroutine(AttackTimer());
    }

    //eÌU
    private  void Attack()
    {
        // eÛÌ¡»
        GameObject bullets = pool.SetBullet(bulletPrefab, muzzle.position);

        Rigidbody bulletRig = bullets.GetComponent<Rigidbody>();

        // eÛÌÊuð²®
        bullets.transform.position = muzzle.position;

        //¬xð0É
        bulletRig.velocity = default;

        //ü«ÏX
        bullets.transform.LookAt(target);

        // RigidbodyÉÍðÁ¦Ä­Ë
        bulletRig.AddForce(bullets.transform.forward * bulletSpeed,ForceMode.Impulse );

        bullets.GetComponent<Bullet>().muzzlePos = muzzle.position;
    }

    private IEnumerator AttackTimer()
    {
        while (this)
        {
            yield return new WaitForSeconds(Random.Range(4f, 10f));
            if(buttle)
                Attack();
        }

        yield break;
    }

    public void AttackOn()
    {
        buttle = true;
    }
    public void AttackOff()
    {
        buttle = false;
    }

}
