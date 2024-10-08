using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Bag : MonoBehaviour
{
    public GameObject nextButton;
	
	//Variables para las animaciones del personaje
	public Animator animator; 
	public int layerIndex1;
	public int layerIndex2;
	
	//El modelado 3D del personaje
	public GameObject guideMeshObject;
	
	//Question Canvas
	public TextMeshProUGUI questionText;
	public List<GameObject> yesQuestionButtonList;
	private Dictionary<string, GameObject> yesQuestionButtonDirectory;
	public Button noQuestionButton;
	public GameObject questionCanvas;
	
	//Lista de botones para la interacción en InteractionCanvas
	public List<Button> buttons;
	
	//Lista de audios del guía
	public List<AudioClip> guideLearning;
	private Dictionary<string, AudioClip> guideLearningImagesDictionary;
	
	//Audio de efecto al inicio de la aplicación
	public AudioClip birdSongSoundEffectClip;
	private AudioSource audioSource;
	
	//Lista de las imagenes que se escanean
	public List<GameObject> modelList;
	private Dictionary<string, GameObject> modelDictionary;
	public List<GameObject> firstAidKitList;
	private Dictionary<string, GameObject> firstAidKitDictionary;
	
	//Variables para el control de audio
	private float currentTime = 0f;
	private short contAudioReproduce = 0;
	private short contInteractive = 0;
	
	//Contadores para saber cuantas veces se escaneo la imagen
	private short contChocolates = 0;
	private short contCannedFood = 0;
	private short contAntibacterialGel = 0;
	private short contFleeceBlanket = 0;
	private short contPlasticBags = 0;
	private short contHandTowel = 0;
	private short contPolyesterRopes = 0;
	private short contFlashlight = 0;
	private short contPortableRadio = 0;
	private short contToiletPaper = 0;
	private short contToothbrush = 0;
	private short contWater = 0;
	private short contWhistle = 0;
	private short contFirstAidKit = 0;

	private short contAntibiotics = 0;
	private short contBondages = 0;
	private short contCotton = 0;
	private short contMask = 0;
	private short contGloves = 0;
	private short contHydrogenPeroxide = 0;
	private short contTape = 0;
	
	//Contador final
	private short contEnd = 0;
	
	//Contador de imagenes escaneadas y texto de imagenes escaneadas
	private short contScannedModels = 1;
	public TextMeshProUGUI scannedModelsText;
	
	//Condicional para saber si el audio termino
	private bool audioFinishFlag;
	
	//Imagen de conteo
	public GameObject rawImageGameObject;
	
	// Referencia al componente RawImage
	public RawImage rawImage;

	// Nueva textura a asignar
	public Texture2D newTexture;
	
	//Videos
	public List<GameObject> videoList;
	private Dictionary<string, GameObject> videoDictionary;
	//Respuesta de la adivinanza es correcta
	private bool answer = false;
	
	//Siguiente Adivinanza
	private short contNext = 0;
	
	//Audio Adivinanza
	private string riddleAudio;
	
	//Lista de imagenes
	public List<GameObject> pictureList; 
	
	void Start()
	{
		//Sound
		audioSource = GetComponent<AudioSource>();
		InvokeRepeating("CheckAudioTime", 0f, 1f);

		guideLearningImagesDictionary = new Dictionary<string, AudioClip>();
		foreach (AudioClip clip in guideLearning)
		{
			guideLearningImagesDictionary[clip.name] = clip;
		}

		//Animation
		animator.SetLayerWeight(layerIndex1, 1f);
		animator.SetLayerWeight(layerIndex2, 2f);
		
		//Model
		modelDictionary = new Dictionary<string, GameObject>();
		
		foreach (GameObject model in modelList)
		{
			if (model != null)
			{
				modelDictionary[model.name] = model;
			}
		}
			
		firstAidKitDictionary = new Dictionary<string, GameObject>();
		
		foreach (GameObject firstAidKit in firstAidKitList)
		{
			if (firstAidKit != null)
			{
				firstAidKitDictionary[firstAidKit.name] = firstAidKit;
			}
		}
		
		//Video
		videoDictionary = new Dictionary<string, GameObject>();
		foreach (GameObject video in videoList)
		{
			if (video != null)
			{
				videoDictionary[video.name] = video;
			}
		}
		
		//Botones de preguntas para repetir interaccion
		yesQuestionButtonDirectory = new Dictionary<string, GameObject>();
		foreach (GameObject button in yesQuestionButtonList)
		{
			if (button != null)
			{
				yesQuestionButtonDirectory[button.name] = button;
			}
		}
	}

	void Update()
	{
		ControlAnimationPartameters();
		FinishLearningScene();
	}

	void InitialMovementClipPlay()
	{
		audioSource.clip = birdSongSoundEffectClip;
		audioSource.Play();
	}

	// NO TERMINADA: PARA POSICIONAR AL GUIA AL COSTADO DE LA IMAGEN ESCANEADA 
	void GuidePositionModel()
	{
		if (contChocolates == 1)
		{
			Debug.Log("Conteo de Chocolatesa: 1");
		}
	}

	//FUNCIÓN DE CONTROL DE ANIMACIONES Y AUDIO PARA LA REPRODUCCIÓN DE AUDIO Y ANIMACIONES EN LA ESCENA
	void ControlAnimationPartameters()
	{
		//Animation StartMovement Start
		//if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") && 
		//    animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		//{
//
//
		//}

		//Animation StartMovement End
		if (contAudioReproduce == 0)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartMovement") &&
			    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
			    !animator.IsInTransition(0))
			{
				//Audio
				audioSource.Pause();
				SetAudioClipByName("Scene 2.1 and 2.2");
				contAudioReproduce++;
				//Animation
				animator.SetInteger("Actions", 1);
				audioSource.Play();
			}
		}
		
		if (contAudioReproduce == 1)
		{
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				//gameObject.SetActive(false);
				foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
				{
					aux.Value.SetActive(true);
				}

				rawImageGameObject.gameObject.SetActive(true);
				scannedModelsText.gameObject.SetActive(true);
				guideMeshObject.SetActive(false);
			}

			if (scannedModelsText.gameObject.activeSelf)
			{
				scannedModelsText.text = contScannedModels.ToString();
			}
			
			//CUANDO ESCANEA LA IMAGEN DE LA COLUMMNA, Y TERMINA SU AUDIO, SE HABILITA TODOS LOS MODELOS
			//ESTO PARA SEGUIR ESCANEANDO UNO POR UNO, Y, ASÍ, NO TENER INTERFERENCIA SI DE CASUALIDAD SE ESCANEA 2 MODELOS
			if (audioSource.clip.name == "Scene 2.3.Chocolates" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["ChocolatesVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.CannedFood" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["CannedFoodVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.AntibacterialGel" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["AntibacterialGelVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.FleeceBlanket" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["FleeceBlanketVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.Flashlight" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["FlashlightVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.HandTowel" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["HandTowelVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.PlasticBags" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["PlasticBagsVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.PolyesterRopes" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["PolyesterRopesVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.PortableRadio" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["PortableRadioVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.ToiletPaper" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["ToiletPaperVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.Toothbrush" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["ToothbrushVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.Water" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["WaterVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 2.3.Whistle" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				videoDictionary["WhistleVideo"].SetActive(false);
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 2.3.FirstAidKit" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				foreach (KeyValuePair<string, GameObject> aux in firstAidKitDictionary)
				{
					aux.Value.SetActive(true);
				}

				rawImageGameObject.gameObject.SetActive(true);
				scannedModelsText.gameObject.SetActive(true);
				guideMeshObject.SetActive(false);
			}

			if (audioSource.clip.name == "Scene 3.2.Antibiotics.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Antibiotics.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Bondages.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Bondages.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Cotton.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Cotton.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Mask.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Mask.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Gloves.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Gloves.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.HydrogenPeroxide.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.HydrogenPeroxide.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Tape.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Tape.2" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Scene 3.2.Not" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveFirstAidKitModel();
				//SetActiveModel(false, audioSource.clip.name);
			}
		}
		
	}

	//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
	void CheckAudioTime()
	{
		currentTime = audioSource.time;

		Debug.Log(currentTime);
		
		//Audio Scene 2.1
		if (currentTime >= 9 && currentTime < 10 && contInteractive == 0)
		{
			audioSource.Pause();
			buttons[0].gameObject.SetActive(true);
			contInteractive++;
		}

		if (currentTime >= 27 && currentTime < 28 && contInteractive == 1)
		{
			pictureList[2].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (currentTime >= 45 && currentTime < 46 && contInteractive == 2)
		{
			pictureList[3].gameObject.SetActive(false);
			pictureList[4].gameObject.SetActive(false);
			pictureList[5].gameObject.SetActive(false);
			pictureList[6].gameObject.SetActive(false);
			pictureList[7].gameObject.SetActive(false);
			
			audioSource.Pause();
			buttons[2].gameObject.SetActive(true);
			contInteractive++;
		}
		
		//Pictures 2.1
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 12 && currentTime < 13)
		{
			pictureList[0].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 16 && currentTime < 17)
		{
			pictureList[0].gameObject.SetActive(false);
			pictureList[1].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 22 && currentTime < 23)
		{
			pictureList[1].gameObject.SetActive(false);
			pictureList[2].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 32 && currentTime < 33)
		{
			pictureList[3].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 33 && currentTime < 34)
		{
			pictureList[4].gameObject.SetActive(true);
			pictureList[5].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 34 && currentTime < 35)
		{
			pictureList[6].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 35 && currentTime < 36)
		{
			pictureList[7].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 53 && currentTime < 54)
		{
			pictureList[8].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 56 && currentTime < 57)
		{
			pictureList[9].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 61 && currentTime < 62)
		{
			pictureList[8].gameObject.SetActive(false);
			pictureList[9].gameObject.SetActive(false);
			pictureList[10].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.1 and 2.2" && currentTime >= 65 && currentTime < 66)
		{
			pictureList[10].gameObject.SetActive(false);
		}
		
		//Pictures 2.4
		
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 17 && currentTime < 18)
		{
			pictureList[11].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 22 && currentTime < 23)
		{
			pictureList[12].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 29 && currentTime < 30)
		{
			pictureList[11].gameObject.SetActive(false);
			pictureList[12].gameObject.SetActive(false);
		}
		
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 34 && currentTime < 35)
		{
			pictureList[13].gameObject.SetActive(true);
		}
		
		//Pictures 3.1
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 6 && currentTime < 7)
		{
			pictureList[14].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 10 && currentTime < 11)
		{
			pictureList[14].gameObject.SetActive(false);
			pictureList[15].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 10 && currentTime < 11)
		{
			pictureList[14].gameObject.SetActive(false);
			pictureList[15].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 14 && currentTime < 15)
		{
			pictureList[15].gameObject.SetActive(false);
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 18 && currentTime < 19)
		{
			pictureList[16].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 23 && currentTime < 24)
		{
			pictureList[16].gameObject.SetActive(false);
			pictureList[17].gameObject.SetActive(true);
		}
		
		//Audio Scene 2.4
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 4 && currentTime < 5 && contInteractive == 3)
		{
			audioSource.Pause();
			buttons[6].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 2.4" && currentTime >= 44 && currentTime < 45 && contInteractive == 4)
		{
			pictureList[13].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[3].gameObject.SetActive(true);
			contInteractive++;
		}
		
		//Audio Scene 3.1
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 25 && currentTime < 26 && contInteractive == 5)
		{
			audioSource.Pause();
			buttons[4].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.1" && currentTime >= 32 && currentTime < 33 && contInteractive == 6)
		{
			pictureList[17].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[2].gameObject.SetActive(true);
			contInteractive++;
		}
		
		//Audio Scene 3.2
		if (audioSource.clip.name == "Scene 3.2.Antibiotics.2" && currentTime >= 11 && currentTime < 12 && contInteractive == 7)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.2.Bondages.2" && currentTime >= 6 && currentTime < 7 && contInteractive == 8)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.2.Cotton.2" && currentTime >= 5 && currentTime < 6 && contInteractive == 9)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.2.Mask.2" && currentTime >= 6 && currentTime < 7 && contInteractive == 10)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.2.Gloves.2" && currentTime >= 7 && currentTime < 8 && contInteractive == 11)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (audioSource.clip.name == "Scene 3.2.HydrogenPeroxide.2" && currentTime >= 7 && currentTime < 8 && contInteractive == 12)
		{
			audioSource.Pause();
			buttons[5].gameObject.SetActive(true);
			contInteractive++;
		}
	}

	public void UnPauseAudioSource()
	{
		audioSource.UnPause();
	}

	// ASIGNAR UN AUDIOSOURCE DE LA LISTA DE AUDIOCLIP DEL GUIA
	public void SetAudioClipByName(string clipName)
	{
		if (guideLearningImagesDictionary.ContainsKey(clipName))
		{
			audioSource.clip = guideLearningImagesDictionary[clipName];
			Debug.Log($"AudioClip '{clipName}' asignado al AudioSource.");
		}
		else
		{
			Debug.LogWarning($"AudioClip con el nombre '{clipName}' no encontrado.");
		}
	}

	// HABILITAR LA FUNCIÓN DE TODOS LOS MODELOS DE LAS IMAGENES PARA ESCANEAR
	public void SetActiveModel(bool flag, string nameModel)
	{
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(true);
		}

		modelDictionary[nameModel].SetActive(flag);
	}
	
	public void SetActiveFirstAidKitModel()
	{
		foreach (KeyValuePair<string, GameObject> aux in firstAidKitDictionary)
		{ 
			aux.Value.SetActive(true);
		}
	}

	//CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN DEL BOTIQUIN ++++++++++++++++++++++++++++++++++++++++++++++++++
	//FUNCION CONDICIONAL SI SE ESCANEO Y NO ES LA CORRECTA
	public void Validation(string audio, string model)
	{
		if (answer == true)
		{
			//videoDictionary[model].SetActive(true);
			contScannedModels--;
			contNext++;
			SetAudioClipByName(audio);
			audioSource.Play();
		}
		else
		{
			SetAudioClipByName("Scene 3.2.Not");
			audioSource.Play();
		}
	}
	public void IncreasesAntibioticsImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contAntibiotics == 0 && riddleAudio == "Scene 3.2.Antibiotics.1")
			{
				contScannedModels--;
				contNext++;
				contAntibiotics++;
				SetAudioClipByName("Scene 3.2.Antibiotics.2");
				audioFinishFlag = false;
				
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}
		}
		
		//Validation("Scene 3.2.Antibiotics.2", "Antibiotics");
	}

	public void IncreasesBondagesImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contBondages == 0 && riddleAudio == "Scene 3.2.Bondages.1")
			{
				contScannedModels--;
				contNext++;
				contBondages++;
				SetAudioClipByName("Scene 3.2.Bondages.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	public void IncreasesCottonImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contCotton == 0 && riddleAudio == "Scene 3.2.Cotton.1")
			{
				contScannedModels--;
				contNext++;
				contCotton++;
				SetAudioClipByName("Scene 3.2.Cotton.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	public void IncreasesMaskImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contMask == 0 && riddleAudio == "Scene 3.2.Mask.1")
			{
				contScannedModels--;
				contNext++;
				contMask++;
				SetAudioClipByName("Scene 3.2.Mask.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	public void IncreasesGlovesImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contGloves == 0 && riddleAudio == "Scene 3.2.Gloves.1")
			{
				contScannedModels--;
				contNext++;
				contGloves++;
				SetAudioClipByName("Scene 3.2.Gloves.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	public void IncreasesHydrogenPeroxideImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contHydrogenPeroxide == 0 && riddleAudio == "Scene 3.2.HydrogenPeroxide.1")
			{
				contScannedModels--;
				contNext++;
				contHydrogenPeroxide++;
				SetAudioClipByName("Scene 3.2.HydrogenPeroxide.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	public void IncreasesTapeImageCounter()
	{
		if (audioFinishFlag == true)
		{
			if (contTape == 0 && riddleAudio == "Scene 3.2.Tape.1")
			{
				contScannedModels--;
				contNext++;
				contTape++;
				SetAudioClipByName("Scene 3.2.Tape.2");
				audioFinishFlag = false;
				audioSource.Play();
			}
			else
			{
				SetAudioClipByName("Scene 3.2.Not");
				audioFinishFlag = false;
				audioSource.Play();
			}	
		}
	}
	
	
	// CONTEO DE LAS VECES QUE SE HA ESCANEADO UNA IMAGEN
	public void IncreaseChocolatesImageCounter()
	{
		if (contChocolates == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.Chocolates");
			audioSource.Play();
			contChocolates++;
			contScannedModels--;
			videoDictionary["ChocolatesVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				
				yesQuestionButtonDirectory["YesChocolatesBtn"].SetActive(true);
				
				questionText.text = "Te gustaría repasar de nuevo la parte de los <b>Chocolates</b>?";
			}
		}
	}
	public void IncreaseCannedFoodImageCounter()
	{
		if (contCannedFood == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.CannedFood");
			audioSource.Play();
			contCannedFood++;
			contScannedModels--;
			videoDictionary["CannedFoodVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				
				yesQuestionButtonDirectory["YesCannedFoodBtn"].SetActive(true);
				
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Comida Enlatada</b>?";	
			}
		}
	}
	public void IncreaseAntibacterialGelImageCounter()
	{
		if (contAntibacterialGel == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.AntibacterialGel");
			audioSource.Play();
			contAntibacterialGel++;
			contScannedModels--;
			videoDictionary["AntibacterialGelVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesAntibacterialGelBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Gel Antibacterial</b>?";	
			}
		}
	}
	public void IncreasePlasticBagsImageCounter()
	{
		if (contPlasticBags == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.PlasticBags");
			audioSource.Play();
			contPlasticBags++;
			contScannedModels--;
			videoDictionary["PlasticBagsVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesPlasticBagsBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de las <b>Bolsas Plásticas</b>?";	
			}
		}
	}
	public void IncreaseHandTowelImageCounter()
	{
		if (contHandTowel == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.HandTowel");
			audioSource.Play();
			contHandTowel++;
			contScannedModels--;
			videoDictionary["HandTowelVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesHandTowelBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Toalla de mano</b>?";	
			}
		}
	}
	public void IncreaseFlashlightImageCounter()
	{
		if (contFlashlight == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.Flashlight");
			audioSource.Play();
			contFlashlight++;
			contScannedModels--;
			videoDictionary["FlashlightVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFlashlightBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Linterna</b>?";	
			}
		}
	}
	public void IncreaseFleeceBlanketImageCounter()
	{
		if (contFleeceBlanket == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.FleeceBlanket");
			audioSource.Play();
			contFleeceBlanket++;
			contScannedModels--;
			videoDictionary["FleeceBlanketVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFleeceBlanketBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Manta Polar</b>?";	
			}
		}
	}
	public void IncreasePolyesterRopesImageCounter()
	{
		if (contPolyesterRopes== 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.PolyesterRopes");
			audioSource.Play();
			contPolyesterRopes++;
			contScannedModels--;
			videoDictionary["PolyesterRopesVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesPolyesterRopesBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Cuerdas de Poliéster</b>?";	
			}
		}
	}
	
	public void IncreasePortableRadioImageCounter()
	{
		if (contPortableRadio == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.PortableRadio");
			audioSource.Play();
			contPortableRadio++;
			contScannedModels--;
			videoDictionary["PortableRadioVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesPortableRadioBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Radio Portátil</b>?";	
			}
		}
	}
	
	public void IncreaseToiletPaperImageCounter()
	{
		if (contToiletPaper == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.ToiletPaper");
			audioSource.Play();
			contToiletPaper++;
			contScannedModels--;
			videoDictionary["ToiletPaperVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesToiletPaperBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Papel Higiénico</b>?";	
			}
		}
	}
	
	public void IncreaseToothbrushImageCounter()
	{
		if (contToothbrush == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.Toothbrush");
			audioSource.Play();
			contToothbrush++;
			contScannedModels--;
			videoDictionary["ToothbrushVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesToothbrushBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Cepillo de Dientes</b>?";	
			}
		}
	}
	
	public void IncreaseWaterImageCounter()
	{
		if (contWater == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.Water");
			audioSource.Play();
			contWater++;
			contScannedModels--;
			videoDictionary["WaterVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWaterBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Agua</b>?";	
			}
		}
	}
	
	public void IncreaseWhistleImageCounter()
	{
		if (contWhistle == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.Whistle");
			audioSource.Play();
			contWhistle++;
			contScannedModels--;
			videoDictionary["WhistleVideo"].SetActive(true);
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWhistleBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Silbato</b>?";	
			}
		}
	}

	public void IncreaseFirstAidKitImageCounter()
	{
		if (contFirstAidKit == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 2.3.FirstAidKit");
			audioSource.Play();
			contFirstAidKit++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFirstAidKitBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Botiquín</b>?";	
			}
		}
	}
	
	//Acciones de los botones SI de las preguntas
	public void YesButtonChocolates()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.Chocolates");
		audioSource.Play();
		questionCanvas.SetActive(false);
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Chocolates"].SetActive(true);
		videoDictionary["ChocolatesVideo"].SetActive(true);
	}
	
	public void YesButtonCannedFood()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.CannedFood");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["CannedFood"].SetActive(true);
		videoDictionary["CannedFoodVideo"].SetActive(true);
	}
	
	public void YesButtonAntibacterialGel()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.AntibacterialGel");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["AntibacterialGel"].SetActive(true);
		videoDictionary["AntibacterialGelVideo"].SetActive(true);
	}
	
	public void YesButtonPlasticBags()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.PlasticBags");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["PlasticBags"].SetActive(true);
		videoDictionary["PlasticBagsVideo"].SetActive(true);
	}
	
	public void YesButtonHandTowel()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.HandTowel");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["HandTowel"].SetActive(true);
		videoDictionary["HandTowelVideo"].SetActive(true);
	}
	
	public void YesButtonFlashlight()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.Flashlight");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Flashlight"].SetActive(true);
		videoDictionary["FlashlightVideo"].SetActive(true);
	}
	
	public void YesButtonFleeceBlanket()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.FleeceBlanket");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["FleeceBlanket"].SetActive(true);
		videoDictionary["FleeceBlanketVideo"].SetActive(true);
	}
	
	public void YesButtonPolyesterRopes()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.PolyesterRopes");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["PolyesterRopes"].SetActive(true);
		videoDictionary["PolyesterRopesVideo"].SetActive(true);
	}
	
	public void YesButtonPortableRadio()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.PortableRadio");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["PortableRadio"].SetActive(true);
		videoDictionary["PortableRadioVideo"].SetActive(true);
	}
	
	public void YesButtonToiletPaper()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.ToiletPaper");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["ToiletPaper"].SetActive(true);
		videoDictionary["ToiletPaperVideo"].SetActive(true);
	}

	public void YesButtonToothbrush()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.Toothbrush");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Toothbrush"].SetActive(true);
		videoDictionary["ToothbrushVideo"].SetActive(true);
	}
	
	public void YesButtonWater()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.Water");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Water"].SetActive(true);
		videoDictionary["WaterVideo"].SetActive(true);
	}
	
	public void YesButtonWhistle()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.Whistle");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Whistle"].SetActive(true);
		videoDictionary["WhistleVideo"].SetActive(true);
	}

	public void YesButtonFirstAidKit()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 2.3.FirstAidKit");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["FirstAidKit"].SetActive(true);
	}

	//Finalización de la interacción y que pase al siguiente
	private void FinishLearningScene()
	{
		if (contScannedModels == 0 && !audioSource.isPlaying && contEnd == 0)
		{
			foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
			{
				aux.Value.SetActive(false);
			}
			
			SetAudioClipByName("Scene 2.4");
			audioSource.Play();
			guideMeshObject.SetActive(true);
			scannedModelsText.gameObject.SetActive(false);
			rawImageGameObject.gameObject.SetActive(false);
			contEnd = 1;
			contScannedModels = 7;

			//nextButton.SetActive(true);
		}

		if (!audioSource.isPlaying && contEnd == 1 && currentTime == 0)
		{
			rawImage.texture = newTexture;
			SetAudioClipByName("Scene 3.1");
			audioSource.Play();  
			contEnd = 2;
		}
		
		if (!audioSource.isPlaying && contEnd == 2 && contNext == 0 && currentTime == 0)
		{
			SetAudioClipByName("Scene 3.2.Antibiotics.1");
			riddleAudio = "Scene 3.2.Antibiotics.1";
			contEnd = 3;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 3 && contNext == 1 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
	        contCotton = 0;
	        contMask = 0;
	        contGloves = 0;
	        contHydrogenPeroxide = 0;
	        contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.Bondages.1");
			riddleAudio = "Scene 3.2.Bondages.1";
			contEnd = 4;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 4 && contNext == 2 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
			contCotton = 0;
			contMask = 0;
			contGloves = 0;
			contHydrogenPeroxide = 0;
			contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.Cotton.1");
			riddleAudio = "Scene 3.2.Cotton.1";
			contEnd = 5;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 5 && contNext == 3 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
			contCotton = 0;
			contMask = 0;
			contGloves = 0;
			contHydrogenPeroxide = 0;
			contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.Mask.1");
			riddleAudio = "Scene 3.2.Mask.1";
			contEnd = 6;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 6 && contNext == 4 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
			contCotton = 0;
			contMask = 0;
			contGloves = 0;
			contHydrogenPeroxide = 0;
			contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.Gloves.1");
			riddleAudio = "Scene 3.2.Gloves.1";
			contEnd = 7;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 7 && contNext == 5 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
			contCotton = 0;
			contMask = 0;
			contGloves = 0;
			contHydrogenPeroxide = 0;
			contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.HydrogenPeroxide.1");
			riddleAudio = "Scene 3.2.HydrogenPeroxide.1";
			contEnd = 8;
			audioFinishFlag = false;
			audioSource.Play();
		}
		
		if (!audioSource.isPlaying && contEnd == 8 && contNext == 6 && currentTime == 0)
		{
			contAntibiotics = 0;
			contBondages = 0;
			contCotton = 0;
			contMask = 0;
			contGloves = 0;
			contHydrogenPeroxide = 0;
			contTape = 0;
	        
			SetAudioClipByName("Scene 3.2.Tape.1");
			riddleAudio = "Scene 3.2.Tape.1";
			contEnd = 9;
			audioFinishFlag = false;
			audioSource.Play();
		}

		if (!audioSource.isPlaying && contEnd == 9 && contNext == 7 && contScannedModels == 0)
		{
			foreach (KeyValuePair<string, GameObject> aux in firstAidKitDictionary)
			{
				aux.Value.SetActive(false);
			}
			
			guideMeshObject.SetActive(true);
			scannedModelsText.gameObject.SetActive(false);
			rawImageGameObject.gameObject.SetActive(false);
			contEnd = 10; 
			SetAudioClipByName("Scene 3.3");
			audioFinishFlag = false;
			audioSource.Play();
			nextButton.SetActive(true);
		}
		
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
