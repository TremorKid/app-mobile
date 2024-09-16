using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Environment;

namespace Service
{
    public class AppService : MonoBehaviour
    {
            
        private readonly string quizUrl = Host.BaseUrl + "/service/quiz/";
        private readonly string generalParameterUrl = Host.BaseUrl + "/service/general-parameter/";
        
        public void SendQuiz(string quizBeanJson)
        {
            StartCoroutine(SendQuizCoroutine(quizBeanJson));
        }
        
        private IEnumerator SendQuizCoroutine(string quizBeanJson)
        {
            using var webRequest = new UnityWebRequest(quizUrl, "POST");
        
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(quizBeanJson);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            var asyncOperation = webRequest.SendWebRequest();
            
            while (!asyncOperation.isDone)
                yield return null;
            
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                Debug.Log($"Mensaje enviado correctamente: {webRequest.result}");
            }
            
            Debug.Log($"Response: {webRequest.downloadHandler.text}");
        }
        
        public void GetGeneralParameterValue(string code, Action<string> callback)
        {
            StartCoroutine(GetGeneralParameterCoroutine(code, callback));
        }
        
        private IEnumerator GetGeneralParameterCoroutine(string code, Action<string> callback)
        {
            using var webRequest = new UnityWebRequest(generalParameterUrl + code, "GET");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
                
            yield return webRequest.SendWebRequest();
                
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {webRequest.error}");
                callback(null);
            }
            else
            {
                callback(webRequest.downloadHandler.text);
            }
        }
    }
}