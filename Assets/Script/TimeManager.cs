using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const float _playerAttackHitStopTime = 0.2f;
    private const float _hitStopTimeSpeed = 0.001f;

    private float _deltaTime = 0.0f;

    private bool _isSlowNow = false;

    private float _worldTime = default;
    public float WorldTime
    {
        get { return _worldTime; }
    }


    private enum HitStopTipe
    {
        playerAttack=0,
    }

    private HitStopTipe _stopType = default;

    [SerializeField]
    Animator AnimeController;

    private void Update()
    {
        AnimatorStateInfo animeStateInfo = AnimeController.GetCurrentAnimatorStateInfo(0);

        _worldTime = Mathf.Lerp(0, 100, animeStateInfo.normalizedTime);

        

        //ヒットストップやつ
        if (_isSlowNow)
        {
            _deltaTime+= Time.unscaledDeltaTime;

            switch (_stopType)
            {
                case HitStopTipe.playerAttack:

                    if (_deltaTime >= _playerAttackHitStopTime)
                    {
                        SetNormalTime();
                    }

                    break;
            }

            
        }
    }

    /// <summary>
    /// プレイヤーの攻撃(剣)が敵にあたったときにヒットストップさせてみそ
    /// </summary>
    public void PlayerAttackHitStop()
    {
        _deltaTime = 0.0f;
        Time.timeScale = _hitStopTimeSpeed;
        _isSlowNow = true;
        _stopType = HitStopTipe.playerAttack;
    }

    private void SetNormalTime()
    {
        Time.timeScale = 1.0f;
        _isSlowNow = false;
    }

}
