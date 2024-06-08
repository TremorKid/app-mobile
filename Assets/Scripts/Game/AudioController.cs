using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    public void PauseAudioForSeconds(float seconds)
    {
        StartCoroutine(PauseAndResumeAudio(seconds));
    }

    private IEnumerator PauseAndResumeAudio(float seconds)
    {
        Debug.Log("Pausando audio");
        audioSource.Pause();
        yield return new WaitForSeconds(seconds);
        Debug.Log("Reanudando audio");
        audioSource.UnPause();
    }
}