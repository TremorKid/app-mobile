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

    }
}
