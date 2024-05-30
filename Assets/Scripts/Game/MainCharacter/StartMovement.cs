using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovement : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public int layerIndex1;
    public int layerIndex2;
    public AudioClip birdSongSoundEffectClip;
    public AudioClip displacementSoundEffectClip;
    public AudioClip talkSoundEffectClip;
    private AudioSource audioSource;

    void Start()
    {
		//Sound
	    audioSource = GetComponent<AudioSource>();
	    
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
	    if (audioSource.clip == talkSoundEffectClip && audioSource.isPlaying)
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
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") && 
		    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && 
		    !animator.IsInTransition(0))
		{
			//Audio
			audioSource.Pause();
			audioSource.clip = talkSoundEffectClip;
			//Animation
			animator.SetInteger("Actions", 1);
			audioSource.Play();
		}
    }

	//void ControlAudioParameters()
	//{
	//	
	//}
}
