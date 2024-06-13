using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class QuizNavigation : MonoBehaviour
{
    private string _url = "http://3.232.140.223:8080/service/quiz/";
    private QuizBean _quizBean;
    private int _index;
    void Start()
    {
        _quizBean = new QuizBean();
        _index = 0;
    }
    
    public void IncrementIndex()
    {
        Debug.Log("INCREMENTANDO: " + _index);
        ++_index;
        
        _quizBean.quiz1 = "value1";
        _quizBean.quiz2 = "value2";
        _quizBean.quiz3 = "value3";
        _quizBean.quiz4 = "value4";
        _quizBean.quiz5 = "value5";
        _quizBean.quiz6 = "value6";
        _quizBean.quiz7 = "value7";
        _quizBean.quiz8 = "value8";
        _quizBean.quiz9 = "value9";
        _quizBean.quiz10 = "value10";
        _quizBean.userName = "ErikcCortez";
        Debug.Log("EMPEZAMOS SETEAR VALORES" + _quizBean);
    }
    
    public void DecrementIndex()
    {
        --_index;
    }

    public void SetOptionQuiz()
    {
        // Debug.Log("EMPEZAMOS 111111111");
        // if (_index == 1)
        // {
        //     _quizBean.quiz1 = "value1";
        // } else if (_index == 2)
        // {
        //     _quizBean.quiz2 = "value2";
        // } else if (_index == 3)
        // {
        //     _quizBean.quiz3 = "value3";
        // } else if (_index == 4)
        // {
        //     _quizBean.quiz4 = "value4";
        // } else if (_index == 5)
        // {
        //     _quizBean.quiz5 = "value5";
        // } else if (_index == 6)
        // {
        //     _quizBean.quiz6 = "value6";
        // } else if (_index == 7)
        // {
        //     _quizBean.quiz7 = "value7";
        // } else if (_index == 8)
        // {
        //     _quizBean.quiz8 = "value8";
        // } else if (_index == 9)
        // {
        //     _quizBean.quiz9 = "value9";
        // } else if (_index == 10)
        // {
        //     _quizBean.quiz10 = "value10";
        // } else if (_index == 10)
        // {
        //     Debug.Log("EMPEZAMOS 11111");
        //     _quizBean.quiz10 = "value10";
        // } else if (_index == 11)
        // {
        //     Debug.Log("EMPEZAMOS ENVIAR1111111");
        //     _quizBean.userName = "ErikcCortez";
        //     // SendQuiz(URL, _quizBean);
        // } 
    }
    
    public void SendQuiz()
    {
        _quizBean.quiz1 = "value1";
        _quizBean.quiz2 = "value2";
        _quizBean.quiz3 = "value3";
        _quizBean.quiz4 = "value4";
        _quizBean.quiz5 = "value5";
        _quizBean.quiz6 = "value6";
        _quizBean.quiz7 = "value7";
        _quizBean.quiz8 = "value8";
        _quizBean.quiz9 = "value9";
        _quizBean.quiz10 = "value10";
        _quizBean.userName = "ErikcCortez";
        
        Debug.Log("EMPEZAMOS ENVIAR " + _quizBean);
        string quizBeanJson = JsonUtility.ToJson(_quizBean);
        Debug.Log("EMPEZAMOS ENVIAR " + quizBeanJson);
        StartCoroutine(SendQuizCoroutine(quizBeanJson));
    }

    private IEnumerator SendQuizCoroutine(string quizBeanJson)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(_url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(quizBeanJson);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Enviar la solicitud
            UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();

            // Esperar hasta que se complete la solicitud
            while (!asyncOperation.isDone)
                yield return null;

            Debug.Log("Mensaje enviado correctamente: " + webRequest.result);
            
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + webRequest.error);
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("Mensaje enviado correctamente: " + webRequest.result);
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
        }
    }
    


}
