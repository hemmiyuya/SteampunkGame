using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respoon : MonoBehaviour
{
    [SerializeField]
    GameObject gameoverWindow;
    [SerializeField]
    Transform respoonTrs;
    Animator anim;
    Animator animUi;
    CharacterHp characterHp;

    private void Start()
    {
        anim=GetComponent<Animator>();
        characterHp = GetComponent<CharacterHp>();
        animUi = gameoverWindow.GetComponent<Animator>();
    }

    public void RespoonPosSet()
    {
        StartCoroutine(WaitRespoon());
    }
    private IEnumerator WaitRespoon()
    {
        gameoverWindow.SetActive(true);
        yield return new WaitForSeconds(2);

        anim.SetBool("Death", false);
        transform.position = respoonTrs.position;
        transform.forward = respoonTrs.forward;
        animUi.SetTrigger("respoon");
        yield return new WaitForSeconds(1);
        characterHp.Respoon();
        yield return new WaitForSeconds(2);
        gameoverWindow.SetActive(false);

        yield break;

    }
}
