using System;
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
            // PlayerPrefs.DeleteKey("userName");
            try
            {
                _userName = PlayerPrefs.GetString(UserName);
                Debug.Log("que es esta weba: " + _userName);
                if (!string.IsNullOrEmpty(_userName))
                {
                    SceneManager.LoadScene(QuizScene);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("que es esta weba: " + e);
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

