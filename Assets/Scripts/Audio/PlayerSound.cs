using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(int num)
    {
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }
}
