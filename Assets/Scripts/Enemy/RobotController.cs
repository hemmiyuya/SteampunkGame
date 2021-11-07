using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField,Header ("çUåÇîÕàÕ")]
    float range=10;

    float distance = 0;

    RobotAttack robotAttack;
    RobotMove robotMove;
    Animator animator;

    Transform playerTrs;
    string playerTag = "Player";

    bool action;

    float jumpTime=1f;
    float spinTime=5f;

    private void Start()
    {
        robotAttack = GetComponent<RobotAttack>();
        robotMove = GetComponent<RobotMove>();
        animator = GetComponent<Animator>();
        playerTrs = GameObject.FindWithTag(playerTag).transform;
        StartCoroutine(Distance());
        StartCoroutine(Action());
    }

    private void Update()
    {
        //éÀíˆäOÇ»ÇÁà⁄ìÆ
        if (distance > range)
        {
            robotMove.Turn(playerTrs);
            animator.SetBool("walk",true);
            action = false;
        }
        //îÕàÕì‡Ç»ÇÁçUåÇ
        else
        {
            action = true;
            animator.SetBool("walk", false);
            animator.SetBool("turn", false);
        }
    }

    private IEnumerator Action()
    {
        while (this)
        {
            yield return new WaitForSeconds(3f);
            if (action)
            {
                int random = Random.Range(0, 3);
                if (random == 0)
                {
                    robotMove.Turn(playerTrs);

                    yield return StartCoroutine(robotAttack.JumpAttack(transform.position, playerTrs.position, jumpTime));
                }
                else if (random == 1)
                    yield return StartCoroutine(robotAttack.SpinAttack(spinTime));
                else if (random == 2)
                    yield return StartCoroutine(robotAttack.CrashAttack());

            }
        }
        
        yield break;
    }

    private IEnumerator Distance()
    {
        while (this) 
        {
            distance = Vector3.Distance(playerTrs.position, transform.position);
            yield return new WaitForSeconds(1);
        }
        yield break;
    }
}
