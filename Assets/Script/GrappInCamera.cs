using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappInCamera : MonoBehaviour
{

    private bool judgeNow;

    Grappling2 grappling2 = default;

    private string playerCameraTag = "PlayerCamera";
    private void Start()
    {
        grappling2 = GameObject.FindGameObjectWithTag(playerCameraTag).GetComponent<Grappling2>();
    }

    /// <summary>
    /// �J�����̎��E�ɓ����Ă��邩�ǂ������肵�n�߂�
    /// </summary>
    public void StartJudgeInCamera()
    {
        Debug.Log("����J�n");
        judgeNow = true;
    }

    private void OnBecameInvisible()
    {
        if (judgeNow)
        {
            Debug.Log("���E�؂ꂽ");
            grappling2.OutOfVisibility();
            judgeNow = false;
        }

    }
    

}
