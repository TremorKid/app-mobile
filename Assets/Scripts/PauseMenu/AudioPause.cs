using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPause : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    
    public void PauseAudio()
    {
        audio.Pause();
    }

    // Update is called once per frame
    public void ResumeAudio()
    {
        audio.Play();
    }
}
