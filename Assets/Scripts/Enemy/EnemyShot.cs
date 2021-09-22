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

    [SerializeField]
    private bool buttle = true;

    void Start()
    {
        target = GameObject.FindWithTag("Target").transform;

        StartCoroutine(AttackTimer());
    }

    //e‚ÌUŒ‚ˆ—
    private  void Attack()
    {
        print("ATTACK");
        Vector3 shotVector = target.position - muzzle.position;

        // ’eŠÛ‚Ì•¡»
        GameObject bullets = Instantiate(bulletPrefab) as GameObject;

        // Rigidbody‚É—Í‚ğ‰Á‚¦‚Ä”­Ë
        bullets.GetComponent<Rigidbody>().AddForce(shotVector* bulletSpeed);

        // ’eŠÛ‚ÌˆÊ’u‚ğ’²®
        bullets.transform.position = muzzle.position;

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
