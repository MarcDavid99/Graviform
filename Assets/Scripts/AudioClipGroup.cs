using System.Collections;
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

    public float minVolumeFromOptions;
    public float maxVolumeFromOptions;

    private float timestamp;
    private AudioSourcePool audioSourcePool;

    public void Awake()
    {
        minVolumeFromOptions = VolumeMin;
        maxVolumeFromOptions = VolumeMax;

    }

    public void Play(AudioSource audioSource)
    {
      
        if (audioClips == null || audioClips.Count <= 0)
        {
            return;
        }

        //if (Time.time)
        
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

    public void Mute()
    {
        VolumeMax = 0;
        VolumeMin = 0;
    }

    public void UnMute()
    {
        VolumeMax = maxVolumeFromOptions;
        VolumeMin = minVolumeFromOptions;
    }

}