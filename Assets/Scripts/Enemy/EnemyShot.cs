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
    /// true‚Ì@UŒ‚‚·‚é
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

    //e‚ÌUŒ‚ˆ—
    private  void Attack()
    {
        // ’eŠÛ‚Ì•¡»
        GameObject bullets = pool.SetBullet(bulletPrefab, muzzle.position);

        Rigidbody bulletRig = bullets.GetComponent<Rigidbody>();

        // ’eŠÛ‚ÌˆÊ’u‚ğ’²®
        bullets.transform.position = muzzle.position;

        //‘¬“x‚ğ0‚É
        bulletRig.velocity = default;

        //Œü‚«•ÏX
        bullets.transform.LookAt(target);

        // Rigidbody‚É—Í‚ğ‰Á‚¦‚Ä”­Ë
        bulletRig.AddForce(bullets.transform.forward * bulletSpeed,ForceMode.Impulse );

        bullets.GetComponent<Bullet>().muzzlePos = muzzle.position;

    }

    private IEnumerator AttackTimer()
    {
        while (buttle)
        {
            yield return new WaitForSeconds(Random.Range(4f, 10f));

            Attack();
        }

        yield break;
    }

}
