using System;
using Newtonsoft.Json;
using Quiz.Bean;
using Quiz.Enum;
using Quiz.Model;
using Service;
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
        private QuestionsTemplate _questionsTemplate;
        
        // Components UI
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
        private const string Learning = "Learning";
        
        private void Start()
        {
            _quizBeanSend = new QuizBean();
            _appService = gameObject.AddComponent<AppService>();
            _index = 0;

            _appService.GetGeneralParameterValue(GeneralParameterEnum.QuizTemplate, value =>
            {
                try {
                    _questionsTemplate = JsonConvert.DeserializeObject<QuestionsTemplate>(value);
                } catch (Exception e) {
                    Debug.LogError("Error al deserializar JSON: " + e.Message);
                }
            });
            // UpdateText(_index);
            prevBtn.gameObject.SetActive(false);
        }
        
        public void IncrementIndex()
        {
            ++_index;
            if (_index == 10)
            {
                _quizBeanSend.userName = PlayerPrefs.GetString("userName");
                SendQuiz(JsonUtility.ToJson(_quizBeanSend));
                SceneManager.LoadScene(MenuScene);
            }
            else
            {
                UpdateText(_index);
                if (_index == 9)
                {
                    nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = TextSendBtn;
                    nextBtn.image.color = ChangeColor("send");
                }
            }

            if (_index != 0)
            {
                prevBtn.gameObject.SetActive(true);
            }

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
                    Debug.LogError("Opción no encontrada.");
                    break;
            }

            optionBtn.image.color = ChangeColor("mark");
        }
        
        private void SendQuiz(string sendQuizBean)
        {
            _appService.SendQuiz(sendQuizBean);
        }
        
        private void UpdateText(int index)
        {
            textMeshQuestion.text = _questionsTemplate.questions[index].question;
            meshOption1Btn.GetComponentInChildren<TextMeshProUGUI>().text = _questionsTemplate.questions[index].alternative1;
            meshOption2Btn.GetComponentInChildren<TextMeshProUGUI>().text = _questionsTemplate.questions[index].alternative2;
            meshOption3Btn.GetComponentInChildren<TextMeshProUGUI>().text = _questionsTemplate.questions[index].alternative3;
            meshOption4Btn.GetComponentInChildren<TextMeshProUGUI>().text = _questionsTemplate.questions[index].alternative4;
            meshOption5Btn.GetComponentInChildren<TextMeshProUGUI>().text = _questionsTemplate.questions[index].alternative5;
        }

        private static Color ChangeColor(string code)
        {
            return code switch
            {
                "mark" => new Color32(17, 170, 0, 255),
                "send" => new Color32(10, 115, 0, 255),
                "disable" => new Color32(150, 150, 150, 255),
                _ => new Color32(255, 255, 255, 255)
            };
        }

        private void ResetButtonColor()
        {
            var color = ChangeColor("none");
            meshOption1Btn.image.color = color;
            meshOption2Btn.image.color = color;
            meshOption3Btn.image.color = color;
            meshOption4Btn.image.color = color;
            meshOption5Btn.image.color = color;
        }

    }
}
