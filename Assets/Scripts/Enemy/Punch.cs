using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    /// <summary>
    /// trueÇÃéûÅ@çUåÇÇ∑ÇÈ
    /// </summary>
    [SerializeField]
    private bool buttle = true;
    Animator anim;

    Vector3 Rectangle = new Vector3(1, 1, 1);
    float up = 1;
    float scale = 1.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AttackTimer());
    }

    //èeÇÃçUåÇèàóù
    private void Attack()
    {
        anim.SetTrigger("Attack");

    }

    private IEnumerator AttackTimer()
    {
        while (this)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            if (buttle)
                Attack();
        }

        yield break;
    }

    public void Ray()
    {
        Vector3 boxRayCenter = new Vector3(transform.position.x, transform.position.y + up, transform.position.z);

        RaycastHit[] hitPunch = Physics.BoxCastAll(boxRayCenter, Rectangle * scale, transform.forward, transform.rotation, 1f);

        foreach (RaycastHit hit in hitPunch)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("haita ");
                hit.transform.GetComponent<CharacterHp>().Damage(3, 0, transform.position);
            }
        }
    }
    void OnDrawGizmos()
    {
        Vector3 boxRayCenter = new Vector3(transform.position.x, transform.position.y + up, transform.position.z);
        Gizmos.DrawWireCube(boxRayCenter + transform.forward, Rectangle * scale * 2);
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
