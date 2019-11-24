﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioClipGroup")]
public class AudioClipGroup : ScriptableObject


{

    [Range(0, 2)]
    public float VolumeMin = 1;
    [Range(0, 2)]
    public float VolumeMax = 1;
    [Range(0, 2)]
    public float PitchMin = 1;
    [Range(0, 2)]
    public float PitchMax = 1;
    public float Cooldown = 0.1f;
    public List<AudioClip> audioClips;

    private float timestamp;
    private AudioSourcePool audioSourcePool;


    public void Play(AudioSource audioSource)
    {
        Debug.Log("play(audiosource) enne kontrolli");
        if (audioClips == null || audioClips.Count <= 0)
        {
            return;
        }

        //if (Time.time)
        Debug.Log("play(audiosource)");
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.volume = Random.Range(VolumeMin, VolumeMax);
        audioSource.pitch = Random.Range(PitchMin, PitchMax);

        audioSource.Play();
    }


    public void Play()
    {
        if (audioSourcePool == null)
        {
            audioSourcePool = FindObjectOfType<AudioSourcePool>();
        }
        Play(audioSourcePool.getSource());
    }

}