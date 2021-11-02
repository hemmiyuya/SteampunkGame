using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    bool turn = false;
    Transform target;

    Animator anim;

    float time;
    float angle;
    float animTime=1.38f;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (turn)
        {
            time += Time.deltaTime;
            transform.rotation= Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,angle,0f), time/animTime);
            if(time> animTime)
            {
                turn = false;
                anim.SetBool("turn", false);
            }
        }
    }

    public void Turn(Transform player)
    {
        time = 0;
        target = player;
        turn = true;

        var diff = target.position - transform.position;

        var axis = Vector3.Cross(Vector3.forward, diff);

        angle = Vector3.Angle(Vector3.forward, diff) * (axis.y < 0 ? -1 : 1);

        anim.SetBool("turn", true);
        anim.SetFloat("TurnBlend", axis.y < 0 ? -1 : 1);
    }
}
