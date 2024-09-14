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
            var color = SharedTools.ChangeColor(isInitialQuiz ? SharedTools.Disable : SharedTools.Enable);
            
            activityBtn.image.color = color;
            
            activityBtn.enabled = !isInitialQuiz;
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
