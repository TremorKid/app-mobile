using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class During : MonoBehaviour
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
	private short contBeam = 0;
	private short contColumn = 0;
	private short contEmeregencyBackpack = 0;
	private short contFirstAidKit = 0;
	private short contTelevision = 0;
	private short contTable = 0;
	private short contWindow = 0;
	private short contStair = 0;
	
	//Contador final
	private short contEnd = 0;

	//Contador de imagenes escaneadas y texto de imagenes escaneadas
	private short contScannedModels = 3;
	public TextMeshProUGUI scannedModelsText;

	//Condicional para saber si el audio termino
	private bool audioFinishFlag;

	//Condicional para saber si el usuario quiere finalizar la actividad
	private bool audioEnd;
	
	//Imagenes que se mostrarán
	public List<GameObject> pictureList; 
	
	//Imagen de conteo
	public GameObject rawImageGameObject;

	void Start()
	{
		audioEnd = false;

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
	//void GuidePositionModel()
	//{
	//	if (contBeam == 1)
	//	{
	//		Debug.Log("Conteo de Beama: 1");
	//	}
	//}

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
				SetAudioClipByName("Scene 4.1");
				contAudioReproduce++;
				//Animation
				animator.SetInteger("Actions", 1);
				audioSource.Play();
			}
		}

		if (contAudioReproduce == 1)
		{
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name == "Scene 4.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				//gameObject.SetActive(false);
				foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
				{
					aux.Value.SetActive(true);
				}

				scannedModelsText.gameObject.SetActive(true);
				rawImageGameObject.gameObject.SetActive(true);
				guideMeshObject.SetActive(false);
			}

			if (scannedModelsText.gameObject.activeSelf)
			{
				scannedModelsText.text = contScannedModels.ToString();
			}

			//CUANDO ESCANEA LA IMAGEN DE LA COLUMMNA, Y TERMINA SU AUDIO, SE HABILITA TODOS LOS MODELOS
			//ESTO PARA SEGUIR ESCANEANDO UNO POR UNO, Y, ASÍ, NO TENER INTERFERENCIA SI DE CASUALIDAD SE ESCANEA 2 MODELOS
			if (audioSource.clip.name == "Scene 4.2.Beam" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.Column" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.EmeregencyBackpack" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.FirstAidKit" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.Stair" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.Table" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.Television" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}

			if (audioSource.clip.name == "Scene 4.2.Window" && currentTime == 0 && !audioSource.isPlaying)
			{
				audioFinishFlag = true;
				SetActiveModel(false, audioSource.clip.name);
			}
			
		}

	}

	//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
	void CheckAudioTime()
	{
		currentTime = audioSource.time;

		Debug.Log(currentTime);
		
		//Audio Scene 4.1
		
		if (currentTime >= 6 && currentTime < 7 && contInteractive == 0)
		{
			pictureList[3].gameObject.SetActive(true);
		}
		
		if (currentTime >= 15 && currentTime < 16 && contInteractive == 0)
		{
			audioSource.Pause();
			buttons[0].gameObject.SetActive(true);
			contInteractive++;
		}
		
		//Audio Scene 4.3
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 14 && currentTime < 15)
		{
			pictureList[0].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 24 && currentTime < 25)
		{
			pictureList[0].gameObject.SetActive(false);
		}
		
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 25 && currentTime < 26)
		{
			pictureList[1].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 37 && currentTime < 38)
		{
			pictureList[1].gameObject.SetActive(false);
		}
		
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 38 && currentTime < 39)
		{
			pictureList[2].gameObject.SetActive(true);
		}
		
		if (audioSource.clip.name == "Scene 4.3" && currentTime >= 52 && currentTime < 53 && contInteractive == 1)
		{
			pictureList[2].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
			buttons[2].gameObject.SetActive(true);
			contInteractive = 2;
		}

	}

	public void UnPauseAudioSource()
	{
		audioSource.UnPause();
	}

	public void EndActivity()
	{
		contEnd = 2;
	}

	public void Again()
	{
		contEnd = 0;
		contInteractive = 1;
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
	public void IncreaseBeamImageCounter()
	{
		if (contBeam == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Beam");
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
				
				questionText.text = "Volver a escuchar";
			}
		}
	}
	public void IncreaseColumnImageCounter()
	{
		if (contColumn == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Column");
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
				
				questionText.text = "¿Te gustaría repetir la parte de las columnas?";	
			}
		}
	}
	public void IncreaseEmeregencyBackpackImageCounter()
	{
		if (contEmeregencyBackpack == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.EmeregencyBackpack");
			audioSource.Play();
			contEmeregencyBackpack++;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesEmeregencyBackpackBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de la mochila de emergencia?";	
			}
		}
	}
	public void IncreaseTelevisionImageCounter()
	{
		if (contTelevision == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Television");
			audioSource.Play();
			contTelevision++;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTelevisionBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de los televisores?";	
			}
		}
	}
	public void IncreaseTableImageCounter()
	{
		if (contTable == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Table");
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
				questionText.text = "¿Te gustaría repetir la parte de las mesas?";	
			}
		}
	}
	public void IncreaseStairImageCounter()
	{
		if (contStair == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Stair");
			audioSource.Play();
			contStair++;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesStairBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de las escaleras?";	
			}
		}
	}
	public void IncreaseFirstAidKitImageCounter()
	{
		if (contFirstAidKit == 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.FirstAidKit");
			audioSource.Play();
			contFirstAidKit++;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFirstAidKitBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte del botiquín?";	
			}
		}
	}
	public void IncreaseWindowImageCounter()
	{
		if (contWindow== 0)
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Window");
			audioSource.Play();
			contWindow++;
		}
		else
		{
			if (audioFinishFlag == true)
			{
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWindowBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de las ventanas?";	
			}
		}
	}
	
	//Acciones de los botones SI de las preguntas
	public void YesButtonBeam()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.Beam");
		audioSource.Play();
		questionCanvas.SetActive(false);
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Beam"].SetActive(true);
	}
	
	public void YesButtonColumn()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.Column");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Column"].SetActive(true);
	}
	
	public void YesButtonEmeregencyBackpack()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.EmeregencyBackpack");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["EmeregencyBackpack"].SetActive(true);
	}
	
	public void YesButtonTelevision()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.Television");
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
		SetAudioClipByName("Scene 4.2.Table");
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
		SetAudioClipByName("Scene 4.2.Stair");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Stair"].SetActive(true);
	}
	
	public void YesButtonFirstAidKit()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.FirstAidKit");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["FirstAidKit"].SetActive(true);
	}
	
	public void YesButtonWindow()
	{
		audioFinishFlag = false;
		SetAudioClipByName("Scene 4.2.Window");
		audioSource.Play();
		questionCanvas.SetActive(false);
		
		foreach (KeyValuePair<string, GameObject> aux in modelDictionary)
		{ 
			aux.Value.SetActive(false);
		}

		modelDictionary["Window"].SetActive(true);
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
			
			SetAudioClipByName("Scene 4.3");
			audioSource.Play();
			guideMeshObject.SetActive(true);
			contEnd = 1;
			scannedModelsText.gameObject.SetActive(false);
			rawImageGameObject.gameObject.SetActive(false);

			//nextButton.SetActive(true);
		}

		if (!audioSource.isPlaying && contEnd == 2 && audioEnd == false)
		{
			SetAudioClipByName("Scene 4.4");
			audioEnd = true;
			audioSource.Play();
		}
		
		if (audioSource.clip.name == "Scene 4.4" && currentTime == 0 && !audioSource.isPlaying && audioEnd == true)
		{
			guideMeshObject.SetActive(false);
			//LoadScene();
		}
		
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
