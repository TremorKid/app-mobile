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
        private AppService _appService;
        private QuizBean _quizBeanSend;
        
        // Components UI
        public static QuestionsTemplate QuestionsTemp;
        public static bool IsInitialQuiz;
        public TextMeshProUGUI textMeshQuestion;
        public Button meshOption1Btn;
        public Button meshOption2Btn;
        public Button meshOption3Btn;
        public Button meshOption4Btn;
        public Button meshOption5Btn;
        public Button nextBtn;
        public Button prevBtn;
        private int _index;
        
        private const string TextSendBtn = "Enviar";
        private const string MenuScene = "Menu";
        // private const string Learning = "Learning";
        
        private void Start()
        {
            _quizBeanSend = new QuizBean();
            _appService = gameObject.AddComponent<AppService>();
            _index = 0;
            
            UpdateText(_index);
            prevBtn.gameObject.SetActive(false);
        }
        
        public void IncrementIndex()
        {
            ++_index;
            if (_index == 10)
            {
                _quizBeanSend.userName = PlayerPrefs.GetString("userName");
                _quizBeanSend.isFirstQuiz = IsInitialQuiz;
                SendQuiz(JsonUtility.ToJson(_quizBeanSend));
                
                MenuLogic.IsInitialQuiz = IsInitialQuiz;
                SceneManager.LoadScene(MenuScene);
                return;
            }
            else
            {
                UpdateText(_index);
                if (_index == 9)
                {
                    nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = TextSendBtn;
                    nextBtn.image.color = SharedTools.ChangeColor("send");
                }
            }

            if (_index != 0)
            {
                prevBtn.gameObject.SetActive(true);
            }

            OrganiceAlternatives(_index);
            
            ResetButtonColor();
        }
        
        public void DecrementIndex()
        {
            --_index;
            UpdateText(_index);
            if (_index == 0)
            {
                prevBtn.gameObject.SetActive(false);
            }
            OrganiceAlternatives(_index);
            ResetButtonColor();
        }
        
        public void SetOptionQuiz(Button optionBtn)
        {
            ResetButtonColor();
            var optionTextBtn = optionBtn.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (_index)
            {
                case 0:
                    _quizBeanSend.quiz1 = optionTextBtn;
                    break;
                case 1:
                    _quizBeanSend.quiz2 = optionTextBtn;
                    break;
                case 2:
                    _quizBeanSend.quiz3 = optionTextBtn;
                    break;
                case 3:
                    _quizBeanSend.quiz4 = optionTextBtn;
                    break;
                case 4:
                    _quizBeanSend.quiz5 = optionTextBtn;
                    break;
                case 5:
                    _quizBeanSend.quiz6 = optionTextBtn;
                    break;
                case 6:
                    _quizBeanSend.quiz7 = optionTextBtn;
                    break;
                case 7:
                    _quizBeanSend.quiz8 = optionTextBtn;
                    break;
                case 8:
                    _quizBeanSend.quiz9 = optionTextBtn;
                    break;
                case 9:
                    _quizBeanSend.quiz10 = optionTextBtn;
                    break;
                default:
                    Debug.LogError("Opción no encontrada." + _index);
                    break;
            }

            optionBtn.image.color = SharedTools.ChangeColor("mark");
        }
        
        private void SendQuiz(string sendQuizBean)
        {
            try
            {
                _appService.SendQuiz(sendQuizBean);
            }
            catch (Exception e)
            {
                Debug.LogError("Error en Service: " + e);
            }
        }
        
        private void UpdateText(int index)
        {
            textMeshQuestion.text = QuestionsTemp.questions[index].question;
            meshOption1Btn.GetComponentInChildren<TextMeshProUGUI>().text = QuestionsTemp.questions[index].alternative1;
            meshOption2Btn.GetComponentInChildren<TextMeshProUGUI>().text = QuestionsTemp.questions[index].alternative2;
            meshOption3Btn.GetComponentInChildren<TextMeshProUGUI>().text = QuestionsTemp.questions[index].alternative3;
            meshOption4Btn.GetComponentInChildren<TextMeshProUGUI>().text = QuestionsTemp.questions[index].alternative4;
            meshOption5Btn.GetComponentInChildren<TextMeshProUGUI>().text = QuestionsTemp.questions[index].alternative5;
        }
        
        private void OrganiceAlternatives(int index)
        {
            ResetPositionAlternatives();
            if (QuestionsTemp.questions[index].alternative3 != "") return;
            meshOption1Btn.transform.localPosition = new Vector3(-90, -70, 0);
            meshOption2Btn.transform.localPosition = new Vector3(90, -70, 0);
            meshOption3Btn.gameObject.SetActive(false);
            meshOption4Btn.gameObject.SetActive(false);
            meshOption5Btn.gameObject.SetActive(false);
        }
        
        private void ResetPositionAlternatives()
        {
            meshOption1Btn.transform.localPosition = new Vector3(-90, 0, 0);
            meshOption2Btn.transform.localPosition = new Vector3(-90, -70, 0);
            meshOption3Btn.gameObject.SetActive(true);
            meshOption4Btn.gameObject.SetActive(true);
            meshOption5Btn.gameObject.SetActive(true);
        }

        private void ResetButtonColor()
        {
            var color = SharedTools.ChangeColor("none");
            meshOption1Btn.image.color = color;
            meshOption2Btn.image.color = color;
            meshOption3Btn.image.color = color;
            meshOption4Btn.image.color = color;
            meshOption5Btn.image.color = color;
        }

    }
}
