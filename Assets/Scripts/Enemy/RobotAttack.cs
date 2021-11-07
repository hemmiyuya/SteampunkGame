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

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    public IEnumerator SpinAttack(float time)
    {

        anim.SetBool("spin",true);
        timer = 0;
        //èôÅXÇ…ëÅÇ≠âÒì]
        while (timer < 2)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            float interpolatedValue = timer / 2;

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            anim.SetFloat("SpinSpeed", Mathf.Lerp(0.2f, 0.6f, interpolatedValue));
        }

        ColliderOn();

        timer = 0;
        while (time > timer)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }

        ColliderOff();

        timer = 2;

        //èôÅXÇ…íxÇ≠
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
        timer = 0;
        Vector3 center = new Vector3(0,1,0);
        startPosition -= center;
        targetPosition -= center;

        while (time > timer)
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
        collider.SetActive(true);
    }

    public void ColliderOff()
    {
        collider.SetActive(false);
    }
}
