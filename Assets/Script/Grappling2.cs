using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling2 : MonoBehaviour
{
    private RaycastHit hit;
    int layerMask = ~(1 << 8);
    private Rigidbody rig;
    private float force = 1;
    private float addForce = 0.4f;

    [SerializeField]
    GameObject player;

    //[SerializeField]
    //GameObject playerObj;

    [SerializeField]
    LineRenderer lineRenderer;

    CharacontrolManager characontrolManager = default;

    bool moveFlag = false;

    Vector3[] initialPosition = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] positions;

    [SerializeField]
    private GameObject anchorObj;
    private GameObject nowAnchor;
    private Transform anchorTransform;

    //アンカーの発射位置と到着位置
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float nowshootTime=default;
    private float nowRemoveTime = default;
    private float nowgrappTime=default;

    private float shootSpeed = 34f;
    private float startEndDistance=default;

    private bool shootFlag = false;
    private bool hitFrag = false;
    private bool remeveanchorFrag = false;
    void Start()
    {
        rig = player.GetComponent<Rigidbody>();
        characontrolManager = player.GetComponent<CharacontrolManager>();
        lineRenderer.startWidth = 0.1f;                   // 開始点の太さを0.1にする
        lineRenderer.endWidth = 0.1f;                     // 終了点の太さを0.1にする
        positions = initialPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& !remeveanchorFrag)
        {
            GrapplingShot();
            //rig.useGravity = false;
            
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            
            //rig.useGravity = true;
            
        }
        if (moveFlag)
        {
            positions = new Vector3[] { player.transform.position, hit.point };
        }
        // 線を引く場所を指定する
        //lineRenderer.SetPositions(positions);

    }

    private void FixedUpdate()
    {
        //グラップル中
        if (moveFlag)
        {
            GrapplingMove(hit.point, player.transform.position);
            if (rig.velocity.magnitude >= 40)
            {
                rig.velocity = rig.velocity * 0.9f;
            }
            lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPosition });
            Debug.Log(Vector3.Distance(player.transform.position, endPosition));

            nowgrappTime += Time.deltaTime;

            if (Vector3.Distance(player.transform.position, endPosition)<5||player.GetComponent<Rigidbody>().velocity.magnitude<=0.3f)
            {
                moveFlag = false;
                nowgrappTime = 0;
                characontrolManager.GravityOn();
                positions = initialPosition;
                remeveanchorFrag = true;
            }
            
        }

        //グラップル発射中
        if (shootFlag)
        {
            Debug.Log("startEndDistance"+startEndDistance);

            nowshootTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;

            float present_Location = (nowshootTime * shootSpeed) / startEndDistance;

            anchorTransform.position = Vector3.Lerp(startPosition, endPosition, present_Location);
            lineRenderer.SetPositions(new Vector3[] { new Vector3(player.transform.position.x,player.transform.position.y+0.5f,player.transform.position.z), anchorTransform.position });
            if (endPosition == anchorTransform.position)
            {
                if (hitFrag)
                {
                    moveFlag = true;
                    characontrolManager.GravityOff();
                    player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
                }
                else
                {
                    remeveanchorFrag = true;
                }
                nowshootTime = 0;
                //GrapplingMove(hit.point, player.transform.position);
                shootFlag = false;
                present_Location = 0;
            }
        }

        //グラップルフック外し中
        if (remeveanchorFrag)
        {
            Debug.Log("Remove");
            nowRemoveTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;

            float present_Location2 = (nowRemoveTime * shootSpeed) / startEndDistance;

            Vector3 nowPlayerPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);

            anchorTransform.position = Vector3.Lerp(endPosition, nowPlayerPos, present_Location2);

            lineRenderer.SetPositions(new Vector3[] { anchorTransform.position, nowPlayerPos });

            if (anchorTransform.position == nowPlayerPos)
            {
                Debug.Log("一連終わり!");
                GameObject.Destroy(nowAnchor);
                remeveanchorFrag = false;
                present_Location2 = 0;
            }
        }
    }


    private bool GrapplingShot()
    {
        startPosition =new Vector3( player.transform.position.x,player.transform.position.y+0.5f,player.transform.position.z);
        

        if (Physics.Raycast(startPosition, transform.TransformDirection(Vector3.back), out hit, 35, layerMask))
        {
            Debug.DrawRay(startPosition, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            endPosition = hit.point;
            startEndDistance = Vector3.Distance(startPosition, endPosition);
            nowAnchor = Instantiate(anchorObj, player.transform.position, player.transform.rotation);
            shootFlag = true;
            hitFrag = true;
            return true;
        }
        else
        {
            Debug.DrawRay(startPosition, transform.TransformDirection(Vector3.back) * 35, Color.white);
            Debug.Log("Did not Hit");
            endPosition = transform.TransformDirection(Vector3.forward) * 35;
            Debug.Log(endPosition);
            nowAnchor = Instantiate(anchorObj, startPosition, player.transform.rotation);
        }
        startEndDistance = Vector3.Distance(startPosition, endPosition);
        shootFlag = true;
        hitFrag = false;
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
