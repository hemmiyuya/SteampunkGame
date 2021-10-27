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
        if (Physics.Raycast(transform.position, transform.forward, out hit,1f))
        {
            gameObject.SetActive(false);
        }
        Debug.DrawRay(transform.position, transform.forward*1, Color.red , 1,true);

        if (Vector3.Distance(transform.position, muzzlePos) > Range) gameObject.SetActive(false);
    }
}
