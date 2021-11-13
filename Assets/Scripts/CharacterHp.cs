using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHp : MonoBehaviour
{
    [SerializeField]
    Image healthbar;

    [SerializeField]
    int hp = 200;

    int maxHp;

    float timer;

    bool damageFlag;

    Rigidbody rb;
    Animator anim;

    CharacontrolManager characontrolManager;
    Grappling2 grappling;

    private void Start()
    {
         maxHp = hp;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        characontrolManager = GetComponent<CharacontrolManager>();
        grappling = GetComponent<Grappling2>();
        StartCoroutine(AutoHeal());
        damageFlag = true;
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="parameter">0��hit�A�j���[�V�����@�A�@1���m�b�N�o�b�N�A�j���[�V����</param>
    /// <param name="enemyVec">�G�̈ʒu</param>
    public void Damage(int damage,int parameter,Vector3 enemyVec)
    {
        if (damageFlag)
        {
            damageFlag = false;

            MoveOff();

            hp -= damage;
            if (hp <= 0)
            {
                Death();
                hp = 0;
            }
            else
            {
                if (parameter == 0)
                {
                    anim.SetBool("Damage",true);
                    damageFlag = true;
                }
                else
                {
                    anim.SetBool("Knockback",true);
                    transform.LookAt( new Vector3(enemyVec.x, transform.position.y, enemyVec.z));
                    StartCoroutine(MoveBack(enemyVec));
                }
            }
            StartCoroutine(Transition());
        }
        
    }

    public void Heal(int heal)
    {
        if (hp >= maxHp) return;
        hp += heal;
        if (hp > maxHp)
            hp = maxHp;
        StartCoroutine(Transition());
            
    }

    private void Death()
    {
        anim.SetBool("Death", true);
    }

    /// <summary>
    /// hp�΁[�̕ύX
    /// </summary>
    IEnumerator Transition()
    {
        timer = 0;
        float fromGauge = healthbar.fillAmount;
        float toGauge = ((float)hp /(float)maxHp);

        while (0.5f > timer)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            float interpolatedValue = timer / 0.5f;

            healthbar.fillAmount = Mathf.Lerp(fromGauge, toGauge, interpolatedValue);
        }

        yield break;
    }

    private IEnumerator MoveBack(Vector3 enemey)
    {
        Vector3 vec = (transform.position - enemey).normalized;

        Vector3 forceDirection = new Vector3(vec.x, 1.0f, vec.z);

        // ��̌����ɉ����͂̑傫�����`
        float forceMagnitude = 15.0f;

        Vector3 force = forceMagnitude * forceDirection;

        rb.AddForce(force, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);

        damageFlag = true;

        yield break;
    }

    IEnumerator AutoHeal()
    {
        while (this)
        {
            yield return new WaitForSeconds(3f);
            if (hp>=0)
            {
                //hp��3������
                Heal( (int)(maxHp / 100) * 3);
            }
        }
        yield break;
    }

    //�A�j���[�V�����C�x���g�ŌĂяo��
    public void MoveOn()
    {
        anim.SetBool("Damage", false);
        anim.SetBool("Knockback", false);
        characontrolManager.moveFlag = true;
        grappling.grappFlag = true;
    }

    public void AnimOff()
    {
        anim.SetBool("Damage", false);
    }

    private void MoveOff()
    {
        characontrolManager.moveFlag = false;
        grappling.grappFlag = false;
    }

    public void Respoon()
    {
        //hp������
        Heal(maxHp / 2);
        MoveOn();

    }
}
