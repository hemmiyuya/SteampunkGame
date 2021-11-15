using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    bool turn = false;
    Transform target;

    Animator anim;
    Rigidbody rig;

    float time;
    float angle;
    float animTime=1.38f;

    public bool gravity = true;
    private bool grandcheck=false;

    [SerializeField]
    private float g_maxDistance = 0.68f;
    [SerializeField]
    Vector3 g_boxSize = new Vector3(0.2f, 0.45f, 0.2f);
    [SerializeField]
    float g_castHight = 0.6f;
    RaycastHit hit;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, g_castHight, 0) + new Vector3(0, -1, 0) * g_maxDistance, g_boxSize * 2);
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /// <summary>
        /// ê⁄ínîªíË
        /// </summary>

        grandcheck = Physics.BoxCast(transform.position + new Vector3(0, g_castHight, 0), g_boxSize, new Vector3(0, -1, 0), out hit, transform.rotation, g_maxDistance, 1);

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

        if (gravity && !grandcheck)
        {
            rig.AddForce(new Vector3(0f, -100f, 0f) * 9.81f, ForceMode.Acceleration);
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
