using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMovement : MonoBehaviour
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
	private short contColumn = 0;
	private short contFirstAidKit = 0;
	private short contEmergencyBackpack = 0;
	private short contStair = 0;
	private short contWindow = 0;
	private short contTelevision = 0;
	private short contBeam = 0;
	private short contTable = 0;
	
	//Condicional para saber si el audio termino
	private bool audioFinishFlag;
	
	//Contador de imagenes escaneadas y texto de imagenes escaneadas
	private short contScannedModels = 13;
	public TextMeshProUGUI scannedModelsText;
	public GameObject rawImageGameObject;
	
	//Contador final
	private short contEnd = 0;
	
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
		if (contColumn == 1)
		{
			Debug.Log("Conteo de columna: 1");
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
				SetAudioClipByName("Guide_Start_Learning");
				contAudioReproduce++;
				//Animation
				animator.SetInteger("Actions", 1);
				audioSource.Play();
			}
		}

		if (contAudioReproduce == 1)
		{
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime == 0 && !audioSource.isPlaying)
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
			
			//CUANDO ESCANEA LA IMAGEN DE LA COLUMNA, Y TERMINA SU AUDIO, SE HABILITA TODOS LOS MODELOS
			//ESTO PARA SEGUIR ESCANEANDO UNO POR UNO, Y, ASÍ, NO TENER INTERFERENCIA SI DE CASUALIDAD SE ESCANEA 2 MODELOS
			if (audioSource.clip.name == "Guide_Column" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_FirstAidKit" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_EmergencyBackpack" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Stair" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Table" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Television" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Window" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Beam" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
			//if (audioSource.clip.name == "Guide_Meetin2gPoint" && currentTime == 0 && !audioSource.isPlaying)
			//{
			//	audioFinishFlag = true;
			//	SetActiveModel(false, audioSource.clip.name);
			//}
		}
		
	}

	//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
	void CheckAudioTime()
	{
		currentTime = audioSource.time;

		Debug.Log(currentTime);

		if (currentTime >= 19 && currentTime < 20 && contInteractive == 0)
		{
			audioSource.Pause();
			buttons[0].gameObject.SetActive(true);
			contInteractive++;
		}

		if (currentTime >= 36 && currentTime < 37 && contInteractive == 1)
		{
			pictureList[1].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
			contInteractive++;
		}
		
		//Pictures Guide_Start_Learning
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 22 && currentTime < 23)
		{
			pictureList[0].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 30 && currentTime < 31)
		{
			pictureList[0].gameObject.SetActive(false);
			pictureList[1].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 41 && currentTime < 42)
		{
			pictureList[2].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 45 && currentTime < 46)
		{
			pictureList[2].gameObject.SetActive(false);
			pictureList[3].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 51 && currentTime < 52)
		{
			pictureList[3].gameObject.SetActive(false);
		}
		
		if (audioSource.clip.name == "Guide_Start_Learning" && currentTime >= 54 && currentTime < 55 && contInteractive == 2)
		{
			audioSource.Pause();
			pictureList[4].gameObject.SetActive(true);
			pictureList[5].gameObject.SetActive(true);
			pictureList[6].gameObject.SetActive(true);
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
	public void IncreaseColumnImageCounter()
	{
		if (contColumn == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Column");
			audioSource.Play();
			contColumn++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				
				yesQuestionButtonDirectory["YesColumnBtn"].SetActive(true);
				
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Columnas y Vigas</b>?";
			}
		}
	}
	public void IncreaseFirstAidKitImageCounter()
	{
		if (contFirstAidKit == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_FirstAidKit");
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
				
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Botiquín</b>?";	
			}
		}
	}
	public void IncreaseEmergencyBackpackImageCounter()
	{
		if (contEmergencyBackpack == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_EmergencyBackpack");
			audioSource.Play();
			contEmergencyBackpack++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesEmergencyBackpackBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Mochila de Emergencia</b>?";	
			}
		}
	}
	public void IncreaseWindowImageCounter()
	{
		if (contWindow == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Window");
			audioSource.Play();
			contWindow++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWindowBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Ventana</b>?";	
			}
		}
	}
	public void IncreaseTelevisionImageCounter()
	{
		if (contTelevision == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Television");
			audioSource.Play();
			contTelevision++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTelevisionBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Televisión</b>?";	
			}
		}
	}
	public void IncreaseTableImageCounter()
	{
		if (contTable == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Table");
			audioSource.Play();
			contTable++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTableBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Mesa</b>?";	
			}
		}
	}
	public void IncreaseStairImageCounter()
	{
		if (contStair == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Stair");
			audioSource.Play();
			contStair++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesStairBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de las <b>Escaleras</b>?";	
			}
		}
	}
	public void IncreaseBeamImageCounter()
	{
		if (contBeam== 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_Beam");
			audioSource.Play();
			contBeam++;
			contScannedModels--;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesBeamBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Punto de Reunión</b>?";	
			}
		}
	}
	
	//Acciones de los botones SI de las preguntas
	public void YesButtonColumn()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Column");
		audioSource.Play();
		questionCanvas.SetActive(false);
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Column"].SetActive(true);
	}
	
	public void YesButtonFirstAidKit()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_FirstAidKit");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["FirstAidKit"].SetActive(true);
	}
	
	public void YesButtonEmergencyBackpack()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_EmergencyBackpack");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["EmergencyBackpack"].SetActive(true);
	}
	
	public void YesButtonWindow()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Window");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Window"].SetActive(true);
	}
	
	public void YesButtonTelevision()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Television");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Television"].SetActive(true);
	}
	
	public void YesButtonTable()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Table");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Table"].SetActive(true);
	}
	
	public void YesButtonStair()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Stair");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Stair"].SetActive(true);
	}
	
	public void YesButtonBeam()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Guide_Beam");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Beam"].SetActive(true);
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
			nextButton.SetActive(true);
			guideMeshObject.SetActive(true);
			scannedModelsText.gameObject.SetActive(false);
			rawImageGameObject.gameObject.SetActive(false);
			contEnd = 1;
		}
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
