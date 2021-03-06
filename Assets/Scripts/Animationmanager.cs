using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationmanager : MonoBehaviour
{
    Animator _anim;
    //アニメーションタグ群
    private const string _walkTag = "Walk";
    private const string _walkSpeedTag = "WalkSpeed";
    private const string _runTag = "Run";
    private const string _runSpeedTag = "RunSpeed";
    private const string _climbTag = "Climb";
    private const string _climbSpeedTag = "ClimbSpeed";
    private const string _jumpTag = "Jump";
    private const string _fallingTag = "Falling";
    private const string _noLandingTag = "NoLanding";
    private const string _landingTag = "Landing";
    private const string _rollingTag = "Rolling";
    private const string _climbingTag = "GoUp";
    private const string _grapShootTag = "GrapShot";
    private const string _grapHitTag = "GrapHit";
    private const string _grapCompTag = "GrapComp";
    private const string _grapWalkTag = "GrapWalk";
    private const string _grapSpeedXTag = "Speed_x";
    private const string _grapSpeedZTag = "Speed_z";
    private string[] _swordAttack = { "SwordFirstAttack", "SwordSecondAttack", "SwordThirdAttack" };
    private string[] _gunAttack = { "GunFirstAttack", "GunSecondAttack", "GunThirdAttack" };
    private string _grapKick = "Kick";

    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    public string NowClipName()
    {
        return _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }
    public void WalkAnimStart()
    {
        _anim.SetBool(_walkTag, true);
    }
    public void WalkAnimEnd()
    {
        _anim.SetBool(_walkTag, false);
    }
    public void ClimbAnimStart()
    {
        _anim.SetBool(_climbTag, true);
    }
    public void ClimbAnimEnd()
    {
        _anim.SetBool(_climbTag, false);
    }
    public void JumpAnimStart()
    {
        _anim.SetBool(_jumpTag, true);
    }

    public void JumpAnimEnd()
    {
        _anim.SetBool(_jumpTag, false);
    }
    public void FallingStart()
    {
        _anim.SetBool(_fallingTag,true);
    }
    public void FallingEnd()
    {
        _anim.SetBool(_fallingTag,false);
    }

    public bool CheckFalling()
    {
        return _anim.GetBool(_fallingTag);
    }

    public void NoLandingStart()
    {
        _anim.SetTrigger(_noLandingTag);
    }
    public void LandingStart()
    {
        _anim.SetTrigger(_landingTag);
    }
    public void RollingStart()
    {
        _anim.SetTrigger(_rollingTag);
    }
    public void ClimbingStart()
    {
        _anim.SetTrigger(_climbingTag);
    }
    
    public void SwordAttackStart(int combo)
    {
        _anim.SetTrigger(_swordAttack[combo - 1]);
    }

    public void GunAttackStart(int combo)
    {
        _anim.SetTrigger(_gunAttack[combo - 1]);
    }

    public void SetWalkSpeed(float animspeed)
    {
        _anim.SetFloat(_walkSpeedTag,animspeed);
    }

    public void SetClimbSpeed(float animspeed)
    {
        _anim.SetFloat(_climbSpeedTag, animspeed);
    }

    public void ActiveRootMotion()
    {
        _anim.applyRootMotion = true;
    }
    public void InvalidRootMotion()
    {
        _anim.applyRootMotion = false;
    }
    public void GrapShoot()
    {
        _anim.SetTrigger(_grapShootTag);
    }
    public void GrapHit()
    {
        _anim.SetTrigger(_grapHitTag);
    }
    public void GrapComp()
    {
        _anim.SetTrigger(_grapCompTag);
    }
    public void Kick()
    {
        _anim.SetTrigger(_grapKick);
    }
    public void GrapWalk(bool walk)
    {
        _anim.SetBool(_grapWalkTag, walk);
    }

    public void GrapWalkX(float speed_x)
    {
        _anim.SetFloat(_grapSpeedXTag, speed_x);
    }
    public void GrapWalkZ(float speed_z)
    {
        _anim.SetFloat(_grapSpeedZTag, speed_z);
    }
}
