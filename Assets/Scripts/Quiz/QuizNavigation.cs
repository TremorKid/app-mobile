using System;
using MainMenu;
using Quiz.Bean;
using Quiz.Model;
using Service;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizNavigation : MonoBehaviour
    {
        private AppService appService;
        private QuizBean quizBeanSend;
        
        // Components UI
        public static QuestionsTemplate questionsTemplate;
        public static bool isInitialQuiz;
        public TextMeshProUGUI textMeshQuestion;
        public Button meshOption1Btn;
        public Button meshOption2Btn;
        public Button meshOption3Btn;
        public Button meshOption4Btn;
        public Button nextBtn;
        public Button prevBtn;
        private int index;
        
        private const string TextSendBtn = "Enviar";
        private const string MenuScene = "Menu";
        // private const string Learning = "Learning";
        
        private void Start()
        {
            quizBeanSend = new QuizBean();
            appService = gameObject.AddComponent<AppService>();
            index = 0;
            
            UpdateText();
            prevBtn.gameObject.SetActive(false);
        }
        
        public void IncrementIndex()
        {
            ++index;
            if (index == 10)
            {
                quizBeanSend.userName = PlayerPrefs.GetString("userName");
                quizBeanSend.isFirstQuiz = isInitialQuiz;
                SendQuiz(JsonUtility.ToJson(quizBeanSend));
                
                MenuLogic.isInitialQuiz = isInitialQuiz;
                SceneManager.LoadScene(MenuScene);
                return;
            }
            else
            {
                UpdateText();
                if (index == 9)
                {
                    nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = TextSendBtn;
                    nextBtn.image.color = SharedTools.ChangeColor("send");
                }
            }

            if (index != 0)
            {
                prevBtn.gameObject.SetActive(true);
            }

            OrganizeAlternatives();
            
            ResetButtonColor();
        }
        
        public void DecrementIndex()
        {
            --index;
            UpdateText();
            if (index == 0)
            {
                prevBtn.gameObject.SetActive(false);
            }
            OrganizeAlternatives();
            ResetButtonColor();
        }
        
        public void SetOptionQuiz(Button optionBtn)
        {
            ResetButtonColor();
            var optionTextBtn = optionBtn.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (index)
            {
                case 0:
                    quizBeanSend.quiz1 = optionTextBtn;
                    break;
                case 1:
                    quizBeanSend.quiz2 = optionTextBtn;
                    break;
                case 2:
                    quizBeanSend.quiz3 = optionTextBtn;
                    break;
                case 3:
                    quizBeanSend.quiz4 = optionTextBtn;
                    break;
                case 4:
                    quizBeanSend.quiz5 = optionTextBtn;
                    break;
                case 5:
                    quizBeanSend.quiz6 = optionTextBtn;
                    break;
                case 6:
                    quizBeanSend.quiz7 = optionTextBtn;
                    break;
                case 7:
                    quizBeanSend.quiz8 = optionTextBtn;
                    break;
                case 8:
                    quizBeanSend.quiz9 = optionTextBtn;
                    break;
                case 9:
                    quizBeanSend.quiz10 = optionTextBtn;
                    break;
                default:
                    Debug.LogError("Opción no encontrada." + index);
                    break;
            }

            optionBtn.image.color = SharedTools.ChangeColor("mark");
        }
        
        private void SendQuiz(string sendQuizBean)
        {
            try
            {
                appService.SendQuiz(sendQuizBean);
            }
            catch (Exception e)
            {
                Debug.LogError("Error en Service: " + e);
            }
        }
        
        private void UpdateText()
        {
            textMeshQuestion.text = questionsTemplate.questions[index].question;
            meshOption1Btn.GetComponentInChildren<TextMeshProUGUI>().text = questionsTemplate.questions[index].alternative1;
            meshOption2Btn.GetComponentInChildren<TextMeshProUGUI>().text = questionsTemplate.questions[index].alternative2;
            meshOption3Btn.GetComponentInChildren<TextMeshProUGUI>().text = questionsTemplate.questions[index].alternative3;
            meshOption4Btn.GetComponentInChildren<TextMeshProUGUI>().text = questionsTemplate.questions[index].alternative4;
        }
        
        private void OrganizeAlternatives()
        {
            ResetPositionAlternatives();
            if (questionsTemplate.questions[index].alternative3 != null) return;
            meshOption1Btn.transform.localPosition = new Vector3(-90, 0, 0);
            meshOption2Btn.transform.localPosition = new Vector3(90, 0, 0);
            meshOption3Btn.gameObject.SetActive(false);
            meshOption4Btn.gameObject.SetActive(false);
        }
        
        private void ResetPositionAlternatives()
        {
            meshOption1Btn.transform.localPosition = new Vector3(-90, 0, 0);
            meshOption2Btn.transform.localPosition = new Vector3(-90, -70, 0);
            meshOption3Btn.gameObject.SetActive(true);
            meshOption4Btn.gameObject.SetActive(true);
        }

        private void ResetButtonColor()
        {
            var color = SharedTools.ChangeColor("none");
            meshOption1Btn.image.color = color;
            meshOption2Btn.image.color = color;
            meshOption3Btn.image.color = color;
            meshOption4Btn.image.color = color;
        }

    }
}
