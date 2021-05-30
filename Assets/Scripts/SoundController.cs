using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioClip[] Clip;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(int index)
    {
        _audioSource.clip = Clip[index];
        _audioSource.Play();
    }
}
