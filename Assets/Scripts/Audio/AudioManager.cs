using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource = null;

    [SerializeField]
    private double FadeInSeconds = 3;
    [SerializeField]
    private bool IsFadeIn = true;
    [SerializeField]
    private double FadeOutSeconds = 3;
    [SerializeField]
    private bool IsFadeOut = true;

    double FadeDeltaTime = 0;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (IsFadeIn)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime > FadeInSeconds)
            {
                FadeDeltaTime = FadeInSeconds;
                IsFadeIn = false;
            }
            audioSource.volume = (float)(FadeDeltaTime / FadeInSeconds)*0.1f;
        }
        else if (IsFadeOut)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime > FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                IsFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds)*0.1f;
        }
        else
        {
            FadeDeltaTime = 0;
        }
    }

    public void FeedOut()
    {

    }
}
