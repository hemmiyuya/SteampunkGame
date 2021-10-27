using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{


    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;

    private float _accelSpeed = default;
    private float _slowdownSpeed = default;
    private float _climbAnimSpeed = default;

    public void InitialSet(GameObject player,GameObject camera, float accelspeed, float slowdownspeed, float climbanimspeed)
    {
        _player = player;
        _camera = camera;
        _anim = player.GetComponent<Animationmanager>();
        _accelSpeed     = accelspeed;
        _slowdownSpeed  = slowdownspeed;
        _climbAnimSpeed = climbanimspeed;
    }

    public void PlayerClimb(float hori, float vert, bool run,Vector3 normal)
    {
        Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
        //無操作で減速
        if (hori == 0 && vert == 0)
        {
            _anim.SetClimbSpeed(0);
            playervelocity.velocity = playervelocity.velocity.magnitude > 0 ? playervelocity.velocity * _slowdownSpeed : Vector3.zero;
            return;
        }
        //加速と回転処理
        Quaternion cameraRotation = Quaternion.AngleAxis(normal.magnitude, normal);
        Vector3 velo = cameraRotation * new Vector3(hori * -normal.z, vert, hori * normal.x).normalized;
        var velocityXZ = Vector3.Scale(playervelocity.velocity, new Vector3(1, 0, 1));
        //アニメーション処理
        _anim.ClimbAnimStart();
        _anim.SetClimbSpeed((Mathf.Abs(vert) + Mathf.Abs(hori)) * _climbAnimSpeed);
        playervelocity.velocity = velo * _accelSpeed;
        //_player.transform.rotation = Quaternion.LookRotation(velo, normal);

    }
    
}
