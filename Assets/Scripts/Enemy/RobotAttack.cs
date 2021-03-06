using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    Animator anim;

    float timer;

    GameObject player;

    [SerializeField]
    GameObject collider;
    [SerializeField]
    GameObject[] efects;

    EnemyHp enemyHp;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        enemyHp = GetComponent<EnemyHp>();
    }

    public IEnumerator SpinAttack(float time)
    {

        anim.SetBool("spin",true);
        timer = 0;
        //???X?ɑ??????]
        while (timer < 2)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            float interpolatedValue = timer / 2;

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            anim.SetFloat("SpinSpeed", Mathf.Lerp(0.2f, 0.6f, interpolatedValue));
        }


        collider.SetActive(true);
        efects[0].SetActive(true);
        

        timer = 0;
        while (time > timer && enemyHp.GetHp() > 0)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }

        efects[0].SetActive(false);
        ColliderOff();

        timer = 2;

        //???X?ɒx??
        while (timer > 0)
        {
            yield return new WaitForSeconds(0.01f);
            timer -= 0.01f;
            float interpolatedValue = timer / 2;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            anim.SetFloat("SpinSpeed", Mathf.Lerp(0.2f, 0.6f, interpolatedValue));
        }
        anim.SetBool("spin", false);
        yield break;
    }

    public IEnumerator CrashAttack()
    {
        anim.SetTrigger("crash");
        yield return new WaitForSeconds(3f);
        yield break;
    }


    public IEnumerator JumpAttack(Vector3 startPosition, Vector3 targetPosition, float time)
    {
        anim.SetTrigger("jump");

        Destroy( Instantiate(efects[1], targetPosition, Quaternion.identity),2f);

        yield return new WaitForSeconds(1);


        timer = 0;
        Vector3 center = new Vector3(0,0.1f,0);
        startPosition -= center;
        targetPosition -= center;

        while (time > timer&& enemyHp.GetHp() > 0)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            float interpolatedValue= timer / time;

            transform.position = Vector3.Slerp(startPosition, targetPosition, interpolatedValue)+center;
        }
        yield break;
    }


    public void ColliderOn()
    {
        Destroy(Instantiate(efects[2], transform.position, Quaternion.identity),0.7f);
        collider.SetActive(true);
    }

    public void ColliderOff()
    {
        collider.SetActive(false);
    }
}
