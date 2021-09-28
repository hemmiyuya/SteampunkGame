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
    private bool _running        = default;

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
    public void PlayerWalk(float hori,float vert,bool run)
    {
        Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
        //無操作で減速
        if (hori == 0 && vert == 0)
        {
            _running = false;
            _anim.WalkAnimEnd();
            _anim.RunAnimEnd();
            playervelocity.velocity = playervelocity.velocity.magnitude > 0 ? playervelocity.velocity * _slowdownSpeed : Vector3.zero;
            return;
        }
        //加速と回転処理
            if (!_running && run)
            {
                _running = true;
            }

            Quaternion cameraRotation = Quaternion.AngleAxis(_camera.transform.eulerAngles.y,Vector3.up);
            Vector3 velo = cameraRotation * new Vector3(hori, 0, vert).normalized;
            var velocityXZ = Vector3.Scale(playervelocity.velocity, new Vector3(1, 0, 1));
        //アニメーション処理
        if (_running)
            {
                _anim.RunAnimStart();
                _anim.SetRunSpeed(velocityXZ.magnitude / _runAnimSpeed);
                velo *= 2;
            }
            else
            {
                _anim.WalkAnimStart();
                _anim.SetWalkSpeed(velocityXZ.magnitude / _walkAnimSpeed);
            }
        playervelocity.velocity = velo * _groundAccelSpeed;
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
