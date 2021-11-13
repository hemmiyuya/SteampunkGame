using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    private Animator _anim         = default;

    [SerializeField]
    private GameObject _mainCavas  = default;
    [SerializeField]
    private GameObject _startCamera  = default;
    [SerializeField]
    private GameObject _startFilmBack  = default;
    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    /// <summary>
    /// ゲームスタート
    /// </summary>
    public void GameStart()
    {
        transform.parent.gameObject.SetActive(false);
        _mainCavas.SetActive(true);
        
    }
    /// <summary>
    /// シーン遷移前のアニメーション変更
    /// </summary>
    public void AnimationStart()
    {
        Debug.Log("aaaa");
        _anim.SetTrigger("gamestart");
        StartCoroutine(FadeOut());
    }

    [SerializeField]
    private Image _blackOutImage;


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
        }
        StartCoroutine(FadeNow());

        yield break;
    }

    private IEnumerator FadeNow()
    {
        GameStart();
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
        yield break;
    }

}
