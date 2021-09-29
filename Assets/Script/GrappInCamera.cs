using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappInCamera : MonoBehaviour
{

    private bool judgeNow;

    Grappling2 grappling2 = default;

    private string playerTag = "Player";
    private void Start()
    {
        grappling2 = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Grappling2>();
    }

    /// <summary>
    /// �J�����̎��E�ɓ����Ă��邩�ǂ������肵�n�߂�
    /// </summary>
    public void StartJudgeInCamera()
    {
        judgeNow = true;
    }

    private void OnBecameInvisible()
    {
        if (judgeNow)
        {
            grappling2.OutOfVisibility();
            judgeNow = false;
        }

    }
    

}
