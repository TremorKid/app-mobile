using UnityEngine;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
    public class PauseMenuLogic : MonoBehaviour
    {
        public void Pause()
        {
            Time.timeScale = 0f;
        }
    
        public void Resume()
        {
            Time.timeScale = 1f;
        }

        public void BackMainMenu()
        {
            Resume();
            SceneManager.LoadScene(2);
        }
    }
}