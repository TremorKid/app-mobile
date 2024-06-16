using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuLogic : MonoBehaviour
    {
        public void Play(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Player has quit the game");
        }
    }   
}
