using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovement : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public int layerIndex1;
    public int layerIndex2;

    void Start()
    {
        animator.SetLayerWeight(layerIndex1, 1f);
        animator.SetLayerWeight(layerIndex2, 2f);
    }

    void Update()
    {
		ControlAnimationPartameters();
    }
	
	void ControlAnimationPartameters()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
		{
			animator.SetInteger("Actions", 1);
		}
    }
}
