using System.Collections;
using UnityEngine;

public class CharacontrolManager : MonoBehaviour
{

    private enum Job
    {
        Wait,
        Run,
        Jump,
        Climb
    }

    private string[] notAttack = { "idle", "walk", "run" };

    [SerializeField]
    private GameObject gun = null;
    [SerializeField]
    private GameObject sowrd = null;

    private Job job = Job.Wait;
    /// <summary>
    /// �I�u�W�F�N�g�Q
    /// </summary>
    [SerializeField]
    private GameObject _Camera = default;

    /// <summary>
    /// �X�N���v�g�Q
    /// </summary>
    private InputManager     _input=default;
    private Animationmanager _anim=default;
    private Walk             _walk=default;
    private Jump             _jump=default;
    private Grab             _grab=default;
    private Attack      _attack=default;
    private GrappWalk _grappWalk=default;
    private Grappling2 _grappling2 = default;

    /// <summary>
    /// ����p���C
    /// </summary>
    [SerializeField]
    private int _groundLayer  = default;
    [Header("������")]
    [SerializeField]
    private bool _checkGround = default;
    [SerializeField]
    private float g_castHight = default;
    [SerializeField]
    private Vector3 g_boxSize = default;
    [SerializeField]
    private Color g_rayColor  = default;

    [Header("������(���R��������p)")]
    [SerializeField]
    private float g_downRayHight = default;
    [SerializeField]
    private Vector3 g_downBoxSize  = default;
    [SerializeField]
    private Color g_downRayColor = default;

    [Header("�����苤�ʃp�����[�^")]
    [SerializeField]
    private float g_maxDistance    = default;
    [SerializeField]
    private RaycastHit g_hit       = default;
    [SerializeField]
    private Vector3 g_hitPoint     = default;
    [SerializeField]
    private Vector3 g_nowPoint     = default;

    [Header("������(�i��)")]
    [SerializeField]
    private Vector3 w_heightRayOrigin = default;
    [SerializeField]
    private Vector3 w_heightRayPlusVector = default;
    [SerializeField]
    private float w_heightRayAmount = default;
    private RaycastHit w_heightHit = default;
    [SerializeField]
    private Color w_heightRayColor = default;


    [Header("�ǔ���(��)")]
    [SerializeField]
    private bool _checkStep               = default;
    [SerializeField]
    private Vector3 w_fowardDownRayOrigin       = default;
    [SerializeField]
    private Vector3 w_fowardDownRayPlusVector   = default;
    [SerializeField]
    private float w_fowardDownRayAmount         = default;
    [SerializeField]
    private RaycastHit w_fowardDownHit          = default;
    [SerializeField]
    private Color w_fowardDownRayColor          = default;

    [Header("�ǔ���(��)")]
    [SerializeField]
    private Vector3 w_fowardUpRayOrigin       = default;
    [SerializeField]
    private Vector3 w_fowardUpRayPlusVector   = default;
    [SerializeField]
    private float w_fowardUpRayAmount         = default;
    [SerializeField]
    private RaycastHit w_fowardUpHit          = default;
    [SerializeField]
    private Color w_fowardUpRayColor          = default;

    [Header("�ǔ���(�i���̒��_)")]
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
    /// ����֌W�p�����[�^
    /// </summary>
    [Header("���s�֌W")]
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
    [Header("�W�����v�֌W")]
    [SerializeField]
    private bool _inputJump      = default;
    [SerializeField]
    private bool _jumping        = default;
    [SerializeField]
    private float _jumpPower     = default;
    [SerializeField]
    private float _maxAltitude   = default;
    [SerializeField]
    private float _skyAccelSpeed = default;
    [SerializeField]
    private float _flightTime    = default;
    [SerializeField]
    private float _noLandingTime = default;
    [SerializeField]
    private float _rollingTime   = default;
    [Header("�Ǔo��֌W")]
    [SerializeField]
    private bool _grabing             = default;
    [SerializeField]
    private float _climbAccelSpeed    = default;
    [SerializeField, Range(0.01f, 1)]
    private float _climbSlowDownSpeed = default;
    [SerializeField]
    private float _climbAnimSpeed     = default;
    [SerializeField]
    private Vector3 _wallNormal       = default;
    [SerializeField]
    private float _playerTilt         = default;
    [Header("�U���֌W")]
    [SerializeField]
    //�������(�f�t�H���g�e)
    private bool _inputSwitchWeapon   = default;
    [SerializeField]
    private bool _attacking           = default;
    [SerializeField]
    private bool _inputAttack         = default;
    [SerializeField]
    private int _combo                = default;
    private bool _animCheck           = default;
    private bool _wait                = default;
    [SerializeField]
    private int _maxcombo             = 3;
    [Header("�O���b�v��")]
    [SerializeField]
    private bool _grapping = default;

    void Start()
    {
        _input  = GetComponent<InputManager>();
        _anim   = GetComponent<Animationmanager>();
        _walk   = new Walk();
        _jump   = new Jump();
        _grab   = new Grab();
        _attack = new Attack();
        _grappWalk = new GrappWalk();
        _grappling2 = GetComponent<Grappling2>();
        _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _skyAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
        _grab.InitialSet(this.gameObject, _Camera, _climbAccelSpeed, _climbSlowDownSpeed, _climbAnimSpeed);
        _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
        _attack.InitialSet(this.gameObject, _Camera);
        _grappWalk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _skyAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed);
    }

    private void Update()
    {
        /*��������H(���s���ɃC���X�y�N�^�[�Ő��l�ς���Ƃ�����)
        _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _skyAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
        _grab.InitialSet(this.gameObject, _Camera, _climbAccelSpeed, _climbSlowDownSpeed, _climbAnimSpeed);
        _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
        _attack.InitialSet(this.gameObject, _Camera);
        */
        //����
        _inputHori = _input.GetHorizontal();
        _inputVert = _input.GetVertical();
        _inputRun  = _input.GetRunButton();
        if (_input.GetMouseLeftClick())
        {
            _inputAttack = true;
        }
        if (_input.GetJumpButton())
        {
            _inputJump = true;
        }
        if (_input.GetSwitchWeaponButton())
        {
            _inputSwitchWeapon = !_inputSwitchWeapon;
            gun.active = !gun.active;
            sowrd.active = !sowrd.active;
        }
        //����
        _checkGround = GroundCheck();
        _checkStep   = StepCheck();
        _checkWall   = WallCheck();

        if (_animCheck)
        {
            //���݂̃A�j���[�V�����N���b�v��
            if (_anim.NowClipName() == notAttack[0]
                || _anim.NowClipName() == notAttack[1]
                || _anim.NowClipName() == notAttack[2])
            {
                _attacking = false;
                _inputAttack = false;
                _anim.InvalidRootMotion();
                _animCheck = false;
            }
        }
    }
    private void FixedUpdate()
    {
        //�i���z����
        GoUpCheck();

        //�ǔ���E����
        WallMove();

        //�ڒn����E����
        if (!_grabing && _checkGround || _goUpping)
        {
            if (!_goUpping && _inputAttack && !_attacking)
            {
                _attacking = true;
                _combo = 1;
                _anim.ActiveRootMotion();
            }
            if (_attacking && _inputAttack && _maxcombo >= _combo&&!_wait)
            {
                StartCoroutine(Attack());
            }
            else
            {
                GroundMove();
            }
        }
        else
        {
            _inputAttack = false;
            //�󒆔���E����
            SkyMove();
        }
        _inputJump = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = g_rayColor;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, g_castHight, 0) + new Vector3(0, -1, 0) * g_maxDistance, g_boxSize * 2);
        Gizmos.color = g_downRayColor;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, g_downRayHight, 0) + new Vector3(0, -1, 0) * g_maxDistance, g_downBoxSize * 2);
        Gizmos.color = w_fowardDownRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardDownRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardDownRayPlusVector * w_fowardDownRayAmount);
        Gizmos.color = w_fowardUpRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardUpRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_fowardUpRayPlusVector * w_fowardUpRayAmount);
        Gizmos.color = w_upperFowardRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_upperFowardRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_upperFowardRayPlusVector * w_upperFowardRayAmount);
        Gizmos.color = w_heightRayColor;
        Gizmos.DrawRay(transform.position +Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_heightRayOrigin, Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * w_heightRayPlusVector * w_heightRayAmount);
    }

    /// <summary>
    /// �ڒn����
    /// </summary>
    private bool GroundCheck()
    {
        return Physics.BoxCast(transform.position + new Vector3(0, g_castHight, 0),g_boxSize, new Vector3(0, -1, 0),transform.rotation, g_maxDistance, 1 << _groundLayer);
    }
    private bool GroundDownCheck()
    {
        return Physics.BoxCast(transform.position + new Vector3(0, g_downRayHight, 0), g_downBoxSize, new Vector3(0, -1, 0), transform.rotation, g_maxDistance, 1 << _groundLayer);
    }
    /// <summary>
    /// �i������
    /// </summary>
    private bool StepCheck()
    {
        Quaternion playerRotation = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
        bool fowarddowncheck = Physics.Raycast(transform.position + playerRotation * w_fowardDownRayOrigin, playerRotation * w_fowardDownRayPlusVector, out w_fowardDownHit, w_fowardDownRayAmount, 1 << _groundLayer);
        bool heightcheck     = Physics.Raycast(transform.position + playerRotation * w_heightRayOrigin, playerRotation * w_heightRayPlusVector ,out w_heightHit, w_heightRayAmount, 1 << _groundLayer);
        bool fowardupcheck   = Physics.Raycast(transform.position + playerRotation * w_fowardUpRayOrigin, playerRotation * w_fowardUpRayPlusVector, out w_fowardUpHit, w_fowardUpRayAmount, 1 << _groundLayer);
        if (fowarddowncheck && heightcheck && !fowardupcheck)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// �ǔ���
    /// </summary>
    private bool WallCheck()
    {
        Quaternion playerRotation = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
        bool lowercheck = Physics.Raycast(transform.position + playerRotation * w_fowardDownRayOrigin, playerRotation * w_fowardDownRayPlusVector, out w_fowardDownHit, w_fowardDownRayAmount, 1 << _groundLayer);
        bool uppercheck = Physics.Raycast(transform.position + playerRotation * w_upperFowardRayOrigin, playerRotation * w_upperFowardRayPlusVector, out w_upperFowardHit, w_upperFowardRayAmount, 1 << _groundLayer);
        if (lowercheck && uppercheck)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// �i�����z��
    /// </summary>
    private void GoUpCheck()
    {
        //����
        if (_checkStep && !_goUpping && !_attacking)
        {
            _goUpping = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            g_hitPoint = w_heightHit.point;
            g_nowPoint = transform.position;
            _goUpTime = 0;
            if (!_checkGround)
            {
                _anim.ClimbingStart();
            }
        }
        //����
        if (_goUpping)
        {
            _goUpTime += Time.deltaTime * _goUpSpeed;
            _walk.PlayerGoUp(g_nowPoint, g_hitPoint, _goUpTime);
            float nowdis = Vector3.Distance(transform.position, g_hitPoint);
            if (nowdis <= 0.1)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                _goUpping = false;
                _grabing  = false;
                _flightTime = 0;
                _anim.ClimbAnimEnd();
            }
        }
    }

    /// <summary>
    /// �n�ʓ���
    /// </summary>
    private void GroundMove()
    {
        //���n�A�j���[�V����
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
        if ( _grappling2.UsingGrapp)
        {
            _grappWalk.GrappNowWalk(-_inputHori, -_inputVert, _inputRun, _grappling2.NowEndPos);
        }
        //����
        if (!_grabing && !_attacking&&!_grappling2.UsingGrapp)
        {
            _walk.PlayerWalk(-_inputHori, -_inputVert, _inputRun);
        }
        //�W�����v
        if (_inputJump)
        {
            _jumping = true;
            _anim.JumpAnimStart();
            _jump.PlayerJump();
        }
    }

    /// <summary>
    /// �󒆓���
    /// </summary>
    private void SkyMove()
    {
        if (!_jumping && !GroundDownCheck() && !_attacking)
        {
            _anim.FallingStart();
            _jumping = true;
        }
        _flightTime += Time.deltaTime;
        Vector3 gravityvector = Vector3.zero;
        if (!_grabing)
        {
            gravityvector = Physics.gravity;
            _walk.SkyMoving(_inputHori, _inputVert);
        }
        else
        {
            gravityvector = w_upperFowardHit.normal * -9.81f;
        }
        _jump.PlayerAddGravity(_gravity, gravityvector);
    }


    /// <summary>
    /// �Ǔ���
    /// </summary>
    private void WallMove()
    {
        //�ǈړ��͂���
        if (!_goUpping && _checkWall && !_attacking)
        {
            _grabing = true;
            _anim.ClimbAnimStart();
            transform.rotation = Quaternion.LookRotation(w_upperFowardHit.normal * -1, Vector3.up);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _wallNormal = w_upperFowardHit.normal;
        }
        //�ǈړ�����
        if (_grabing && _inputVert < 0 && _inputJump)
        {
            _jump.PlayerWallJump((w_upperFowardHit.normal + Vector3.up).normalized);
            _grabing = false;
            _inputJump = false;
            _anim.ClimbAnimEnd();
            _flightTime = 0;
        }
        else if (_grabing && !_checkWall)
        {
            _jump.PlayerWallJump((w_upperFowardHit.normal + Vector3.up).normalized);
            _grabing = false;
            _inputJump = false;
            _anim.ClimbAnimEnd();
            _flightTime = 0;
        }
        //�ǈړ�����
        if (_grabing)
        {
            _grab.PlayerClimb(_inputHori, _inputVert, _inputRun, _wallNormal);
        }

    }
    public void GravityOff()
    {
        _gravity = 1;
    }

    public void GravityOn()
    {
        _gravity = 5.5f;
    }

    public bool ReturnWeponFlag()
    {
        return _inputSwitchWeapon;
    }

    /// <summary>
    /// �U������
    /// </summary>
    private IEnumerator Attack()
    {
        if (_inputSwitchWeapon)
        {
            _attack.PlayerSwordAttack(_combo);
        }
        else
        {
            _attack.PlayerGunAttack(_combo);
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _combo++;
        _wait = true;
        _inputAttack = false;
        yield return new WaitForSeconds(0.4f);
        _animCheck = true;
        _wait = false;

        yield break;
    }

}
