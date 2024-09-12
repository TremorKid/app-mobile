using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cube; // Cubo asignable desde el Inspector
    private bool isRed = true;
    private Renderer cubeRenderer;
    
    void Start()
    {
        if (cube != null)
        {
            cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.red; // Color inicial
        }
        else
        {
            Debug.LogError("Cubo no asignado en el Inspector.");
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detectar toque en Android
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);
    
            RaycastHit hit;
            if (Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit))
            {
                if (hit.transform.gameObject == cube) // Verificar si el objeto tocado es el cubo
                {
                    ChangeCubeColor();
                }
            }
        }
    }
    
    void ChangeCubeColor()
    {
        if (cubeRenderer != null)
        {
            if (isRed)
            {
                cubeRenderer.material.color = Color.green;
            }
            else
            {
                cubeRenderer.material.color = Color.red;
            }
            isRed = !isRed;
        }
    }
}