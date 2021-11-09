using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const float _playerAttackHitStopTime = 0.2f;
    private const float _hitStopTimeSpeed = 0.001f;

    private float _deltaTime = 0.0f;
    [SerializeField]
    private float minasTime = 0.0f;
    private const float resetTime = 1.0f;

    private bool _isSlowNow = false;

    [SerializeField]
    private float _worldTime = default;
    public float WorldTime
    {
        get { return _worldTime; }
    }

    [SerializeField]
    private Material _lampMat1;
    [SerializeField]
    private Material _lampMat2;
    [SerializeField]
    private GameObject _lamps;
    //ランプ発光のオンオフ
    private bool _setLamp=true;

    [SerializeField]
    private Color _lamp1onColor;
    [SerializeField]
    private Color _lamp1offColor;
    private Color _lamp2onColor;
    private Color _lamp2offColor;

    private void Awake()
    {
        _lamp1onColor = _lampMat1.GetColor("_EmissionColor");
        _lamp2onColor = _lampMat2.GetColor("_EmissionColor");

        _lamp1offColor = _lamp1onColor * 0.2f;
        _lamp2offColor = _lamp2onColor * 0.2f;
    }

    /// <summary>
    /// 終了時マテリアルをもとに戻す
    /// </summary>
    private void OnApplicationQuit()
    {
        _lampMat1.SetColor("_EmissionColor", _lamp1onColor);
        _lampMat2.SetColor("_EmissionColor", _lamp2onColor);
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
        float animeTime = animeStateInfo.normalizedTime;
        animeTime = animeTime - minasTime;
        if (animeTime >= resetTime) 
        {
           
           minasTime += 1;
        } 
        _worldTime = Mathf.Lerp(0, 100, animeTime);
        if (_worldTime == 100)
        {
            _worldTime = 0;
        }

        //昼と夜の電気管理
        if (!_setLamp && _worldTime >= 30 && _worldTime < 75)
        {
            _lampMat1.SetColor("_EmissionColor", _lamp1onColor);
            _lampMat2.SetColor("_EmissionColor", _lamp2onColor);
            _lamps.SetActive(true);
            _setLamp = true;
        }
        else if (_setLamp && (_worldTime >= 75 || _worldTime < 30))
        {
            _lampMat1.SetColor("_EmissionColor", _lamp1offColor);
            _lampMat2.SetColor("_EmissionColor", _lamp2offColor);
            _lamps.SetActive(false);
            _setLamp = false;
        }
        

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
