using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private CharacterHp _characterHp;

    [SerializeField]
    private CharacontrolManager _characontrolManager;

    private const float _morningTime = 0.75f;

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
    Animator AnimeControllerDirectionalLight;

    [SerializeField]
    Animator AnimeControllerClowd;

    /// <summary>
    /// 寝るとき、暗転して朝にする
    /// </summary>
    public void Sleep()
    {
        StartCoroutine(FadeOut());
        audioSource.Play();
    }

    [SerializeField]
    private Image _blackOutImage;

    [SerializeField]
    private AudioSource audioSource;

    private IEnumerator FadeOut()
    {
        
        float FadeOutTime = 4;
        float FadeOutNowTime = 0;
        float FadeOutPer = 0;
        while (FadeOutPer < 1)
        {
            FadeOutPer = FadeOutNowTime / FadeOutTime;
            float a = Mathf.Lerp(0, 1, FadeOutPer);
            FadeOutNowTime += 0.1f;
            _blackOutImage.color = new Color(_blackOutImage.color.r, _blackOutImage.color.g, _blackOutImage.color.b, a);
            yield return new WaitForSeconds(0.1f);
            _characontrolManager.moveFlag = false;
        }
        StartCoroutine(FadeNow());
        AnimeControllerDirectionalLight.Play(0, -1, _morningTime);
        AnimeControllerClowd.Play(0, -1, _morningTime);
        _characterHp.Heal(100);
        yield break;
    }

    private IEnumerator FadeNow()
    {
        float FadeTime = 1.5f;
        float FadeNowTime = 0;
        while (FadeNowTime < FadeTime)
        {
            FadeNowTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(FadeIn());
        yield break;
    }

    private IEnumerator FadeIn()
    {
        float FadeInTime = 2;
        float FadeInNowTime = 0;
        float FadeInPer = 0;

        while (FadeInPer < 1)
        {
            FadeInPer = FadeInNowTime / FadeInTime;
            float a = Mathf.Lerp(1, 0, FadeInPer);
            FadeInNowTime += 0.1f;
            _blackOutImage.color = new Color(_blackOutImage.color.r, _blackOutImage.color.g, _blackOutImage.color.b, a);
            yield return new WaitForSeconds(0.1f);
        }
        _characontrolManager.moveFlag = true;
        yield break;
    }

    private void Update()
    {
        AnimatorStateInfo animeStateInfo = AnimeControllerDirectionalLight.GetCurrentAnimatorStateInfo(0);
        float animeTime = animeStateInfo.normalizedTime;
        //animeTime = animeTime - minasTime;
        if (animeTime >= resetTime) 
        {
            AnimeControllerDirectionalLight.Play(0, -1, 0f);
            //minasTime += 1;
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
