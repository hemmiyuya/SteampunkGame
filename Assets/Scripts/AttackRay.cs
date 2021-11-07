using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRay : MonoBehaviour
{
    [SerializeField]
    private float range = 20;
    [SerializeField]
    Transform muzzle = default;
    private int shotDamage = 14;
    private int slashDamage = 19;
    [SerializeField]
    float radius = 2f;
    [SerializeField]
    float scale = 4;

    RaycastHit hit;
    PlayerSound playerSound = null;

    Vector3    boxRayCenter;
    [SerializeField]
    Vector3 Rectangle=new Vector3(1,1,1);
    [SerializeField]
    float up = 1;

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

    public void Slash()
    {
        boxRayCenter = new Vector3(transform.position.x, transform.position.y + up, transform.position.z);

        RaycastHit[] hitSlash = Physics.BoxCastAll(boxRayCenter, Rectangle * scale, transform.forward, transform.rotation, 1f);

        foreach (RaycastHit hit in hitSlash)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyController>().Damage(slashDamage);
            }
        }
            
        
    }
    void OnDrawGizmos()
    {
        boxRayCenter = new Vector3(transform.position.x, transform.position.y + up, transform.position.z);
        Gizmos.DrawWireCube(boxRayCenter + transform.forward, Rectangle * scale*2);
    }
}
