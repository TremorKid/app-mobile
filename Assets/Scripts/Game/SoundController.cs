using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip birdSongSoundEffectClip;
    public AudioClip displacementSoundEffectClip;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = birdSongSoundEffectClip;
        audioSource.Play();
    }
    
    void Update()
    {
        
    }

    void PlayAudio()
    {
        
    }
}
