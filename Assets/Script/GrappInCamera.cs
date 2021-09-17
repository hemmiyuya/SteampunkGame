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
    /// カメラの視界に入っているかどうか判定し始める
    /// </summary>
    public void StartJudgeInCamera()
    {
        Debug.Log("判定開始");
        judgeNow = true;
    }

    private void OnBecameInvisible()
    {
        if (judgeNow)
        {
            Debug.Log("視界切れた");
            grappling2.OutOfVisibility();
            judgeNow = false;
        }

    }
    

}
