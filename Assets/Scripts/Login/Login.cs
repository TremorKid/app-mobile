using System;
using Newtonsoft.Json;
using Quiz;
using Quiz.Enum;
using Quiz.Model;
using Service;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Login
{
    public class Login : MonoBehaviour
    {
        private AppService appService;
        public TMP_InputField inputText;
        private string userName;
        private bool validOk;
        
        private const string QuizSceneName = "Quiz";
        private const string UserNameKey = "userName";

        private void Start()
        {
            appService = gameObject.AddComponent<AppService>();
            
            // Get Questions Template from General Parameter
            appService.GetGeneralParameterValue(GeneralParameterEnum.QuizTemplate, value =>
            {
                try {
                    QuizNavigation.questionsTemplate = JsonConvert.DeserializeObject<QuestionsTemplate>(value);
                } catch (Exception e) {
                    Debug.LogError("Error al deserializar JSON: " + e.Message);
                }
            });
            
            
            var skipLogin = true;
            appService.GetGeneralParameterValue(GeneralParameterEnum.SkipLogin, value =>
            {
                try {
                    skipLogin = bool.Parse(value);
                } catch (Exception e) {
                    Debug.LogError("Error al parsear valor: " + e.Message);
                }
            });

            if (skipLogin)
            {
                PlayerPrefs.DeleteKey("userName");
            }
            userName = PlayerPrefs.GetString(UserNameKey);
            if (!string.IsNullOrEmpty(userName))
            {
                SceneManager.LoadScene(QuizSceneName);
            }
        }
        
        public void SendName()
        {
            if (!validOk) return;
            
            PlayerPrefs.SetString(UserNameKey, inputText.text);
            QuizNavigation.isInitialQuiz = true;
            SceneManager.LoadScene(QuizSceneName);
        }
        
        public void ResetInput()
        {
            inputText.text = string.Empty;
        }
        
        public void ValidationName(Button button)
        {
            validOk = inputText.text.Length > 0;
            
            var color = SharedTools.ChangeColor(validOk ? SharedTools.Coral : SharedTools.Disable);
            button.image.color = color;
            
            var block = button.colors;
            block.disabledColor = color;
            block.pressedColor = color;
            block.normalColor = color;
            block.selectedColor = color;
            block.highlightedColor = Color.white;
            button.colors = block;
        }
    }
}

