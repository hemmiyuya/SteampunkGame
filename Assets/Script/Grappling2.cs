using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grappling2 : MonoBehaviour
{
    private RaycastHit hit;
    private RaycastHit hit2;
    int layerMask = ~(1 << 7);
    private Rigidbody rig;
    private float force = 2;
    private float firstForce = 25f;
    private float addForce = 0.32f;

    [SerializeField]
    Transform grapMuzzle;
    GameObject mainCamara;
    GameObject playerCamera;

    private Animator playerAnim;

    //[SerializeField]
    //GameObject playerObj;

    [SerializeField]
    LineRenderer lineRenderer;

    CharacontrolManager characontrolManager = default;

    bool moveFlag = false;

    [SerializeField]
    private GameObject anchorObj;
    private GameObject nowAnchor;
    private Transform anchorTransform;

    //アンカーレイの発射位置と到着位置
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float nowshootTime=default;
    private float nowRemoveTime = default;
    private float nowgrappTime=default;

    private float shootSpeed = 55f;
    private float removeSpeed = 70f;
    private float startEndDistance=default;

    [SerializeField]
    private float useGasValue = default;

    private bool shootFlag = false;
    private bool hitFrag = false;
    private bool firstFrag = false;
    private bool removeanchorFrag = false;
    private bool VisibilityNow = false;

    [SerializeField]
    private Image reticle=default;

    [SerializeField]
    private Sprite[] reticleSprites = default;

    [SerializeField]
    private GasGaugeManager gasGaugeManager=default;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        characontrolManager = GetComponent<CharacontrolManager>();
        lineRenderer.startWidth = 0.02f;                   //線の太さ
        lineRenderer.endWidth = 0.02f;                     
        playerAnim = GetComponent<Animator>();
        mainCamara = GameObject.FindGameObjectWithTag("MainCamera");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& !removeanchorFrag&&!moveFlag&& !shootFlag)
        {
            if (gasGaugeManager.UseGasCheck(useGasValue))
            {
                GrapplingShot();
            }
            
            //rig.useGravity = false;
            
        }

        // 線を引く場所を指定する
        //lineRenderer.SetPositions(positions);

        //グラップル発射可能かつ発射したときに刺せる位置ならレティクルをさせるよー表示にする
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+ 0.8f,transform.position.z), transform.TransformDirection(Vector3.back), out hit2, 35, layerMask)
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
            GrapplingMove(hit.point, transform.position);
            if (rig.velocity.magnitude >= 35)
            {
                rig.velocity = rig.velocity * 0.9f;
            }
            lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });

            nowgrappTime += Time.deltaTime;

            if (Vector3.Distance(transform.position, endPosition)<2||!VisibilityNow||(nowgrappTime>=0.5f&&rig.velocity.magnitude<=0.5f))
            {
                moveFlag = false;
                nowgrappTime = 0;
                characontrolManager.GravityOn();
                removeanchorFrag = true;
            }
            
        }

        //グラップル発射中
        if (shootFlag)
        {
            nowshootTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;

            float present_Location = nowshootTime * shootSpeed / startEndDistance;

            anchorTransform.position = Vector3.Lerp(grapMuzzle.position, endPosition, present_Location);
            lineRenderer.SetPositions(new Vector3[] { new Vector3(transform.position.x,transform.position.y+0.8f,transform.position.z), anchorTransform.position });
            if (endPosition == anchorTransform.position)
            {
                if (hitFrag)
                {
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

            Vector3 nowPlayerPos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);

            anchorTransform.position = Vector3.Lerp(endPosition, nowPlayerPos, present_Location2);

            lineRenderer.SetPositions(new Vector3[] { anchorTransform.position, nowPlayerPos });

            if (anchorTransform.position == nowPlayerPos)
            {
                GameObject.Destroy(nowAnchor);
                removeanchorFrag = false;
                firstFrag = true;
                nowRemoveTime = 0;
                present_Location2 = 0;
                playerAnim.SetTrigger("GrapComp");
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (shootFlag)
        {
            playerAnim.SetLookAtWeight(1f, 1f, 1f, 0f, 0.5f);     // LookAtの調整
            playerAnim.SetLookAtPosition(endPosition);          // ターゲットの方向を向く
        }

    }

    /// <summary>
    /// グラップルショット
    /// </summary>
    private void GrapplingShot()
    {
        startPosition = mainCamara.transform.position 
                        += transform.position - mainCamara.transform.position;

        //方向を合わせる
        transform.forward = new Vector3(mainCamara.transform.forward.x, transform.forward.y, mainCamara.transform.forward.z);

        playerAnim.SetTrigger("GrapShot");

        if (Physics.Raycast(startPosition, playerCamera.transform.TransformDirection(Vector3.back), out hit, 35, layerMask))
        {
            print(hit.transform.name);
            Debug.DrawRay(startPosition, playerCamera.transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);

            endPosition = hit.point;
            startEndDistance = Vector3.Distance(startPosition, endPosition);
            nowAnchor = Instantiate(anchorObj, startPosition, transform.rotation);
            hitFrag = true;
            shootFlag = true;

            playerAnim.SetTrigger("GrapHit");
            return;
        }
        else
        {
            Debug.DrawRay(startPosition, playerCamera.transform.TransformDirection(Vector3.back) * 20, Color.white);
            endPosition =  transform.position+ playerCamera.transform.TransformDirection(Vector3.back) * 20;
            nowAnchor = Instantiate(anchorObj, startPosition, transform.rotation);
            playerAnim.SetTrigger("GrapEnd");
        }
        startEndDistance = Vector3.Distance(startPosition, endPosition);
        shootFlag = true;
        hitFrag = false;
    }

    private void GrapplingMove(Vector3 target, Vector3 transform)
    {
        Vector3 addVelocity = default;

        if (Input.GetKey(KeyCode.W)) addVelocity +=rig.transform.up*addForce;
        //if (Input.GetKey(KeyCode.S)) addVelocity += (-rig.transform.up)* addForce;
        if (Input.GetKey(KeyCode.A)) addVelocity +=(-rig.transform.right)* addForce;
        if (Input.GetKey(KeyCode.D)) addVelocity +=rig.transform.right* addForce;

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

    //アニメーションイベントで呼び出し
    public void MoveStart()
    {
        moveFlag = true;
    }
}
