using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quiz;
using TMPro;

namespace During
{
	public class During : MonoBehaviour
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
		private short contBeam = 0;
		private short contColumn = 0;
		private short contEmergencyBackpack = 0;
		private short contFirstAidKit = 0;
		private short contTelevision = 0;
		private short contTable = 0;
		private short contWindow = 0;
		private short contStair = 0;
	
		//Contador final
		private short contEnd;

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
        
        public Button audioButton;
        public AudioClip activitySound;
        public AudioClip guideSound;
        bool audioBackgroundActive = true;
        private short audioCont = 0;
        public AudioSource backgroundAudio;
        
        public TextMeshProUGUI activityText;

		public void Start()
		{
            backgroundAudio.clip = guideSound;
            backgroundAudio.Play();
                    
			audioEnd = false;

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
            if (audioSource.isPlaying)
            {
                // Baja el volumen del audio de fondo
                backgroundAudio.volume = 0.02f;
            }
            else
            {
                // Restaura el volumen del audio de fondo
                backgroundAudio.volume = 0.3f;
            }
                    
			ControlAnimationPartameters();
			FinishLearningScene();
		}

		private void InitialMovementClipPlay()
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
					SetAudioClipByName("Scene 4.1");
					contAudioReproduce++;
					//Animation
					animator.SetInteger(Actions, 1);
					audioSource.Play();
				}
			}

			if (contAudioReproduce != 1) return;
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name == "Scene 4.1" && currentTime == 0 && !audioSource.isPlaying)
			{
				//gameObject.SetActive(false);
				foreach (var aux in modelDictionary)
				{
					aux.Value.SetActive(true);
				}

				scannedModelsText.gameObject.SetActive(true);
				rawImageGameObject.gameObject.SetActive(true);
				guideMeshObject.SetActive(false);
                if (audioCont == 0)
                {
                    backgroundAudio.clip = activitySound;
                    backgroundAudio.Play();
                    audioCont++;
                }
                
                activityText.gameObject.SetActive(true);
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

			if (audioSource.clip.name == "Scene 4.2.EmergencyBackpack" && currentTime == 0 && !audioSource.isPlaying)
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

			if (audioSource.clip.name != "Scene 4.2.Window" || currentTime != 0 || audioSource.isPlaying) return;
			audioFinishFlag = true;
			SetActiveModel(false, audioSource.clip.name);

		}

		//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
		private void CheckAudioTime()
		{
			currentTime = audioSource.time;

			Debug.Log(currentTime);
		
			//Audio Scene 4.1
		
			switch (currentTime)
			{
				case >= 6 and < 7 when contInteractive == 0:
					pictureList[3].gameObject.SetActive(true);
					break;
				case >= 15 and < 16 when contInteractive == 0:
					audioSource.Pause();
					buttons[0].gameObject.SetActive(true);
					contInteractive++;
					break;
			}

			// Audio Scene 4.3
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 4 and < 5 && contInteractive == 1)
			{
				audioSource.Pause();
				buttons[3].gameObject.SetActive(true);
				contInteractive++;
			}
		
			// Picture Audio Scene 4.3
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 21 and < 22)
			{
				pictureList[0].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 28 and < 29)
			{
				pictureList[0].gameObject.SetActive(false);
			}
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 31 and < 32)
			{
				pictureList[1].gameObject.SetActive(true);
			}
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 43 and < 44)
			{
				pictureList[1].gameObject.SetActive(false);
			}
		
			if (audioSource.clip.name == "Scene 4.3" && currentTime is >= 45 and < 46)
			{
				pictureList[2].gameObject.SetActive(true);
			}

			if (audioSource.clip.name != "Scene 4.3" || currentTime is < 59 or >= 60 || contInteractive != 2) return;
			pictureList[2].gameObject.SetActive(false);
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
			buttons[2].gameObject.SetActive(true);
			contInteractive = 3;

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
		// ReSharper disable Unity.PerformanceAnalysis
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
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				
				yesQuestionButtonDirectory["YesBeamBtn"].SetActive(true);
				
				questionText.text = "Volver a escuchar";
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
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				
				yesQuestionButtonDirectory["YesColumnBtn"].SetActive(true);
				
				questionText.text = "¿Te gustaría repetir la parte de las columnas?";
			}
		}
		public void IncreaseEmergencyBackpackImageCounter()
		{
			if (contEmergencyBackpack == 0)
			{
				audioFinishFlag = false;
				SetAudioClipByName("Scene 4.2.EmergencyBackpack");
				audioSource.Play();
				contEmergencyBackpack++;
			}
			else
			{
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesEmergencyBackpackBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de la mochila de emergencia?";
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
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTelevisionBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de los televisores?";
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
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesTableBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de las mesas?";
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
				if (audioFinishFlag != true) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesStairBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de las escaleras?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesFirstAidKitBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte del botiquín?";
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
				if (!audioFinishFlag) return;
				questionCanvas.SetActive(true);
				yesQuestionButtonDirectory["YesWindowBtn"].SetActive(true);
				questionText.text = "¿Te gustaría repetir la parte de las ventanas?";
			}
		}
	
		//Acciones de los botones SI de las preguntas
		public void YesButtonBeam()
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Beam");
			audioSource.Play();
			questionCanvas.SetActive(false);
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(false);
			}

			modelDictionary["Column"].SetActive(true);
		}
	
		public void YesButtonEmergencyBackpack()
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.EmergencyBackpack");
			audioSource.Play();
			questionCanvas.SetActive(false);
		
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(false);
			}

			modelDictionary["EmergencyBackpack"].SetActive(true);
		}
	
		public void YesButtonTelevision()
		{
			audioFinishFlag = false;
			SetAudioClipByName("Scene 4.2.Television");
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
			SetAudioClipByName("Scene 4.2.Table");
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
			SetAudioClipByName("Scene 4.2.Stair");
			audioSource.Play();
			questionCanvas.SetActive(false);
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
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
		
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(false);
			}

			modelDictionary["Window"].SetActive(true);
		}
	
		//Finalización de la interacción y que pase al siguiente
		// ReSharper disable Unity.PerformanceAnalysis
		private void FinishLearningScene()
		{
			if (contScannedModels == 0 && !audioSource.isPlaying && contEnd == 0)
			{
				foreach (var aux in modelDictionary)
				{
					aux.Value.SetActive(false);
				}
			
				SetAudioClipByName("Scene 4.3");
				audioSource.Play();
				guideMeshObject.SetActive(true);
				contEnd = 1;
				scannedModelsText.gameObject.SetActive(false);
				rawImageGameObject.gameObject.SetActive(false);
                backgroundAudio.clip = guideSound;
                backgroundAudio.Play();
                activityText.gameObject.SetActive(false);
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
				LoadScene("Quiz");
			}

		}

		public void LoadScene(string nameScene)
		{
            QuizNavigation.isInitialQuiz = false;
			SceneManager.LoadScene(nameScene);
		}
        
        public void AudioBackground()
        {
            audioBackgroundActive = !audioBackgroundActive;
            if (audioBackgroundActive == false)
            {
                audioButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Mute");
                backgroundAudio.Pause();
            }
            else
            {
                audioButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Audio");
                backgroundAudio.Play();
            }          
        }
	}
}
