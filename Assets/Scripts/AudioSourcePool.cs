using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public int sourceNumber = 10;
    public AudioSource AudioSourcePrefab;

    private List<AudioSource> audioSources;
    private void Awake()
    {
        audioSources = new List<AudioSource>();
        for (int i = 0; i < sourceNumber; i++)
        {
            audioSources.Add(GameObject.Instantiate(AudioSourcePrefab, this.transform));
        }
    }

    public AudioSource getSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }
        AudioSource toRet = GameObject.Instantiate(AudioSourcePrefab, this.transform);
        audioSources.Add(toRet);
        return toRet;



    }

}

