using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBefore : MonoBehaviour
{
    public GameObject nextButton;
    //EmergencyBackpack counter
    private short countStillBottledWater = 0;
    private short countPortableRadio = 0;
    private short countPolyesterRopes = 0;
    private short countMultipurposeBlade = 0;
    private short countFleeceBlanket = 0;
    private short countFlashlight = 0;
    private short countCannedFood = 0;
    private short countLighter = 0;
    private short countHandTowel = 0;
    private short countAntibacterialGel = 0;
    private short countToothBrush = 0;
    
    //FirstAidKit count
    private short countSterileGauze = 0;
    private short countMask = 0;
    private short countAntibiotics = 0;
    private short countAlcohol = 0;
    private short countAdhesiveTape = 0;
    public AudioSource audioSource;
    public List<AudioClip> beforeNameList; 
    private Dictionary<string, AudioClip> beforeNameDictionary;
    public GameObject firstAidKitCanvas;
    public GameObject emergencyBackpackCanvas;
    void Start()
    {
        beforeNameDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in beforeNameList)
        {
            beforeNameDictionary[clip.name] = clip;
        }
    }
    
    void Update()
    {
        BeforeEnding("Before");
    }
    
    // ASIGNAR UN AUDIOSOURCE DE LA LISTA DE AUDIOCLIP
    private void SetAudioClipByName(string clipName)
    {
        if (beforeNameDictionary.ContainsKey(clipName))
        {
            audioSource.clip = beforeNameDictionary[clipName];
        }
    }
    
    // CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN DE LA MOCHILA DE EMERGENCIA
    public void IncreaseStillBottledWaterCount()
    {
        if (countStillBottledWater == 0)
        {
            countStillBottledWater++;
        }
        
        SetAudioClipByName("guide_stillBottledWater_name");
        audioSource.Play();
    }
    public void IncreasePortableRadioCount()
    {
        if (countPortableRadio == 0)
        {
            countPortableRadio++;   
        }
        
        SetAudioClipByName("guide_portableRadio_name");
        audioSource.Play();
    }
    public void IncreasePolyesterRopesCount()
    {
        if (countPolyesterRopes == 0)
        {
            countPolyesterRopes++;
        }
        
        SetAudioClipByName("guide_polyesterRopes_name");
        audioSource.Play();
    }
    public void IncreaseMultipurposeBladeCount()
    {
        if (countMultipurposeBlade == 0)
        {
            countMultipurposeBlade++;
        }
        
        SetAudioClipByName("guide_multipurposeBlade_name");
        audioSource.Play();
    }
    public void IncreaseFleeceBlanketCount()
    {
        if (countFleeceBlanket == 0)
        {
            countFleeceBlanket++;
        }
        
        SetAudioClipByName("guide_fleeceBlanket_name");
        audioSource.Play();
    }
    public void IncreaseFlashlightCount()
    {
        if (countFlashlight == 0)
        {
            countFlashlight++;
        }
        
        SetAudioClipByName("guide_flashlight_name");
        audioSource.Play();
    }
    public void IncreaseCannedFoodCount()
    {
        if (countCannedFood == 0)
        {
            countCannedFood++;
        }
        
        SetAudioClipByName("guide_cannedFood_name");
        audioSource.Play();
    }
    public void IncreaseLighterCount()
    {
        if (countLighter == 0)
        {
            countLighter++;
        }
        
        SetAudioClipByName("guide_lighter_name");
        audioSource.Play();
    }
    public void IncreaseHandTowelCount()
    {
        if (countHandTowel == 0)
        {
            countHandTowel++;
        }
        
        SetAudioClipByName("guide_handTowel_name");
        audioSource.Play();
    }
    public void IncreaseAntibacterialGelCount()
    {
        if (countAntibacterialGel == 0)
        {
            countAntibacterialGel++;
        }
        
        SetAudioClipByName("guide_antibacterialGel_name");
        audioSource.Play();
    }
    public void IncreaseToothBrushCount()
    {
        if (countToothBrush == 0)
        {
            countToothBrush++;
        }
        
        SetAudioClipByName("guide_toothbrush_name");
        audioSource.Play();
    }
    
    // CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN DEL BOTIQUIN 
    public void IncreaseSterileGauzeCount()
    {
        if (countSterileGauze == 0)
        {
            countSterileGauze++;
        }
        
        SetAudioClipByName("guide_sterileGauze_name");
        audioSource.Play();
    }
    public void IncreaseMaskCount()
    {
        if (countMask == 0)
        {
            countMask++;
        }
        
        SetAudioClipByName("guide_mask_name");
        audioSource.Play();
    }
    public void IncreaseAntibioticsCount()
    {
        if (countAntibiotics == 0)
        {
            countAntibiotics++;
        }
        
        SetAudioClipByName("guide_antibiotics_name");
        audioSource.Play();
    }
    public void IncreaseAlcoholCount()
    {
        if (countAlcohol == 0)
        {
            countAlcohol++;
        }
        
        SetAudioClipByName("guide_alcohol_name");
        audioSource.Play();
    }
    public void IncreaseAdhesiveTapeCount()
    {
        if (countAdhesiveTape == 0)
        {
            countAdhesiveTape++;
        }
        
        SetAudioClipByName("guide_adhesiveTape_name");
        audioSource.Play();
    }
    
    //Final de la escena
    private void BeforeEnding(string name)
    {
        if (countStillBottledWater >= 1 && countPortableRadio >= 1 && countPolyesterRopes >= 1 && countMultipurposeBlade >= 1
            && countFleeceBlanket >= 1 && countFlashlight >= 1 && countCannedFood >= 1 && countLighter >= 1
            && countHandTowel >= 1 && countAntibacterialGel >= 1 && countToothBrush >= 1)
        {
            firstAidKitCanvas.SetActive(true);
            emergencyBackpackCanvas.SetActive(false);
        }
        
        if (countSterileGauze >= 1 && countMask >= 1 && countAntibiotics >= 1 && countAlcohol >= 1
            && countAdhesiveTape >= 1)
        {
            //nextButton.SetActive(true);
            SceneManager.LoadScene(name);
        }
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
