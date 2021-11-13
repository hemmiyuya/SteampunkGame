using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScaneAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSource = null;
    [SerializeField]
    private float _roopStartTime    = default;
    [SerializeField]
    private float _roopEndTime      = default;
    [SerializeField]
    private AudioClip[] _seClips    = new AudioClip[3];
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioSource[0].clip = _seClips[0];
        audioSource[0].Play();
        audioSource[0].loop=false;
    }
    private void Update()
    {
        if (audioSource[0].time >= _roopEndTime)
        {
            audioSource[0].time = _roopStartTime;
        }
    }
}
