using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasGaugeManager : MonoBehaviour
{
    // Start is called before the first frame update

    private const float MaxGasValue =100;
    private const float minGasValue =100;
    private float nowGasValue = default;
    private float present_Location = 0;


    [SerializeField]
    private Image gasGauge;

    private const float ReduceSpeed = 6f;
    private bool reduceNow = false;

    private float recoverySpeed = 0.05f;

    [SerializeField]
    private Animation cantUseGageAnim=default;
    private void Start()
    {
        nowGasValue = MaxGasValue;
    }

    private void FixedUpdate()
    {
        if (nowGasValue <= MaxGasValue&&!reduceNow)
        {
            nowGasValue += recoverySpeed;
            gasGauge.fillAmount = nowGasValue / MaxGasValue;
        }
    }

    /// <summary>
    /// �K�X���g���邩�ǂ����m�F
    /// </summary>
    /// <param name="useGasValue">�g����</param>
    /// <returns></returns>
    public bool UseGasCheck(float useGasValue)
    {
        if (nowGasValue - useGasValue >= 0)
        {
            reduceNow = true;
            float startGasRatio = nowGasValue / MaxGasValue;
            nowGasValue -= useGasValue;
            float endGasRatio = nowGasValue / MaxGasValue;
            StartCoroutine(ReduceGauGage(startGasRatio,endGasRatio));
            return true;
        }

        //�Q�[�W��Ԃ��_�ł��鏈�������
        cantUseGageAnim. Play();
        return false;

    }

    /// <summary>
    /// �Q�[�W��������
    /// </summary>
    /// <param name="thisNowGasValue"></param>
    /// <returns></returns>
    private IEnumerator ReduceGauGage(float startGasValue,float endGasValue)
    {   

        while (gasGauge.fillAmount != endGasValue)
        {
            present_Location += Time.deltaTime * ReduceSpeed;

            gasGauge.fillAmount = Mathf.Lerp(startGasValue, endGasValue, present_Location);
            yield return new WaitForSeconds(0.02f);
            
        }
        present_Location = 0;
        reduceNow = false;
        yield break;
    }

}
