using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling2 : MonoBehaviour
{
    private RaycastHit hit;
    int layerMask = ~(1 << 8);
    private Rigidbody rig;
    private float force = 1;
    private float addForce = 0.1f;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerObj;

    [SerializeField]
    LineRenderer lineRenderer;

    bool moveFlag = false;

    Vector3[] initialPosition = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] positions;

    void Start()
    {
        rig = playerObj.GetComponent<Rigidbody>();
        lineRenderer.startWidth = 0.1f;                   // 開始点の太さを0.1にする
        lineRenderer.endWidth = 0.1f;                     // 終了点の太さを0.1にする
        positions = initialPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveFlag = GrapplingShot();
            rig.useGravity = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            moveFlag = false;
            rig.useGravity = true;
            positions = initialPosition;
        }
        if (moveFlag)
        {
            positions = new Vector3[] { player.transform.position, hit.point };
            Debug.Log(hit.point + "あたった");
        }
        // 線を引く場所を指定する
        lineRenderer.SetPositions(positions);

    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            GrapplingMove(hit.point, player.transform.position);
        }
    }


    private bool GrapplingShot()
    {
        if (Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(player.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            return true;
        }
        else
        {
            Debug.DrawRay(player.transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        return false;
    }

    private void GrapplingMove(Vector3 target, Vector3 transform)
    {
        Vector3 addVelocity = default;

        if (Input.GetKey(KeyCode.W)) addVelocity += new Vector3(0, addForce, 0);
        if (Input.GetKey(KeyCode.S)) addVelocity += new Vector3(0, -addForce, 0);
        if (Input.GetKey(KeyCode.A)) addVelocity += new Vector3(-addForce, 0, 0);
        if (Input.GetKey(KeyCode.D)) addVelocity += new Vector3(addForce, 0, 0);

        rig.AddForce(((target - transform).normalized + addVelocity) * force, ForceMode.Impulse);
    }
}
