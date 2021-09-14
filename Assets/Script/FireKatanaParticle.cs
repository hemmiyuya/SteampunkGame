using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C�����퓁�̉��p�[�e�B�N���̊Ǘ�
/// </summary>
public class FireKatanaParticle : MonoBehaviour
{
    private GameObject ParticleObj;
    /// <summary>
    /// ���삷��p�[�e�B�N��
    /// </summary>
    [SerializeField] private ParticleSystem[] particleSystems;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ParticleOn();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            ParticleOFF();
        }
    }

    private void ParticleOn()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }

    private void ParticleOFF()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
