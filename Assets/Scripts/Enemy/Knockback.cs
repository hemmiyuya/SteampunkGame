using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Animator anim;

    EnemyController enemyController;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    public void StopEnemy()
    {
        anim.enabled = false;
        anim.SetLayerWeight(anim.GetLayerIndex("Aim"), 0);
    }

    public void KnockbackEnemy(GameObject player)
    {
        anim.enabled = true;

        enemyController.lookatFlag = false;
        transform.LookAt(player.transform);

        if (this.name == "SteamGirl(Clone)")
        {

            anim.SetTrigger("Knockback");

            StartCoroutine(MoveBack());
        }
        else
            anim.SetTrigger("Damage");
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
        enemyController.lookatFlag = true;
        yield break;
    }
}
