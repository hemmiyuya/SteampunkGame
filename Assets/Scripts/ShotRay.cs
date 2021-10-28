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
    [SerializeField]
    float radius = 2f;
    RaycastHit hit;
    bool isEnable = false;
    PlayerSound playerSound = null;

    private void Start()
    {
        playerSound = GetComponent<PlayerSound>();
    }

    public void Shot()
    {
        Vector3 vecter = new Vector3(muzzle.forward.x, 0, muzzle.forward.z);
        playerSound.PlaySE(0);
        if (Physics.SphereCast(muzzle.position, radius, vecter, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyController>().Damage(shotDamage);
            }
        }
    }
}
