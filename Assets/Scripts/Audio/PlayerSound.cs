using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource[] audioSources = null;

    [SerializeField]
    private AudioClip[] audioClips;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySE(int num)
    {
        audioSources[0].clip = audioClips[num];
        audioSources[0].Play();
    }

    public void Voice_Yo()
    {
        audioSources[1].clip = audioClips[2];
        audioSources[1].Play();
    }
    public void Voice_Ta()
    {
        audioSources[1].clip = audioClips[3];
        audioSources[1].Play();
    }
    public void Voice_Ya()
    {
        audioSources[1].clip = audioClips[4];
        audioSources[1].Play();
    }

    public void Voice_Torya()
    {
        audioSources[1].clip = audioClips[5];
        audioSources[1].Play();
    }
    public void Voice_Ikuyo()
    {
        audioSources[1].clip = audioClips[6];
        audioSources[1].Play();
    }
    public void Voice_Gya()
    {
        audioSources[1].clip = audioClips[7];
        audioSources[1].Play();
    }
    public void Voice_Ita()
    {
        audioSources[1].clip = audioClips[8];
        audioSources[1].Play();
    }
}
