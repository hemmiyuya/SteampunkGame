using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Animationmanager _anim;

    private GameObject _player;

    private float _jumpPower   = default;
    private float _maxAltitude = default;

    public void InitialSet(GameObject player,float jumppower, float maxaltitude)
    {
        _player       = player;
        _anim         = player.GetComponent<Animationmanager>();
        _jumpPower    = jumppower;
        _maxAltitude  = maxaltitude;

    }

    public void PlayerJump()
    {
        _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpPower);
    }
    
}
