using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Range = 30;

    public Vector3 muzzlePos = default;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, transform.localScale.x/2, transform.forward, out hit, 0.1f))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<CharacterHp>().Damage(10,0,transform.position);
            }

            gameObject.SetActive (false);

            print("aaaaaaaaa   "+hit.transform.name);
        }

        if (Vector3.Distance(transform.position, muzzlePos) > Range) gameObject.SetActive(false);
    }
}
