using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSound : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;

    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audio.clip = clips[0];
        audio.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        audio.clip = clips[1];
        audio.Play();
    }
}
