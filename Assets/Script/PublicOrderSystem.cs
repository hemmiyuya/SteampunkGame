using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����V�X�e��
/// </summary>
public class PublicOrderSystem : MonoBehaviour
{
    [SerializeField]
    private float _orderPoint = 0;

    /// <summary>
    /// Worst=0,
    /// VeryBad=1,
    /// Bad=2,
    /// Ordinarily=3,
    /// Good=4,
    /// VeryGood=5,
    /// Best=6,
    /// </summary>
    private enum PublicOrder
    {
        Worst=0,
        VeryBad =1,
        Bad=2,
        Ordinarily=3,
        Good=4,
        VeryGood=5,
        Best=6,
    }

    private void Start()
    {
        AddOrder(500);
    }

    /// <summary>
    /// ���݂̎����]���i�K
    /// </summary>
    private PublicOrder publicOrder=PublicOrder.Ordinarily;

    /// <summary>
    /// �����|�C���g���ϓ�����Ƃ��ɌĂяo��
    /// </summary>
    /// <param name="addOrderPoint">�����Z����|�C���g</param>
    public void AddOrder(float addOrderPoint)
    {
        _orderPoint += addOrderPoint;
        if (_orderPoint > 500) _orderPoint = 500;
        OrderEvaluate();
    }

    [SerializeField]
    private UIManager uiManager = default;

    /// <summary>
    /// �����_�̎����|�C���g�Ɋ�Â��ď�Ԕ�����s��
    /// </summary>
    private void OrderEvaluate()
    {
        int evaluation=default ;

        if (_orderPoint == 500)
        {
            evaluation = 6;
        }
        else if (_orderPoint >=400)
        {
            evaluation = 5;
        }
        else if (_orderPoint >= 300)
        {
            evaluation = 4;
        }
        else if (_orderPoint >= 200)
        {
            evaluation = 3;
        }
        else if (_orderPoint >= 100)
        {
            evaluation = 2;
        }
        else if (_orderPoint > 0)
        {
            evaluation = 1;
        }
        else if (_orderPoint == 0)
        {
            evaluation = 0;
        }

        publicOrder = (PublicOrder)evaluation;

        uiManager.SetOrderStars(_orderPoint);
    }

    /// <summary>
    /// ���݂̕]���i�K���擾����
    /// Worst=0,
    /// VeryBad=1,
    /// Bad=2,
    /// Ordinarily=3,
    /// Good=4,
    /// VeryGood=5,
    /// Best=6,
    /// </summary>
    public int NowOrder
    {
        get { return (int)publicOrder; }
    }

    
}
