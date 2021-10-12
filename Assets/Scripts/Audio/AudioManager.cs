using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] audioSource = null;

    [SerializeField, Range(0, 1)]
    private float bgmVolume = 0.1f;
    [SerializeField,Range(0,1)]
    private float buttleVolume=0.15f;

    [SerializeField]
    private double FadeInSeconds = 3;
    [SerializeField]
    private double FadeOutSeconds = 3;

    [SerializeField]
    private double FadeInDeltaTime = 0;
    [SerializeField]
    private double FadeOutDeltaTime = 0;


    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        BgmOn();
    }

    public void ButtleStart()
    {
        //bgm‚ðÁ‚·
        StartCoroutine(FadeOut(0,bgmVolume));
        //buttlebgm‚ð‚Â‚¯‚é
        StartCoroutine(FadeIn(1,buttleVolume));
    }

    public void BgmOn()
    {
        //buttlebgm‚ðÁ‚·
        StartCoroutine(FadeOut(1,buttleVolume));
        //bgm‚ð‚Â‚¯‚é
        StartCoroutine(FadeIn(0,bgmVolume));
    }

    IEnumerator FadeIn(int i , float volume)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            FadeInDeltaTime += 0.1f;
            if (FadeInDeltaTime > FadeInSeconds)
            {
                FadeInDeltaTime = FadeInSeconds;

            }
            audioSource[i].volume = (float)(FadeInDeltaTime / FadeInSeconds) * volume;
            if (volume <= audioSource[i].volume) break;
        }
        FadeInDeltaTime = 0;
        yield break;
    }

    IEnumerator FadeOut(int i,float volume)
    {
        if (audioSource[i].volume == 0) yield break;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            FadeOutDeltaTime += 0.1f;
            if (FadeOutDeltaTime > FadeOutSeconds)
            {
                FadeOutDeltaTime = FadeOutSeconds;

            }
            audioSource[i].volume = (float)(1.0 - FadeOutDeltaTime / FadeOutSeconds) * volume;
            if (0 == audioSource[i].volume) break;
        }

        FadeOutDeltaTime = 0;
        yield break;
    }
}
