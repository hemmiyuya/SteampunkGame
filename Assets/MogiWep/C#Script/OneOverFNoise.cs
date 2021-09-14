using UnityEngine;
using System.Collections;

public class OneOverFNoise : MonoBehaviour
{
    // 発光値  
    private float emissionValue = 0.0f;

    // 次の発光値  
    private float nextEmissionValue = 0.0f;

    // 最低発光値  
    [SerializeField]
    private float minEmissionValue = 0.0f;

    // 更新係数  
    [SerializeField]
    private float updateTimeFactor = 1.0f;

    // 更新時間  
    private float updateTime;

    Renderer _renderer = default;

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
        Debug.Log("aaa");
        _renderer = GetComponent<Renderer>();

        // 次の発光値を計算  
        CalcNextEmissionValue();
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void FixedUpdate()
    {
        // 更新時間を加算  
        updateTime += Time.fixedDeltaTime;

        // 時間係数を計算  
        float factor = Mathf.Min((updateTime / updateTimeFactor), 1.0f);

        // 補間した発光値を計算  
        float v = Mathf.Lerp(emissionValue, nextEmissionValue, factor);

        if (_renderer != null)
        {
            // シェーダーに発光値をセット  
            _renderer.material.SetFloat("_Emission00FN", v);
        }

        if (factor >= 1.0f)
        {
            updateTime = 0.0f;
            emissionValue = nextEmissionValue;
            CalcNextEmissionValue();
        }
    }

    /// <summary>  
    /// 次の発光値を計算  
    /// </summary>  
    void CalcNextEmissionValue()
    {
        // ランダム値を取得  
        float r = Random.Range(0.0f, 1.0f);

        // 次の発光値を間欠カオス法で計算  
        if (r <= 0.01f)
        {
            nextEmissionValue = r + 0.02f;
        }
        else if (r < 0.5f)
        {
            nextEmissionValue = r + 2.0f * r * r;
        }
        else if (r >= 0.99f)
        {
            nextEmissionValue = r - 0.01f;
        }
        else
        {
            nextEmissionValue = r - 2.0f * (1.0f - r) * (1.0f - r);
        }

        nextEmissionValue = Mathf.Max(nextEmissionValue, minEmissionValue);
    }
}
