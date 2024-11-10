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
        public TextMeshProUGUI validationMessage;
        private int index;
        
        private const string TextSendBtn = "Enviar";
        private const string MenuScene = "Menu";
        // private const string PathOriginalBtn = "unity_builtin_extra";
        private const string PathArrowBtn = "Pictures/arrow_right";
        // private const string Learning = "Learning";
        
        private void Start()
        {
            quizBeanSend = new QuizBean();
            appService = gameObject.AddComponent<AppService>();
            index = 0;
            
            UpdateTextQuestion();
            prevBtn.gameObject.SetActive(false);
        }
        
        public void NextQuestion()
        {
            if (string.IsNullOrEmpty(GetCurrentAnswer()))
            {
                validationMessage.text = "Por favor, responde la pregunta antes de continuar.";
                return;
            }
            
            ++index;
            
            switch (index)
            {
                case 10:
                    quizBeanSend.userName = PlayerPrefs.GetString("userName");
                    quizBeanSend.isFirstQuiz = isInitialQuiz;
                    SendQuiz(JsonUtility.ToJson(quizBeanSend));
                
                    MenuLogic.isInitialQuiz = isInitialQuiz;
                    SceneManager.LoadScene(MenuScene);
                    return;
                case 9:
                    // nextBtn.image.sprite = Resources.Load<Sprite>(PathOriginalBtn);
                    nextBtn.image.sprite = null;
                    nextBtn.image.type = Image.Type.Sliced;
                    nextBtn.image.fillCenter = true;
                    nextBtn.image.pixelsPerUnitMultiplier = 0.35f;
                    nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = TextSendBtn;
                    var color = SharedTools.ChangeColor(SharedTools.Coral);
                    nextBtn.image.color = color;
                    var block = nextBtn.colors;
                    block.disabledColor = color;
                    block.pressedColor = color;
                    block.normalColor = color;
                    block.selectedColor = color;
                    block.highlightedColor = Color.white;
                    nextBtn.colors = block;
                    break;
            }
            
            if (index != 0)
            {
                prevBtn.gameObject.SetActive(true);
            }
            
            HighlightSelectedAnswer();
        }
        
        public void PrevQuestion()
        {
            --index;

            switch (index)
            {
                case 8:
                    nextBtn.image.sprite = Resources.Load<Sprite>(PathArrowBtn);
                    nextBtn.image.type = Image.Type.Simple;
                    nextBtn.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
                    break;
                case 0:
                    prevBtn.gameObject.SetActive(false);
                    break;
            }

            HighlightSelectedAnswer();
            validationMessage.text = string.Empty;
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

            optionBtn.image.color = SharedTools.ChangeColor(SharedTools.Mark);
            validationMessage.text = string.Empty;
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
        
        private void UpdateTextQuestion()
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
            var color = Color.white;
            meshOption1Btn.image.color = color;
            meshOption2Btn.image.color = color;
            meshOption3Btn.image.color = color;
            meshOption4Btn.image.color = color;
        }
        
        private void HighlightSelectedAnswer()
        {
            UpdateTextQuestion();
            OrganizeAlternatives();
            ResetButtonColor();
            var currentAnswer = GetCurrentAnswer();
            
            if (string.IsNullOrEmpty(currentAnswer)) return;
            
            var selectedButton = GetButtonByText(currentAnswer);
            if (selectedButton)
            {
                selectedButton.image.color = SharedTools.ChangeColor(SharedTools.Mark);
            }
        }
        
        private string GetCurrentAnswer()
        {
            return index switch
            {
                0 => quizBeanSend.quiz1,
                1 => quizBeanSend.quiz2,
                2 => quizBeanSend.quiz3,
                3 => quizBeanSend.quiz4,
                4 => quizBeanSend.quiz5,
                5 => quizBeanSend.quiz6,
                6 => quizBeanSend.quiz7,
                7 => quizBeanSend.quiz8,
                8 => quizBeanSend.quiz9,
                9 => quizBeanSend.quiz10,
                _ => null
            };
        }

        private Button GetButtonByText(string text)
        {
            if (meshOption1Btn.GetComponentInChildren<TextMeshProUGUI>().text == text) return meshOption1Btn;
            if (meshOption2Btn.GetComponentInChildren<TextMeshProUGUI>().text == text) return meshOption2Btn;
            if (meshOption3Btn.GetComponentInChildren<TextMeshProUGUI>().text == text) return meshOption3Btn;
            return meshOption4Btn.GetComponentInChildren<TextMeshProUGUI>().text == text ? meshOption4Btn : null;
        }
    }
}
