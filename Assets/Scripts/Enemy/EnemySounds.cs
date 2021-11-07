using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]
    AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();   
    }

    public void PlaySE(int num)
    {
        audio.clip = clips[num];
        audio.Play();
    }

    public void SE_Zun()
    {
        audio.clip = clips[1];
        audio.Play();
    }
}
