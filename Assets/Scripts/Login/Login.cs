using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login
{
    public class Login : MonoBehaviour
    {
        public TMP_InputField inputText;
        private string _userName;
        
        private const string QuizScene = "Quiz";
        private const string UserName = "userName";
        private const string StringEmpty = "";
        private void Start()
        {
            _userName = PlayerPrefs.GetString(UserName);
            if (_userName != null)
            {
                SceneManager.LoadScene(QuizScene);
            }
        }
        
        public void SendName()
        {
            PlayerPrefs.SetString(UserName, inputText.text);
            SceneManager.LoadScene(QuizScene);
        }
        
        public void ResetInput()
        {
            inputText.text = StringEmpty;
        }
    }
}

