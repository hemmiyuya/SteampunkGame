using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    private float _accelSpeed    = default;
    private float _slowdownSpeed = default;
    private float _walkAnimSpeed = default;
    private float _runAnimSpeed  = default;
    private bool _running        = default;

    private InputManager _inputmanager;

    private Animationmanager _anim;

    private GameObject _player;
    private GameObject _camera;

    public void InitialSet(GameObject player, GameObject camera, float accelspeed,float slowdownspeed, float walkanimspeed, float runanimspeed)
    {
        _player        = player;
        _camera        = camera;
        _inputmanager  = player.GetComponent<InputManager>();
        _anim          = player.GetComponent<Animationmanager>();
        _accelSpeed    = accelspeed;
        _slowdownSpeed = slowdownspeed;
        _walkAnimSpeed = walkanimspeed;
        _runAnimSpeed  = runanimspeed;
    }
    
    public void PlayerWalk()
    {
        Rigidbody playervelocity = _player.GetComponent<Rigidbody>();
        //無操作で減速
        if (_inputmanager.GetHorizontal() == 0 && _inputmanager.GetVertical() == 0)
        {
            _running = false;
            _anim.WalkAnimEnd();
            _anim.RunAnimEnd();
            playervelocity.velocity = playervelocity.velocity.magnitude > 0 ? playervelocity.velocity * _slowdownSpeed : Vector3.zero;
            return;
        }
        //加速と回転処理
            if (!_running && _inputmanager.GetRunButton())
            {
                _running = true;
            }

            Quaternion cameraRotation = Quaternion.AngleAxis(_camera.transform.eulerAngles.y,Vector3.up);
            Vector3 velo = cameraRotation * new Vector3(_inputmanager.GetHorizontal(), 0, _inputmanager.GetVertical()).normalized;
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
        playervelocity.velocity = velo * _accelSpeed;
        _player.transform.rotation = Quaternion.LookRotation(velo, Vector3.up);

        return;
    }
}
