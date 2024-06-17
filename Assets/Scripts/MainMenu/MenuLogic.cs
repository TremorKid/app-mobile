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
        
        
        public static bool IsInitialQuiz;
        
        private void Start()
        {
            if (IsInitialQuiz)
            {
                activityBtn.enabled = false;
                instructionBtn.enabled = false;
                exitBtn.enabled = false;
                activityBtn.image.color = SharedTools.ChangeColor("disable");
                instructionBtn.image.color = SharedTools.ChangeColor("disable");
                exitBtn.image.color = SharedTools.ChangeColor("disable");
            }
            else
            {
                activityBtn.enabled = true;
                instructionBtn.enabled = true;
                exitBtn.enabled = true;
                activityBtn.image.color = SharedTools.ChangeColor("enable");
                instructionBtn.image.color = SharedTools.ChangeColor("enable");
                exitBtn.image.color = SharedTools.ChangeColor("enable");
            }
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
