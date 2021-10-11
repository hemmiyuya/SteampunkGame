using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Grappling2 : MonoBehaviour
{
    [SerializeField]
    private int lineVertexCount = 100;
    [SerializeField]
    private int wobbleCount = 1;

    private RaycastHit hit;
    private RaycastHit hit2;
    int layerMask = ~(1 << 7);
    private Rigidbody rig;
    private float force = 2;
    private float firstForce = 27f;
    private float addForce = 0.32f;
    [SerializeField]
    float rayRange = 30;
    [SerializeField]
    Transform grapMuzzle;
    GameObject mainCamera;
    GameObject playerCamera;

    private Animator playerAnim;
    private Animationmanager anim;

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

    private float present_Location;
    private float present_Location2;
    private float nowshootTime=default;
    private float nowRemoveTime = default;
    private float nowgrappTime=default;

    private float shootSpeed = 55f;
    private float removeSpeed = 70f;

    /// <summary>
    /// グラップルの開始位置から終了位置までの長さ
    /// </summary>
    private float startEndDistance=default;

    [SerializeField]
    private float useGasValue = default;

    private bool shotReady = false;
    private bool shootFlag = false;
    private bool firstFrag = false;
    private bool removeanchorFrag = false;
    private bool VisibilityNow = false;

    private bool lockatFlag = false;

    private bool grapplingNow = false;

    /// <summary>
    /// グラップルしてるかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool UsingGrapp
    {
        get 
        {
            return grapplingNow;
        }
    }

    public Vector3 NowEndPos
    {
        get { return endPosition; }
    }


    [SerializeField]
    private Image reticle=default;

    [SerializeField]
    private Sprite[] reticleSprites = default;

    [SerializeField]
    private GasGaugeManager gasGaugeManager=default;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        characontrolManager = GetComponent<CharacontrolManager>();
        lineRenderer.startWidth = 0.02f;                   //線の太さ
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = lineVertexCount;
        anim = GetComponent<Animationmanager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !removeanchorFrag && !moveFlag && !shootFlag && !characontrolManager.GetAtacckingFlag()
            &&shotReady&&!grapplingNow)
        {
            GrapplingShot();
        }


        //グラップル発射可能かつ発射したときに刺せる位置ならレティクルをさせるよー表示にする
        if (Physics.Raycast(mainCamera.transform.position
                        + playerCamera.transform.position - mainCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.back), out hit, rayRange, layerMask)
            && !removeanchorFrag && !moveFlag && !shootFlag)
        {
            shotReady = true;
            reticle.sprite = reticleSprites[0];
        }
        else
        {
            shotReady = false;
            reticle.sprite = reticleSprites[1];
        }

        if (shootFlag)
        {
            nowshootTime += Time.deltaTime;
            anchorTransform = nowAnchor.transform;

            present_Location = nowshootTime * shootSpeed / startEndDistance;

            if (endPosition == anchorTransform.position)
            {
                anim.GrapHit();
                characontrolManager.GravityOff();
                rig.velocity = rig.velocity * 0.1f;
                VisibilityNow = true;
                anchorTransform.GetComponent<GrappInCamera>().StartJudgeInCamera();

                nowshootTime = 0;
                shootFlag = false;
                present_Location = 0;
            }
        }

        if (moveFlag)
        {
            nowgrappTime += Time.deltaTime;
            if (Vector3.Distance(transform.position, endPosition) < 10 || !VisibilityNow || (nowgrappTime >= 0.5f && rig.velocity.magnitude <= 0.5f))
            {
                moveFlag = false;
                nowgrappTime = 0;
                characontrolManager.GravityOn();
                removeanchorFrag = true;
                lockatFlag = false;
                anim.GrapComp();
            }
        }

        //グラップルフック外し中
        if (removeanchorFrag)
        {
            nowRemoveTime += Time.deltaTime;

            anchorTransform = nowAnchor.transform;
            present_Location2 = nowRemoveTime * removeSpeed / startEndDistance;

            if (Vector3.Distance( anchorTransform.position , grapMuzzle.position)<1)
            {
                GameObject.Destroy(nowAnchor);
                removeanchorFrag = false;
                grapplingNow = false;
                firstFrag = true;
                nowRemoveTime = 0;
                present_Location2 = 0;
            }
        }
    }

    private Spring spring;
    private Vector3 currentGrapplePosition;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;

    private void Awake()
    {
        spring = new Spring();
        spring.SetTarget(0);
    }
    private void OnRenderObject()
    {
        if(!grapplingNow && !removeanchorFrag)
        {
            currentGrapplePosition = grapMuzzle.position;
            spring.Reset();
            if (lineRenderer.positionCount > 0)
                lineRenderer.positionCount = 0;
            return;
        }
        if (lineRenderer.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lineRenderer.positionCount = quality + 1;
        }

        currentGrapplePosition = anchorTransform.position;
        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);
        var grapplePoint = endPosition;
        var up = Quaternion.LookRotation((grapplePoint - grapMuzzle.position).normalized) * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for (var i = 0; i < quality + 1; i++)
        {
            var delta = i / (float)quality;
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                         affectCurve.Evaluate(delta);

            lineRenderer.SetPosition(i, Vector3.Lerp(grapMuzzle.position, currentGrapplePosition, delta) + offset);
        }
    }

    private void FixedUpdate()
    {
        //グラップル発射中
        if (shootFlag)
        {
            anchorTransform.position = Vector3.Lerp(grapMuzzle.position, endPosition, present_Location);
        }

        //グラップル中
        if (moveFlag)
        {
            GrapplingMove(endPosition, transform.position);
            if (rig.velocity.magnitude >= 35)
            {
                rig.velocity = rig.velocity * 0.9f;
            }
        }

        //グラップルフック外し中
        if (removeanchorFrag)
        {
            anchorTransform.position = Vector3.Lerp(endPosition, grapMuzzle.position, present_Location2);
        }


    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (lockatFlag)
        {
            playerAnim.SetLookAtWeight(1f, 1f, 1f, 0f, 0.75f);     // LookAtの調整
            playerAnim.SetLookAtPosition(endPosition);          // ターゲットの方向を向く
        }
    }

    /// <summary>
    /// グラップルショット
    /// </summary>
    private void GrapplingShot()
    {
        startPosition = mainCamera.transform.position 
                        + playerCamera.transform.position - mainCamera.transform.position;

        //方向を合わせる
        transform.forward = new Vector3(mainCamera.transform.forward.x, transform.forward.y, mainCamera.transform.forward.z);


        if (Physics.Raycast(startPosition, playerCamera.transform.TransformDirection(Vector3.back), out hit, rayRange, layerMask))
        {
            gasGaugeManager.UseGasCheck(useGasValue);

            anim.GrapShoot();
            anim.GrapWalk(true);

            Debug.DrawRay(startPosition, playerCamera.transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow,5);

            endPosition = hit.point;
            startEndDistance = Vector3.Distance(startPosition, endPosition);
            nowAnchor = Instantiate(anchorObj, grapMuzzle.position, transform.rotation);
            nowAnchor.transform.rotation = transform.rotation;
            Vector3 _rotateAngle = nowAnchor.transform.eulerAngles;
            _rotateAngle.x -= 90.0f;
            nowAnchor.transform.rotation = Quaternion.Euler(_rotateAngle);
            grapplingNow=true;
            shootFlag = true;
            lockatFlag = true;
        }
    }

    private void GrapplingMove(Vector3 target, Vector3 transform)
    {
        Vector3 addVelocity = default;

        if (firstFrag)
        {
            rig.AddForce(((target -  transform).normalized + addVelocity) * firstForce, ForceMode.Impulse);
            firstFrag = false;
        }

        if (Input.GetKey(KeyCode.W)) addVelocity +=rig.transform.up*addForce;
        if (Input.GetKey(KeyCode.A)) addVelocity +=(-rig.transform.right)* addForce;
        if (Input.GetKey(KeyCode.D)) addVelocity +=rig.transform.right* addForce;

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
        anim.GrapWalk(false);
    }
}
