using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovement : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public int layerIndex1;
    public int layerIndex2;
    public AudioController audioController;
    public AudioClip birdSongSoundEffectClip;
    //public AudioClip displacementSoundEffectClip;
    public AudioClip Learning_Guide_Escene1;
    public AudioClip Prueba;
    private AudioSource audioSource;
    private float currentTime = 0f;
    private short contAudioReproduce = 0;


    void Start()
    {
		//Sound
	    audioSource = GetComponent<AudioSource>();
	    InvokeRepeating("CheckAudioTime", 0f, 1f);
	    //Animation
        animator.SetLayerWeight(layerIndex1, 1f);
        animator.SetLayerWeight(layerIndex2, 2f);
    }

    void Update()
    {
		ControlAnimationPartameters();
    }

    void AudioTalkController()
    {
	    if (audioSource.clip == Learning_Guide_Escene1 && audioSource.isPlaying)
	    {
		    animator.SetInteger("Actions", 1);
	    }
    }

    void InitialMovementClipPlay()
    {
	    audioSource.clip = birdSongSoundEffectClip;
	    audioSource.Play();
    }
    

	
	void ControlAnimationPartameters()
	{
		//Animation StartMovement Start
		//if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") && 
		//    animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		//{
//
//
		//}
		
		//Animation StartMovement End
		if (contAudioReproduce == 0)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") && 
			    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && 
			    !animator.IsInTransition(0))
			{
				//Audio
				audioSource.Pause();
				audioSource.clip = Learning_Guide_Escene1;
				contAudioReproduce++;
				//Animation
				animator.SetInteger("Actions", 1);
				audioSource.Play();
			}	
		}
    }

	//void ControlAudioParameters()
	//{
	//	
	//}
	
	void CheckAudioTime()
	{
		currentTime = audioSource.time;

		// Intervalo del segundo 0 al 10
		if (audioSource.clip == Learning_Guide_Escene1 && currentTime >= 0 && currentTime <= 10)
		{
			if (currentTime > 5 && currentTime < 6)
			{
				audioController.PauseAudioForSeconds(5f);
			}
			Debug.Log("Intervalo 0-10 segundos");
		}
		// Intervalo del segundo 11 al 20
		else if (audioSource.clip == Learning_Guide_Escene1 && currentTime > 10 && currentTime <= 20)
		{
			Debug.Log("Intervalo 11-20 segundos");
		}
		// Intervalo del segundo 21 al 40
		else if (audioSource.clip == Learning_Guide_Escene1 && currentTime > 20 && currentTime <= 40)
		{
			Debug.Log("Intervalo 21-40 segundos");
		}
	}
}
