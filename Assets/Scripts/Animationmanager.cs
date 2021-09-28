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
    private string[] _swordAttack = { "SwordFirstAttack", "SwordSecondAttack", "SwordThirdAttack" };
    private string[] _gunAttack = { "GunFirstAttack", "GunSecondAttack", "GunThirdAttack" };

    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }
    public void WalkAnimStart()
    {
        _anim.SetBool(_walkTag, true);
    }
    public void WalkAnimEnd()
    {
        _anim.SetBool(_walkTag, false);
    }
    public void RunAnimStart()
    {
        _anim.SetBool(_runTag, true);
    }
    public void RunAnimEnd()
    {
        _anim.SetBool(_runTag, false);
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

    public void SetRunSpeed(float animspeed)
    {
        _anim.SetFloat(_runSpeedTag, animspeed);
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
}
