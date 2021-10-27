using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappWalk : MonoBehaviour
{
    private float _groundAccelSpeed = default;
    private float _skyAccelSpeed = default;
    private float _slowdownSpeed = default;
    private float _walkAnimSpeed = default;
    private float _runAnimSpeed = default;

    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;

    public void InitialSet(GameObject player, GameObject camera, float groundspeed, float skyspeed, float slowdownspeed, float walkanimspeed)
    {
        _player = player;
        _camera = camera;
        _anim = player.GetComponent<Animationmanager>();
        _groundAccelSpeed = groundspeed;
        _skyAccelSpeed = skyspeed;
        _slowdownSpeed = slowdownspeed;
        _walkAnimSpeed = walkanimspeed;
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hori">?</param>
    /// <param name="vert"></param>
    /// <param name="run"></param>
    /// <param name="grappEndPos">グラップルが刺さる場所</param>
    public void GrappNowWalk(float hori, float vert, bool run,Vector3 grappEndPos)
    { 
        //アニメーション処理
        _anim.GrapWalkX(hori);
        _anim.GrapWalkZ(vert);

        Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
        //無操作で減速
        if (hori == 0 && vert == 0)
        {
            playervelocity.velocity = playervelocity.velocity.magnitude > 0 ? playervelocity.velocity * _slowdownSpeed : Vector3.zero;
            return;
        }
        //加速と回転処理

        Quaternion cameraRotation = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up);
        Vector3 velo = cameraRotation * new Vector3(hori, 0, vert).normalized;
        var velocityXZ = Vector3.Scale(playervelocity.velocity, new Vector3(1, 0, 1));
       
        playervelocity.velocity = velo * _groundAccelSpeed;
        _player.transform.LookAt(new Vector3(grappEndPos.x,_player.transform.position.y,grappEndPos.z));
    }
    /// <summary>
    /// 空中移動
    /// </summary>
    public void SkyMoving(float hori, float vert)
    {
        if (hori != 0 || vert != 0)
        {

            Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
            Quaternion playerRotation = Quaternion.AngleAxis(_player.transform.eulerAngles.y, Vector3.up);
            Vector3 velo = playerRotation * new Vector3(hori, 0, vert).normalized;
            playervelocity.AddForce(velo.x * _skyAccelSpeed, playervelocity.velocity.y, velo.z * _skyAccelSpeed);
        }
    }
}
