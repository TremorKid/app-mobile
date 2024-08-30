using System;
using Newtonsoft.Json;
using Quiz;
using Quiz.Enum;
using Quiz.Model;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login
{
    public class Login : MonoBehaviour
    {
        private AppService _appService;
        public TMP_InputField inputText;
        private string _userName;
        
        private const string QuizScene = "Quiz";
        private const string UserName = "userName";
        private const string StringEmpty = "";
        private void Start()
        {
            // PlayerPrefs.DeleteKey("userName");
            _userName = PlayerPrefs.GetString(UserName);
            _appService = gameObject.AddComponent<AppService>();
            
            // GetGeneralParameter
            _appService.GetGeneralParameterValue(GeneralParameterEnum.QuizTemplate, value =>
            {
                try {
                    QuizNavigation.questionsTemplate = JsonConvert.DeserializeObject<QuestionsTemplate>(value);
                } catch (Exception e) {
                    Debug.LogError("Error al deserializar JSON: " + e.Message);
                }
            });
            
            if (!string.IsNullOrEmpty(_userName))
            {
                // SceneManager.LoadScene(QuizScene);
            }
        }
        
        public void SendName()
        {
            PlayerPrefs.SetString(UserName, inputText.text);
            QuizNavigation.isInitialQuiz = true;
            SceneManager.LoadScene(QuizScene);
        }
        
        public void ResetInput()
        {
            inputText.text = StringEmpty;
        }
    }
}

