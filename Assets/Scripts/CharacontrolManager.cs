using System.Collections;
using UnityEditor;
using UnityEngine;

//�^�O�I�����邽�߂̃G�f�B�^�[�g��
[CustomEditor(typeof(CharacontrolManager))]
public class TagSelecter : Editor
{
    SerializedProperty _cameratag;

    void OnEnable()
    {
        _cameratag = serializedObject.FindProperty("_cameratag");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        _cameratag.stringValue = EditorGUILayout.TagField("Camera Tag", _cameratag.stringValue);

        serializedObject.ApplyModifiedProperties();
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
    /// �I�u�W�F�N�g�Q
    /// </summary>
    private GameObject _Camera = default;
    [HideInInspector]
    public string _cameratag = "MainCamera";

    /// <summary>
    /// �X�N���v�g�Q
    /// </summary>
    private InputManager _input;
    private Animationmanager _anim;
    private Walk _walk ;
    private Jump _jump ;

    /// <summary>
    /// �ڒn����p���C
    /// </summary>
    //���֌W
    [SerializeField]
    private float g_rayhight  = default;
    [SerializeField]
    private float g_rayamount = default;
    private Vector3 g_raypos  = default;
    private RaycastHit g_hit  = default;
    //�Ǌ֌W
    [SerializeField]
    private Vector3 w_rayorigin = default;
    [SerializeField]
    private float w_rayvector   = default;
    [SerializeField]
    private float w_rayamount   = default;
    private Vector3 w_raypos    = default;
    private RaycastHit w_hit    = default;

    /// <summary>
    /// ����֌W�p�����[�^
    /// </summary>
    //���s�֌W
    [SerializeField]
    private float _walkAccelSpeed     = default;
    [SerializeField, Range(0.01f,1)]
    private float _walkSlowDownSpeed  = default;
    [SerializeField]
    private float _walkAnimSpeed      = default;
    [SerializeField]
    private float _runAnimSpeed       = default;
    [SerializeField]
    private float _gravity            = default;
    //�W�����v�֌W
    [SerializeField]
    private bool _jumping             = default;
    [SerializeField]
    private float _jumpPower          = default;
    [SerializeField]
    private float _maxAltitude        = default;
    [SerializeField]
    private float _flightTime         = default;
    [SerializeField]
    private float _noLandingTime    �@= default;
    [SerializeField]
    private float _rollingTime    �@  = default;
    //�Ǔo��֌W


    void Start()
    {
        _Camera = GameObject.FindGameObjectWithTag(_cameratag);

        _input = GetComponent<InputManager>();
        _anim  = GetComponent<Animationmanager>();
        _walk  = new Walk();
        _jump  = new Jump();
        _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
        _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
    }

    private void FixedUpdate()
    {
        //�ڒn����
        g_raypos = transform.position - new Vector3(0, g_rayhight, 0);
        if (Physics.Raycast(g_raypos, new Vector3(0, -1, 0), g_rayamount))
        {
            _anim.FallingEnd();
            _anim.JumpAnimEnd();
            if (_jumping && _flightTime < _noLandingTime)
            {
                _anim.NoLandingStart();
            }
            else if (_jumping && _flightTime < _rollingTime)
            {
                _anim.LandingStart();
            }
            else if(_jumping && _flightTime >= _rollingTime)
            {
                _anim.RollingStart();
            }
            _flightTime = 0;
            _jumping = false;

        #if UNITY_EDITOR
            _walk.InitialSet(this.gameObject, _Camera, _walkAccelSpeed, _walkSlowDownSpeed, _walkAnimSpeed, _runAnimSpeed);
            _jump.InitialSet(this.gameObject, _jumpPower, _maxAltitude);
        #endif
            _walk.PlayerWalk();

            if (!_jumping && _input.GetJumpButton())
            {
                _flightTime = 0;
                _jumping = true;
                _anim.JumpAnimStart();
                _jump.PlayerJump();
            }
        }
        else
        {
            if (!_jumping)
            {
                _flightTime = 0;
                _jumping = true;
            }

            //�W�����v��Ԃ�
            if (!Physics.Raycast(g_raypos, new Vector3(0, -1, 0), g_rayamount * 3) && !_anim.CheckFalling())
            {
                _anim.FallingStart();
            }
            _flightTime += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce((_gravity - 1f) * Physics.gravity, ForceMode.Acceleration);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position - new Vector3(0, g_rayhight, 0), new Vector3(0,g_rayamount * -1,0));
        Gizmos.DrawRay(transform.position - new Vector3(0, g_rayhight, 0), new Vector3(0,g_rayamount * 3 * -1,0));
        Gizmos.DrawRay(transform.position - new Vector3(0, g_rayhight, 0), new Vector3(0,g_rayamount * 3 * -1,0));
    }
}
