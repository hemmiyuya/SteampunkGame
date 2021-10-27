using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRay : MonoBehaviour
{
    [SerializeField]
    private float range = 20;
    [SerializeField]
    Transform muzzle = default;
    [SerializeField]
    private int shotDamage = 14;

    PlayerSound playerSound = null;

    private void Start()
    {
        playerSound = GetComponent<PlayerSound>();
    }

    public void Shot()
    {
        playerSound.PlaySE(0);
        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyController>().Damage(shotDamage);
            }
        }

        Debug.DrawRay(muzzle.position, muzzle.forward*range, Color.red, 1, true);
    }
}
