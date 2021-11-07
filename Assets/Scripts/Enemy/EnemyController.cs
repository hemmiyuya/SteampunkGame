using UnityEngine;
using EnemyState;
using UniRx;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrs = null;

    private Rigidbody enemyRig = null;

    private Animator enemyAnim = null;

    [SerializeField]
    private float walkSpeed = 1;

    [SerializeField]
    private float runSpeed = 3;

    [SerializeField]
    private float rotateSpeed = 1;

    //�ύX�O�̃X�e�[�g��
    private string prevStateName;

    private int plusOrMinus = 1;

    EnemyHp enemyHp;
    int beforHp;

    public bool lookatFlag = true;
    private bool death = false;

    //�X�e�[�g
    public StateManager StateManager { get; set; } = new StateManager();
    public EnemyStateIdle StateIdle { get; set; } = new EnemyStateIdle();
    public EnemyStateWalk StateWalk { get; set; } = new EnemyStateWalk();
    public EnemyStateRun StateRun { get; set; } = new EnemyStateRun();
    public EnemyStateSideWalk StateSideWalk { get; set; } = new EnemyStateSideWalk();

    private void Start()
    {
        playerTrs = GameObject.FindGameObjectWithTag("Target").transform;

        enemyRig = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        enemyHp = GetComponent<EnemyHp>();
        beforHp = enemyHp.GetHp();

        //�X�e�[�g�̏�����
        StateManager.State.Value = StateIdle;
        StateIdle.ExecAction = Idle;
        StateWalk.ExecAction = Walk;
        StateRun.ExecAction = Run;
        StateSideWalk.ExecAction = SideWalk;

        //�X�e�[�g�̒l���ύX���ꂽ����s�������s���悤�ɂ���
        StateManager.State
            .Where(_ => StateManager.State.Value.GetStateName() != prevStateName)
            .Subscribe(_ =>
            {
                prevStateName = StateManager.State.Value.GetStateName();
                StateManager.Execute();
            })
            .AddTo(this);
    }

    private void Update()
    {
        if (death) return;

        if (beforHp != enemyHp.GetHp())
        {
            beforHp = enemyHp.GetHp();
            enemyAnim.SetTrigger("Hit");
            //���ꂽ
            if (enemyHp.GetHp() <= 0)
            {
                enemyAnim.SetTrigger("Death");
                death = true ;
            }
        }

        //���p�x�ȏ�Ń^�[�Q�b�g�Ɍ��������킹��
        if (Vector3.Dot(transform.forward, playerTrs.position - transform.position) <= 11.31)
        {
            LookTarget();
        }

        if (prevStateName == "State:Walk" )
        {
            LookTarget();
        }

        if (prevStateName == "State:Run")
        {
            LookTarget();
        }

        if (prevStateName == "State:SideWalk")
        {
            LookTarget();
        }
    }

    //�A�j���[�V�����̋t�Đ�
    public void ReverseAnim()
    {
        enemyAnim.SetFloat(Animator.StringToHash("speed"), -1);
    }
    //�ʏ�̃A�j���[�V����
    public void DefaltAnim()
    {
        enemyAnim.SetFloat(Animator.StringToHash("speed"), 1);
    }

    //�㔼�g���^�[�Q�b�g������
    private void OnAnimatorIK(int layerIndex)
    {
        enemyAnim.SetLookAtWeight(1f, 1f, 1f, 0f, 0.5f);     // LookAt�̒���
        enemyAnim.SetLookAtPosition(playerTrs.position);          // �^�[�Q�b�g�̕���������

    }

    //�^�[�Q�b�g�̕���������
    private void LookTarget()
    {
        if (lookatFlag)
        {
            Vector3 targetDir = new Vector3(playerTrs.position.x, transform.position.y, playerTrs.position.z) - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public void Idle()
    {
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetBool("Side", false);
    }

    public void Walk()
    {
        enemyAnim.SetBool("Walk",true); 
        enemyAnim.SetBool("Run", false);
        enemyAnim.SetBool("Side", false);
    }
    
    public void Run()
    {
        enemyAnim.SetBool("Run", true);
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Side", false);
    }

    public void SideWalk()
    {
        enemyAnim.SetBool("Side", true);
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Run", false);
    }
    

}