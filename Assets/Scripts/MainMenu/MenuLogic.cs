using Shared;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MenuLogic : MonoBehaviour
    {
        public Button activityBtn;
        public Button instructionBtn;
        public Button exitBtn;
        
        
        public static bool isInitialQuiz;
        
        private void Start()
        {
            if (isInitialQuiz)
            {
                activityBtn.image.color = SharedTools.ChangeColor("disable");
                instructionBtn.image.color = SharedTools.ChangeColor("disable");
                exitBtn.image.color = SharedTools.ChangeColor("disable");
            }
            else
            {
                activityBtn.image.color = SharedTools.ChangeColor("enable");
                instructionBtn.image.color = SharedTools.ChangeColor("enable");
                exitBtn.image.color = SharedTools.ChangeColor("enable");
            }
            
            activityBtn.enabled = !isInitialQuiz;
            instructionBtn.enabled = !isInitialQuiz;
            exitBtn.enabled = !isInitialQuiz;
        }

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
