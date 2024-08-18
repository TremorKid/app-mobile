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
	
	//Contador final
	private short contEnd = 0;
	
	//Contador de imagenes escaneadas y texto de imagenes escaneadas
	private short contScannedModels = 13;
	public TextMeshProUGUI scannedModelsText;
	
	//Condicional para saber si el audio termino
	private bool audioFinishFlag;
	
	//Imagen de conteo
	public GameObject rawImageGameObject;
	
	//Videos
	public List<GameObject> videoList;
	private Dictionary<string, GameObject> videoDictionary;
	
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
		}
		
	}

	//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
	void CheckAudioTime()
	{
		currentTime = audioSource.time;

		Debug.Log(currentTime);

		if (currentTime >= 9 && currentTime < 10 && contInteractive == 0)
		{
			audioSource.Pause();
			buttons[0].gameObject.SetActive(true);
			contInteractive++;
		}

		if (currentTime >= 27 && currentTime < 28 && contInteractive == 1)
		{
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
			contInteractive++;
		}
		
		if (currentTime >= 45 && currentTime < 46 && contInteractive == 2)
		{
			audioSource.Pause();
			buttons[2].gameObject.SetActive(true);
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
			
			//nextButton.SetActive(true);
		}
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
