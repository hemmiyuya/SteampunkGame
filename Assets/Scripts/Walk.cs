using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    private float _groundAccelSpeed    = default;
    private float _skyAccelSpeed = default;
    private float _slowdownSpeed = default;
    private float _walkAnimSpeed = default;
    private float _runAnimSpeed  = default;
    private bool  _slowDown      = default;

    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;

    public void InitialSet(GameObject player, GameObject camera, float groundspeed, float skyspeed,float slowdownspeed, float walkanimspeed, float runanimspeed)
    {
        _player           = player;
        _camera           = camera;
        _anim             = player.GetComponent<Animationmanager>();
        _groundAccelSpeed = groundspeed;
        _skyAccelSpeed    = skyspeed;
        _slowdownSpeed    = slowdownspeed;
        _walkAnimSpeed    = walkanimspeed;
        _runAnimSpeed     = runanimspeed;
    }
    /// <summary>
    /// 歩く
    /// </summary>
    public void PlayerWalk(float hori, float vert, bool run)
    {
        Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
       
        //加速と回転処理
        Quaternion cameraRotation = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up);
        Vector3 velo = cameraRotation * new Vector3(hori, 0, vert).normalized;
        //アニメーション処理
        _anim.WalkAnimStart();
        _anim.SetWalkSpeed(playervelocity.velocity.magnitude / _runAnimSpeed);
       
        //無操作で減速
        if (hori == 0 && vert == 0)
        {
            _anim.WalkAnimEnd();
            playervelocity.velocity = playervelocity.velocity.magnitude > 0 ? Vector3.MoveTowards(playervelocity.velocity, velo * 0, _slowdownSpeed) : Vector3.zero;
            return;
        }
        if(run)
        {
            _slowDown = true;
            if (playervelocity.velocity.magnitude >= _runAnimSpeed)
            {
                playervelocity.velocity = velo * _runAnimSpeed;
            }
            else
            {
                playervelocity.velocity += velo * _groundAccelSpeed;
            }
        }
        else if (_slowDown)
        {
            playervelocity.velocity = Vector3.MoveTowards(playervelocity.velocity, velo * _walkAnimSpeed, _slowdownSpeed);
            if(playervelocity.velocity.magnitude == _walkAnimSpeed)
            _slowDown = false;
        }
        else
        {
            if(playervelocity.velocity.magnitude > _walkAnimSpeed * 0.98f)
            {
                playervelocity.velocity = velo * _walkAnimSpeed;
            }
            else
            {
                playervelocity.velocity += velo * _groundAccelSpeed;
            }
        }
        _player.transform.rotation = Quaternion.LookRotation(velo, Vector3.up);
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

    public void PlayerGoUp(Vector3 start,Vector3 end,float time)
    {
        _player.transform.position = Vector3.Lerp(start,end, time);
    }
    
}
