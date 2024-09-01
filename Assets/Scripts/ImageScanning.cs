using System.Collections.Generic;
using Quiz;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ImageScanning : MonoBehaviour
{
    private short countEmergencyBackpack;
    private short countColumn;
    private short countTable;
    private short countStair;
    private short countTelevision;
    private short countWindow;
    private short countMeetingPoint;
    public AudioSource audioSource;
    public List<AudioClip> duringList; 
    private Dictionary<string, AudioClip> duringDictionary;

    private void Start()
    {
        duringDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in duringList)
        {
            duringDictionary[clip.name] = clip;
        }
    }

    private void Update()
    {
        QuizNavigation.isInitialQuiz = false;
        DuringSceneEnding("Quiz");
    }
    
    // ASIGNAR UN AUDIOSOURCE DE LA LISTA DE AUDIOCLIP
    private void SetAudioClipByName(string clipName)
    {
        if (duringDictionary.TryGetValue(clipName, out var value))
        {
            audioSource.clip = value;
        }
    }
    
    // CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN
    public void IncreaseEmergencyBackpackCount()
    {
        if (countEmergencyBackpack == 0)
        {
            countEmergencyBackpack++;
        }
        
        SetAudioClipByName("During_EmergencyBackpack");
        audioSource.Play();
    }
    public void IncreaseFirstAidKitCount()
    {
        if (countEmergencyBackpack == 0)
        {
            countEmergencyBackpack++;   
        }
    }
    
    public void IncreaseColumnCount()
    {
        if (countColumn == 0)
        {
            countColumn++;
        }
        
        SetAudioClipByName("During_SafeZone_1");
        audioSource.Play();
    }
    
    public void IncreaseTableCount()
    {
        if (countTable == 0)
        {
            countTable++;
        }
        
        SetAudioClipByName("During_SafeZone_2");
        audioSource.Play();
    }
    
    public void IncreaseStairCount()
    {
        if (countStair == 0)
        {
            countStair++;
        }
    }
    
    public void IncreaseTelevisionCount()
    {
        if (countTelevision == 0)
        {
            countTelevision++;
        }
        
        SetAudioClipByName("During_UnsafeZone_Television");
        audioSource.Play();
    }
    
    public void IncreaseWindowCount()
    {
        if (countWindow == 0)
        {
            countWindow++;
        }
        
        SetAudioClipByName("During_UnsafeZone_Window");
        audioSource.Play();
    }
    
    public void IncreaseMeetingPointCount()
    {
        if (countMeetingPoint == 0)
        {
            countMeetingPoint++;
        }
        
        SetAudioClipByName("During_SafeZone_3");
        audioSource.Play();
    }

    private void DuringSceneEnding(string nameScene)
    {
        if (countTable >= 1 && countMeetingPoint >= 1 && countColumn >= 1)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
