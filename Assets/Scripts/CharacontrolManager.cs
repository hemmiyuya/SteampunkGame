using System.Collections;
using UnityEditor;
using UnityEngine;

//タグ選択するためのエディター拡張
[CustomEditor(typeof(CharacontrolManager))]
public class TagSelecter : Editor
{
    private SerializedProperty _cameratag;

    void OnEnable()
    {
        _cameratag = serializedObject.FindProperty("_cameratag");

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //　シリアライズオブジェクトの更新
        serializedObject.Update();

        _cameratag.stringValue = EditorGUILayout.TagField("Camera Tag", _cameratag.stringValue);

    }
}
public class CharacontrolManager : MonoBehaviour
{

    private enum Job
    {
        Wait,
        Run,
        Jump,
        Climb
    }
    private Job job = Job.Wait;
    /// <summary>
    /// オブジェクト群
    /// </summary>
    private GameObject _Camera = default;
    [SerializeField]
    public string _cameratag = "MainCamera";

    /// <summary>
    /// スクリプト群
    /// </summary>
    private InputManager _input;
    private Animationmanager _anim;
    private Walk _walk;
    private Jump _jump;
    private Grab _grab;

    /// <summary>
    /// 判定用レイ
    /// </summary>
    [Header("床判定")]
    [SerializeField]
    private bool _checkGround      = default;//接地中か
    [SerializeField]
    private float g_castHight       = default;//
    [SerializeField]
    private Vector3 g_boxSize      = default;
    [SerializeField]
    private Color g_rayColor = default;

    [Header("床判定(自然落下判定用)")]
    [SerializeField]
    private float g_downRayHight = default;
    [SerializeField]
    private Vector3 g_downBoxSize  = default;
    [SerializeField]
    private Color g_downRayColor = default;

    [Header("床判定共通パラメータ")]
    [SerializeField]
    private float g_maxDistance    = default;
    [SerializeField]
    private RaycastHit g_hit       = default;
    [SerializeField]
    private Vector3 g_hitPoint     = default;
    [SerializeField]
    private Vector3 g_nowPoint     = default;

    [Header("床判定(段差)")]
    [SerializeField]
    private Vector3 w_heightRayOrigin = default;
    [SerializeField]
    private Vector3 w_heightRayPlusVector = default;
    [SerializeField]
    private float w_heightRayAmount = default;
    private RaycastHit w_heightHit = default;
    [SerializeField]
    private Color w_heightRayColor = default;


    [Header("壁判定(下)")]
    [SerializeField]
    private bool _checkStep                 = default;
    [SerializeField]
    private Vector3 w_fowardRayOrigin       = default;
    [SerializeField]
    private Vector3 w_fowardRayPlusVector   = default;
    [SerializeField]
    private float w_fowardRayAmount         = default;
    [SerializeField]
    private RaycastHit w_fowardHit          = default;
    [SerializeField]
    private Color w_fowardRayColor          = default;

    [Header("壁判定(上)")]
    [SerializeField]
    private bool _checkWall                    = default;
    [SerializeField]
    private Vector3 w_upperFowardRayOrigin     = default;
    [SerializeField]
    private Vector3 w_upperFowardRayPlusVector = default;
    [SerializeField]
    private float w_upperFowardRayAmount       = default;
    [SerializeField]
    private RaycastHit w_upperFowardHit        = default;
    [SerializeField]
    private Color w_upperFowardRayColor        = default;


    /// <summary>
    /// 動作関係パラメータ
    /// </summary>
    [Header("歩行関係")]
    [SerializeField]
    private float _inputHori         = default;
    [SerializeField]
    private float _inputVert         = default;
    [SerializeField]
    private bool _inputRun           = default;
    [SerializeField]
    private float _walkAccelSpeed    = default;
    [SerializeField, Range(0.01f,1)]
    private float _walkSlowDownSpeed = default;
    [SerializeField]
    private float _walkAnimSpeed     = default;
    [SerializeField]
    private float _runAnimSpeed      = default;
    [SerializeField]
    private float _gravity           = default;
    [SerializeField]
    private bool _goUpping           = default;
    [SerializeField]
    private float _goUpSpeed         = default;
    [SerializeField]
    private float _goUpTime          = default;
    [Header("ジャンプ関係")]
    [SerializeField]
    private bool _inputJump      = default;
    [SerializeField]
    private bool _jumping        = default;
    [SerializeField]
    private float _jumpPower     = default;
    [SerializeField]
    private float _maxAltitude   = default;
    [SerializeField]
    private float _flightTime    = default;
    [SerializeField]
    private float _noLandingTime = default;
    [SerializeField]
    private float _rollingTime   = default;
    [Header("壁登り関係")]
    [SerializeField]
    private bool _grabing        = default;

    void Start()
    {
        _Camera = GameObject.FindGameObjectWithTag(_cameratag);

        _input = GetComponent<InputManager>();
        _anim  = GetComponent<Animationmanager>();
        _walk  = new Walk();
        _jump  = new Jump();
        _grab  = new Grab();
        _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
        _grab.InitialSet(this.gameObject);
        _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
    }

    private void Update()
    {
        _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
        _grab.InitialSet(this.gameObject);
        _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
        //入力
        _inputHori = _input.GetHorizontal();
        _inputVert = _input.GetVertical();
        _inputRun  = _input.GetRunButton();
        if (_input.GetJumpButton())
        {
            _inputJump = true;
        }
        //判定
        _checkGround = GroundCheck();
        _checkStep   = StepCheck();
        _checkWall   = WallCheck();

    }
    private void FixedUpdate()
    {

        GoUpCheck();
        /*
        if (_checkWall || _grabing)
        {
            _grabing = true;
            if (_inputVert < 0 && _inputJump)
            {
                _grabing = false;
            }
        }
        */
        if (!_grabing && _checkGround || _goUpping)
        {
            //着地アニメーション
            if (_jumping && !_goUpping)
            {
                if (_flightTime < _noLandingTime)
                {
                    _anim.NoLandingStart();
                }
                else if (_flightTime < _rollingTime)
                {
                    _anim.LandingStart();
                }
                else
                {
                    _anim.RollingStart();
                }
                _anim.JumpAnimEnd();
                _anim.FallingEnd();
                _jumping = false;
            }
            _flightTime = 0;
            //歩き
            if (!_grabing)
            {
                _walk.PlayerWalk(_inputHori, _inputVert, _inputRun);
            }

            //ジャンプ
            if (_inputJump)
            {
                _jumping = true;
                _anim.JumpAnimStart();
                _jump.PlayerJump();

            }
            _inputJump = false;
        }
        else
        {
            if (!_jumping && !GroundDownCheck())
            {
                _anim.FallingStart();
                _jumping = true;
            }
            _flightTime += Time.deltaTime;
            _jump.PlayerAddGravity(_gravity);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = g_rayColor;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, g_castHight, 0) + new Vector3(0, -1, 0) * g_maxDistance, g_boxSize * 2);
        Gizmos.color = g_downRayColor;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, g_downRayHight, 0) + new Vector3(0, -1, 0) * g_maxDistance, g_downBoxSize * 2);
        Gizmos.color = w_fowardRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardRayPlusVector * w_fowardRayAmount);
        Gizmos.color = w_upperFowardRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_upperFowardRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_upperFowardRayPlusVector * w_upperFowardRayAmount);
        Gizmos.color = w_heightRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_heightRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_heightRayPlusVector * w_heightRayAmount);
    }

    //接地判定
    private bool GroundCheck()
    {
        return Physics.BoxCast(transform.position + new Vector3(0, g_castHight, 0),g_boxSize, new Vector3(0, -1, 0),transform.rotation, g_maxDistance);
    }
    private bool GroundDownCheck()
    {
        return Physics.BoxCast(transform.position + new Vector3(0, g_downRayHight, 0), g_downBoxSize, new Vector3(0, -1, 0), transform.rotation, g_maxDistance);
    }
    //壁判定
    private bool StepCheck()
    {
        Quaternion playerRotation = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
        bool fowardcheck = Physics.Raycast(transform.position + playerRotation * w_fowardRayOrigin, playerRotation * w_fowardRayPlusVector ,out w_fowardHit, w_fowardRayAmount);
        bool heightcheck = Physics.Raycast(transform.position + playerRotation * w_heightRayOrigin, playerRotation * w_heightRayPlusVector ,out w_heightHit, w_heightRayAmount);
        if (fowardcheck && heightcheck)
        {
            return true;
        }
        return false;
    }

    private bool WallCheck()
    {
        Quaternion playerRotation = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
        bool lowercheck = Physics.Raycast(transform.position + playerRotation * w_fowardRayOrigin, playerRotation * w_fowardRayPlusVector, out w_fowardHit, w_fowardRayAmount);
        bool uppercheck = Physics.Raycast(transform.position + playerRotation * w_upperFowardRayOrigin, playerRotation * w_upperFowardRayPlusVector, out w_upperFowardHit, w_upperFowardRayAmount);
        if (lowercheck && uppercheck)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 段差乗り越え
    /// </summary>
    private void GoUpCheck()
    {
        if (_checkStep && !_goUpping)
        {
            _goUpping = true;
            g_hitPoint = w_heightHit.point;
            g_nowPoint = transform.position;
            _goUpTime = 0;
            if (!_checkGround)
            {
                _anim.ClimbingStart();
            }
        }
        if (_goUpping)
        {
            _goUpTime += Time.deltaTime * _goUpSpeed;
            _walk.PlayerGoUp(g_nowPoint, g_hitPoint, _goUpTime);
            float nowdis = Vector3.Distance(transform.position, g_hitPoint);
            if (nowdis <= 0.1)
            {
                _goUpping = false;
            }
        }

    }

    public void GravityOff()
    {
        _gravity=1;
    }

    public void GravityOn()
    {
        _gravity = 7;
    }

}
