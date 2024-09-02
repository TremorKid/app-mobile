using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.MainCharacter
{
	public class StartMovement : MonoBehaviour
	{
		private static readonly int Actions = Animator.StringToHash("Actions");
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
		private float currentTime;
		private short contAudioReproduce;
		private short contInteractive;
	
		//Contadores para saber cuantas veces se escaneo la imagen
		private short contColumn;
		private short contFirstAidKit;
		private short contEmergencyBackpack;
		private short contStair;
		private short contWindow;
		private short contTelevision;
		private short contBeam;
		private short contTable;
	
		//Condicional para saber si el audio termino
		private bool audioFinishFlag;
	
		//Contador de imagenes escaneadas y texto de imagenes escaneadas
		private short contScannedModels = 7;
		public TextMeshProUGUI scannedModelsText;
		public GameObject rawImageGameObject;
	
		//Contador final
		private short contEnd;
	
		//Lista de imagenes
		public List<GameObject> pictureList;

		private void Start()
		{
			//Sound
			audioSource = GetComponent<AudioSource>();
			InvokeRepeating(nameof(CheckAudioTime), 0f, 1f);

			guideLearningImagesDictionary = new Dictionary<string, AudioClip>();
			foreach (var clip in guideLearning)
			{
				guideLearningImagesDictionary[clip.name] = clip;
			}

			//Animation
			animator.SetLayerWeight(layerIndex1, 1f);
			animator.SetLayerWeight(layerIndex2, 2f);
		
			//Model
			modelDictionary = new Dictionary<string, GameObject>();
			foreach (var model in modelList.Where(model => model != null))
			{
				modelDictionary[model.name] = model;
			}
		
			//Botones de preguntas para repetir interaccion
			yesQuestionButtonDirectory = new Dictionary<string, GameObject>();
			foreach (var button in yesQuestionButtonList.Where(button => button != null))
			{
				yesQuestionButtonDirectory[button.name] = button;
			}
		}

		private void Update()
		{
			ControlAnimationPartameters();
			FinishLearningScene();
		}

		private void InitialMovementClipPlay()
		{
			audioSource.clip = birdSongSoundEffectClip;
			audioSource.Play();
		}

		//FUNCIÓN DE CONTROL DE ANIMACIONES Y AUDIO PARA LA REPRODUCCIÓN DE AUDIO Y ANIMACIONES EN LA ESCENA
		// ReSharper disable Unity.PerformanceAnalysis
		private void ControlAnimationPartameters()
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
					animator.SetInteger(Actions, 1);
					audioSource.Play();
				}
			}

			if (contAudioReproduce != 1) return;
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime == 0 && !audioSource.isPlaying)
			{
				//gameObject.SetActive(false);
				foreach (var aux in modelDictionary)
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

			if (audioSource.clip.name != "Guide_Beam" || currentTime != 0 || audioSource.isPlaying) return;
			audioFinishFlag = true;
			SetActiveModel(false, audioSource.clip.name);

			//if (audioSource.clip.name == "Guide_Meetin2gPoint" && currentTime == 0 && !audioSource.isPlaying)
			//{
			//	audioFinishFlag = true;
			//	SetActiveModel(false, audioSource.clip.name);
			//}

		}

		//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
		// ReSharper disable Unity.PerformanceAnalysis
		private void CheckAudioTime()
		{
			currentTime = audioSource.time;

			Debug.Log(currentTime);

			switch (currentTime)
			{
				case >= 19 and < 20 when contInteractive == 0:
					audioSource.Pause();
					buttons[0].gameObject.SetActive(true);
					contInteractive++;
					break;
				case >= 36 and < 37 when contInteractive == 1:
					pictureList[1].gameObject.SetActive(false);
					audioSource.Pause();
					buttons[1].gameObject.SetActive(true);
					contInteractive++;
					break;
			}

			//Pictures Guide_Start_Learning
		
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime is >= 22 and < 23)
			{
				pictureList[0].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime is >= 30 and < 31)
			{
				pictureList[0].gameObject.SetActive(false);
				pictureList[1].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime is >= 41 and < 42)
			{
				pictureList[2].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime is >= 45 and < 46)
			{
				pictureList[2].gameObject.SetActive(false);
				pictureList[3].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Guide_Start_Learning" && currentTime is >= 51 and < 52)
			{
				pictureList[3].gameObject.SetActive(false);
			}

			if (audioSource.clip.name != "Guide_Start_Learning" || currentTime is < 54 or >= 55 ||
			    contInteractive != 2) return;
			audioSource.Pause();
			pictureList[4].gameObject.SetActive(true);
			pictureList[5].gameObject.SetActive(true);
			pictureList[6].gameObject.SetActive(true);
			contInteractive++;

		}

		public void UnPauseAudioSource()
		{
			audioSource.UnPause();
		}

		// ASIGNAR UN AUDIOSOURCE DE LA LISTA DE AUDIOCLIP DEL GUIA
		private void SetAudioClipByName(string clipName)
		{
			if (guideLearningImagesDictionary.TryGetValue(clipName, out var value))
			{
				audioSource.clip = value;
				Debug.Log($"AudioClip '{clipName}' asignado al AudioSource.");
			}
			else
			{
				Debug.LogWarning($"AudioClip con el nombre '{clipName}' no encontrado.");
			}
		}

		// HABILITAR LA FUNCIÓN DE TODOS LOS MODELOS DE LAS IMAGENES PARA ESCANEAR
		private void SetActiveModel(bool flag, string nameModel)
		{
			foreach (var aux in modelDictionary)
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesColumnBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Columnas y Vigas</b>?";
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
				if (!audioFinishFlag) return;
				
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFirstAidKitBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Botiquín</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesEmergencyBackpackBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Mochila de Emergencia</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWindowBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Ventana</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTelevisionBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de <b>Televisión</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTableBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de la <b>Mesa</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesStairBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte de las <b>Escaleras</b>?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesBeamBtn"].SetActive(true);
				questionText.text = "Te gustaría repasar de nuevo la parte del <b>Punto de Reunión</b>?";
			}
		}
	
		//Acciones de los botones SI de las preguntas
		public void YesButtonColumn(string para, string para2)
		{
			audioFinishFlag = false;
			SetAudioClipByName("para");
			audioSource.Play();
			questionCanvas.SetActive(false);
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(false);
			}

			modelDictionary[para2].SetActive(true);
		}
	
		public void YesButtonFirstAidKit()
		{
			audioFinishFlag = false;
			SetAudioClipByName("Guide_FirstAidKit");
			audioSource.Play();
			questionCanvas.SetActive(false);
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(false);
			}

			modelDictionary["Beam"].SetActive(true);
		}

		//Finalización de la interacción y que pase al siguiente
		private void FinishLearningScene()
		{
			if (contScannedModels != 0 || audioSource.isPlaying || contEnd != 0) return;
			foreach (var aux in modelDictionary)
			{
				aux.Value.SetActive(false);
			}
			nextButton.SetActive(true);
			guideMeshObject.SetActive(true);
			scannedModelsText.gameObject.SetActive(false);
			rawImageGameObject.gameObject.SetActive(false);
			contEnd = 1;
		}

		public void LoadScene(string nameScene)
		{
			SceneManager.LoadScene(nameScene);
		}
	}
}
