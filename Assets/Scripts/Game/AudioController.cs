using System.Collections;
using UnityEngine;

namespace Game
{
    public class AudioController : MonoBehaviour
    {
        public AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource)
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
}