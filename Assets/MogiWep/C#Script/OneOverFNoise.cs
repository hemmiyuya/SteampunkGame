using UnityEngine;
using System.Collections;

public class OneOverFNoise : MonoBehaviour
{
    // �����l  
    private float emissionValue = 0.0f;

    // ���̔����l  
    private float nextEmissionValue = 0.0f;

    // �Œᔭ���l  
    [SerializeField]
    private float minEmissionValue = 0.0f;

    // �X�V�W��  
    [SerializeField]
    private float updateTimeFactor = 1.0f;

    // �X�V����  
    private float updateTime;

    Renderer _renderer = default;

    /// <summary>  
    /// ����������  
    /// </summary>  
    void Awake()
    {
        Debug.Log("aaa");
        _renderer = GetComponent<Renderer>();

        // ���̔����l���v�Z  
        CalcNextEmissionValue();
    }

    /// <summary>  
    /// �X�V�O����  
    /// </summary>  
    void Start()
    {
    }

    /// <summary>  
    /// �X�V����  
    /// </summary>  
    void FixedUpdate()
    {
        // �X�V���Ԃ����Z  
        updateTime += Time.fixedDeltaTime;

        // ���ԌW�����v�Z  
        float factor = Mathf.Min((updateTime / updateTimeFactor), 1.0f);

        // ��Ԃ��������l���v�Z  
        float v = Mathf.Lerp(emissionValue, nextEmissionValue, factor);

        if (_renderer != null)
        {
            // �V�F�[�_�[�ɔ����l���Z�b�g  
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
    /// ���̔����l���v�Z  
    /// </summary>  
    void CalcNextEmissionValue()
    {
        // �����_���l���擾  
        float r = Random.Range(0.0f, 1.0f);

        // ���̔����l���Ԍ��J�I�X�@�Ōv�Z  
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
