using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private GameObject _player;

    private float _jumpPower   = default;

    public void InitialSet(GameObject player,float jumppower, float maxaltitude)
    {
        _player       = player;
        _jumpPower    = jumppower;

    }
    /// <summary>
    /// 通常ジャンプ
    /// </summary>
    public void PlayerJump()
    {
        _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpPower);
    }
    /// <summary>
    /// 壁から離れるときのジャンプ
    /// </summary>
    public void PlayerWallJump(Vector3 vec)
    {
        _player.GetComponent<Rigidbody>().AddForce(vec * _jumpPower);
    }

    public void PlayerAddGravity(float gravity, Vector3 gravityvec)
    {
        _player.GetComponent<Rigidbody>().AddForce((gravity - 1f) * gravityvec, ForceMode.Acceleration);
    }
    
}
