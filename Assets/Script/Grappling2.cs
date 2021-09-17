using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grappling2 : MonoBehaviour
{
    private RaycastHit hit;
    private RaycastHit hit2;
    int layerMask = ~(1 << 8);
    private Rigidbody rig;
    private float force = 1;
    private float firstForce = 20f;
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

    private float shootSpeed = 40f;
    private float removeSpeed = 50f;
    private float startEndDistance=default;

    private bool shootFlag = false;
    private bool hitFrag = false;
    private bool firstFrag = false;
    private bool removeanchorFrag = false;
    private bool VisibilityNow = false;

    [SerializeField]
    private Image reticle=default;

    [SerializeField]
    private Sprite[] reticleSprites = default;
    void Start()
    {
        rig = player.GetComponent<Rigidbody>();
        characontrolManager = player.GetComponent<CharacontrolManager>();
        lineRenderer.startWidth = 0.02f;                   //線の太さ
        lineRenderer.endWidth = 0.02f;                     
        positions = initialPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& !removeanchorFrag&&!moveFlag&& !shootFlag)
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

        //グラップル発射可能かつ発射したときに刺せる位置ならレティクルをさせるよー表示にする
        if (Physics.Raycast(new Vector3(player.transform.position.x,player.transform.position.y+ 0.8f,player.transform.position.z), transform.TransformDirection(Vector3.back), out hit2, 35, layerMask)
            &&!removeanchorFrag && !moveFlag && !shootFlag)
        {
            reticle.sprite = reticleSprites[0];
        }
        else
        {
            reticle.sprite = reticleSprites[1];
        }


    }

    private void FixedUpdate()
    {
        //グラップル中
        if (moveFlag)
        {
            GrapplingMove(hit.point, player.transform.position);
            if (rig.velocity.magnitude >= 35)
            {
                rig.velocity = rig.velocity * 0.9f;
            }
            lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPosition });

            nowgrappTime += Time.deltaTime;

            if (Vector3.Distance(player.transform.position, endPosition)<2||!VisibilityNow||(nowgrappTime>=0.5f&&rig.velocity.magnitude<=0.5f))
            {
                moveFlag = false;
                nowgrappTime = 0;
                characontrolManager.GravityOn();
                positions = initialPosition;
                removeanchorFrag = true;
            }
            
        }

        //グラップル発射中
        if (shootFlag)
        {

            nowshootTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;

            float present_Location = nowshootTime * shootSpeed / startEndDistance;

            anchorTransform.position = Vector3.Lerp(startPosition, endPosition, present_Location);
            lineRenderer.SetPositions(new Vector3[] { new Vector3(player.transform.position.x,player.transform.position.y+0.8f,player.transform.position.z), anchorTransform.position });
            if (endPosition == anchorTransform.position)
            {
                if (hitFrag)
                {
                    moveFlag = true;
                    characontrolManager.GravityOff();
                    rig.velocity = rig.velocity * 0.1f;
                    VisibilityNow = true;
                    anchorTransform.GetComponent<GrappInCamera>().StartJudgeInCamera();
                }
                else
                {
                    removeanchorFrag = true;
                }
                nowshootTime = 0;
                //GrapplingMove(hit.point, player.transform.position);
                shootFlag = false;
                present_Location = 0;
            }
        }

        //グラップルフック外し中
        if (removeanchorFrag)
        {
            nowRemoveTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;

            float present_Location2 = nowRemoveTime * removeSpeed / startEndDistance;

            Vector3 nowPlayerPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z);

            anchorTransform.position = Vector3.Lerp(endPosition, nowPlayerPos, present_Location2);

            lineRenderer.SetPositions(new Vector3[] { anchorTransform.position, nowPlayerPos });

            if (anchorTransform.position == nowPlayerPos)
            {
                GameObject.Destroy(nowAnchor);
                removeanchorFrag = false;
                firstFrag = true;
                nowRemoveTime = 0;
                present_Location2 = 0;
            }
        }
    }

    /// <summary>
    /// グラップルショット
    /// </summary>
    /// <returns></returns>
    private bool GrapplingShot()
    {
        startPosition =new Vector3( player.transform.position.x,player.transform.position.y+0.8f,player.transform.position.z);
        

        if (Physics.Raycast(startPosition, transform.TransformDirection(Vector3.back), out hit, 35, layerMask))
        {
            Debug.DrawRay(startPosition, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);

            endPosition = hit.point;
            startEndDistance = Vector3.Distance(startPosition, endPosition);
            nowAnchor = Instantiate(anchorObj, startPosition, player.transform.rotation);
            shootFlag = true;
            hitFrag = true;
            return true;
        }
        else
        {
            Debug.DrawRay(startPosition, transform.TransformDirection(Vector3.back) * 20, Color.white);
            endPosition =  player.transform.position+ transform.TransformDirection(Vector3.back) * 20;
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

        if (firstFrag)
        {
            rig.AddForce(((target - transform).normalized + addVelocity) * firstForce, ForceMode.Impulse);
            firstFrag = false;
        }
        rig.AddForce(((target - transform).normalized + addVelocity) * force, ForceMode.Impulse);
        
    }


    /// <summary>
    /// アンカーが視界から外れたら外すようにする
    /// </summary>
    public void OutOfVisibility()
    {
        VisibilityNow = false;
    }
}
