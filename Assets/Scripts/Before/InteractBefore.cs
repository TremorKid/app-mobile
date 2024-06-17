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
    
    //Imagenes del escaneo de mochila de emergencia
    public List<GameObject> emergencyBackpackModelsList;
    private Dictionary<string, GameObject> emergencyBackpackModelsDictionary;
    //Imagenes del escaneo de mochila de botquín
    public List<GameObject> firstAidKitModelsList;
    private Dictionary<string, GameObject> firstAidKitModelsDictionary;
    void Start()
    {
        beforeNameDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in beforeNameList)
        {
            beforeNameDictionary[clip.name] = clip;
        }
        
        emergencyBackpackModelsDictionary = new Dictionary<string, GameObject>();
        foreach (GameObject aux in emergencyBackpackModelsList)
        {
            emergencyBackpackModelsDictionary[aux.name] = aux;
        }
        
        firstAidKitModelsDictionary = new Dictionary<string, GameObject>();
        foreach (GameObject aux in firstAidKitModelsList)
        {
            firstAidKitModelsDictionary[aux.name] = aux;
        }
    }
    
    void Update()
    {
        BeforeEnding();
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
        countStillBottledWater++;
        
        
        SetAudioClipByName("guide_stillBottledWater_name");
        audioSource.Play();
    }
    public void IncreasePortableRadioCount()
    {
        countPortableRadio++;   
        
        
        SetAudioClipByName("guide_portableRadio_name");
        audioSource.Play();
    }
    public void IncreasePolyesterRopesCount()
    { 
        countPolyesterRopes++;
        
        
        SetAudioClipByName("guide_polyesterRopes_name");
        audioSource.Play();
    }
    public void IncreaseMultipurposeBladeCount()
    {
        countMultipurposeBlade++;
        
        
        SetAudioClipByName("guide_multipurposeBlade_name");
        audioSource.Play();
    }
    public void IncreaseFleeceBlanketCount()
    {
        countFleeceBlanket++;
        
        
        SetAudioClipByName("guide_fleeceBlanket_name");
        audioSource.Play();
    }
    public void IncreaseFlashlightCount()
    { 
        countFlashlight++;
        
        
        SetAudioClipByName("guide_flashlight_name");
        audioSource.Play();
    }
    public void IncreaseCannedFoodCount()
    {
        countCannedFood++;
        
        SetAudioClipByName("guide_cannedFood_name");
        audioSource.Play();
    }
    public void IncreaseLighterCount()
    {
        countLighter++;
        
        SetAudioClipByName("guide_lighter_name");
        audioSource.Play();
    }
    public void IncreaseHandTowelCount()
    {
        countHandTowel++;
        
        
        SetAudioClipByName("guide_handTowel_name");
        audioSource.Play();
    }
    public void IncreaseAntibacterialGelCount()
    {
        countAntibacterialGel++;

        
        SetAudioClipByName("guide_antibacterialGel_name");
        audioSource.Play();
    }
    public void IncreaseToothBrushCount()
    {
        countToothBrush++;
        
        SetAudioClipByName("guide_toothbrush_name");
        audioSource.Play();
    }
    
    // CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN DEL BOTIQUIN 
    public void IncreaseSterileGauzeCount()
    {

        countSterileGauze++;
        
        
        SetAudioClipByName("guide_sterileGauze_name");
        audioSource.Play();
    }
    public void IncreaseMaskCount()
    {
        countMask++;
        
        
        SetAudioClipByName("guide_mask_name");
        audioSource.Play();
    }
    public void IncreaseAntibioticsCount()
    {
        countAntibiotics++;
        
        
        SetAudioClipByName("guide_antibiotics_name");
        audioSource.Play();
    }
    public void IncreaseAlcoholCount()
    {
        countAlcohol++;
        
        
        SetAudioClipByName("guide_alcohol_name");
        audioSource.Play();
    }
    public void IncreaseAdhesiveTapeCount()
    {
        countAdhesiveTape++;
        
        
        SetAudioClipByName("guide_adhesiveTape_name");
        audioSource.Play();
    }
    
    //Final de la escena
    private void BeforeEnding()
    {
        if (countStillBottledWater >= 1 && countPortableRadio >= 1 && countPolyesterRopes >= 1 && countMultipurposeBlade >= 1
            && countFlashlight >= 1 && countLighter >= 1
            && countHandTowel >= 1 && countAntibacterialGel >= 1 && countToothBrush >= 1)
        {
            foreach (KeyValuePair<string, GameObject> aux in emergencyBackpackModelsDictionary)
            { 
                aux.Value.SetActive(false);
            }
            
            foreach (KeyValuePair<string, GameObject> aux in firstAidKitModelsDictionary)
            { 
                aux.Value.SetActive(true);
            }
        }
        
        if (countSterileGauze >= 1 && countAntibiotics >= 1 && countAlcohol >= 1
            && countAdhesiveTape >= 1)
        {
            nextButton.SetActive(true);
        }
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
