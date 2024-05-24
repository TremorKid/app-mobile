using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}