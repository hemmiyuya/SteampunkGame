using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private GameObject _player;
    private Rigidbody _playerRig;
    private float _jumpPower   = default;

    public void InitialSet(GameObject player,float jumppower, float maxaltitude)
    {
        _player       = player;
        _playerRig    = _player.GetComponent<Rigidbody>();
        _jumpPower    = jumppower;

    }
    /// <summary>
    /// 通常ジャンプ
    /// </summary>
    public void PlayerJump()
    {
        _playerRig.AddForce(Vector3.up * _jumpPower);
    }
    /// <summary>
    /// 壁から離れるときのジャンプ
    /// </summary>
    public void PlayerWallJump(Vector3 vec)
    {
        _playerRig.AddForce(vec * _jumpPower);
    }

    /// <summary>
    /// プレイヤーに重力加算
    /// </summary>
    public void PlayerAddGravity(float gravity, Vector3 gravityvec)
    {
        _playerRig.AddForce((gravity - 1f) * gravityvec, ForceMode.Acceleration);
    }
    
    /// <summary>
    /// 上昇中か判断
    /// </summary>
    public bool PlayerUpVec()
    {
        bool hantei = false;
        if (_playerRig.velocity.y > 0)
        {
            hantei = true;
        }
        return hantei;
    }

}
