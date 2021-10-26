using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StopEnemy()
    {
        anim.enabled = false;
        anim.SetLayerWeight(anim.GetLayerIndex("Aim"), 0);
    }

    public void KnockbackEnemy(GameObject player)
    {
        anim.enabled = true;

        transform.LookAt(player.transform);

        anim.SetTrigger("Knockback");

        StartCoroutine(MoveBack());

    }
    private IEnumerator MoveBack()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.Translate(0, 0, -0.075f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        anim.SetLayerWeight(anim.GetLayerIndex("Aim"), 1);
        yield break;
    }
}
