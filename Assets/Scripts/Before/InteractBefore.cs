using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Before
{
    public class InteractBefore : MonoBehaviour
    {
        public GameObject nextButton;
        //EmergencyBackpack counter
        private short countStillBottledWater;
        private short countPortableRadio;
        private short countPolyesterRopes;
        private short countMultipurposeBlade;
        private short countFlashlight;
        private short countLighter;
        private short countHandTowel;
        private short countAntibacterialGel;
        private short countToothBrush;
    
        //FirstAidKit count
        private short countSterileGauze;
        private short countAntibiotics;
        private short countAlcohol;
        private short countAdhesiveTape;
        public AudioSource audioSource;
        public List<AudioClip> beforeNameList; 
        private Dictionary<string, AudioClip> beforeNameDictionary;
    
        //Imagenes del escaneo de mochila de emergencia
        public List<GameObject> emergencyBackpackModelsList;
        private Dictionary<string, GameObject> emergencyBackpackModelsDictionary;
        //Imagenes del escaneo de mochila de botquín
        public List<GameObject> firstAidKitModelsList;
        private Dictionary<string, GameObject> firstAidKitModelsDictionary;

        private void Start()
        {
            beforeNameDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in beforeNameList)
            {
                beforeNameDictionary[clip.name] = clip;
            }
        
            emergencyBackpackModelsDictionary = new Dictionary<string, GameObject>();
            foreach (var aux in emergencyBackpackModelsList)
            {
                emergencyBackpackModelsDictionary[aux.name] = aux;
            }
        
            firstAidKitModelsDictionary = new Dictionary<string, GameObject>();
            foreach (var aux in firstAidKitModelsList)
            {
                firstAidKitModelsDictionary[aux.name] = aux;
            }
        }

        private void Update()
        {
            BeforeEnding();
        }
    
        // ASIGNAR UN AUDIOSOURCE DE LA LISTA DE AUDIOCLIP
        private void SetAudioClipByName(string clipName)
        {
            if (beforeNameDictionary.TryGetValue(clipName, out var value))
            {
                audioSource.clip = value;
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
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
