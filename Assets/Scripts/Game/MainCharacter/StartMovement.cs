using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartMovement : MonoBehaviour
{
	public Animator animator; // Referencia al Animator
	public int layerIndex1;
	public int layerIndex2;
	public GameObject guideMeshObject;
	public List<Button> buttons;
	public List<AudioClip> guideLearning;
	private Dictionary<string, AudioClip> guideLearningImagesDictionary;
	public AudioClip birdSongSoundEffectClip;
	private AudioSource audioSource;
	public List<GameObject> modelList;
	private Dictionary<string, GameObject> modelDictionary;
	private float currentTime = 0f;
	private short contAudioReproduce = 0;
	private short contInteractive = 0;
	private short contColumn = 0;
	private short contFirstAidKit = 0;
	private short contEmergencyBackpack = 0;
	private short contStair = 0;
	private short contWindow = 0;
	private short contTelevision = 0;
	private short contMeetingPoint = 0;
	private short contTable = 0;
	
	
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
	}

	void Update()
	{
		ControlAnimationPartameters();
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

				guideMeshObject.SetActive(false);
			}
			
			//CUANDO ESCANEA LA IMAGEN DE LA COLUMNA, Y TERMINA SU AUDIO, SE HABILITA TODOS LOS MODELOS
			//ESTO PARA SEGUIR ESCANEANDO UNO POR UNO, Y, ASÍ, NO TENER INTERFERENCIA SI DE CASUALIDAD SE ESCANEA 2 MODELOS
			if (audioSource.clip.name == "Guide_Column" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_FirstAidKit" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_EmergencyBackpack" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Stair" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Table" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Television" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Window" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_Beam" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
			
			if (audioSource.clip.name == "Guide_MeetingPoint" && currentTime == 0 && !audioSource.isPlaying)
			{
				SetActiveModel(false, audioSource.clip.name);
			}
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
			audioSource.Pause();
			buttons[1].gameObject.SetActive(true);
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
			SetAudioClipByName("Guide_BeamAndColumn");
			audioSource.Play();
			//transform.position = aux;
			contColumn++;
		}
	}
	public void IncreaseFirstAidKitImageCounter()
	{
		if (contFirstAidKit == 0)
		{
			SetAudioClipByName("Guide_FirstAidKit");
			audioSource.Play();
			contFirstAidKit++;
		}
	}
	public void IncreaseEmergencyBackpackImageCounter()
	{
		if (contEmergencyBackpack == 0)
		{
			SetAudioClipByName("Guide_EmergencyBackpack");
			audioSource.Play();
			contEmergencyBackpack++;
		}
	}
	public void IncreaseWindowImageCounter()
	{
		if (contWindow == 0)
		{
			SetAudioClipByName("Guide_Window");
			audioSource.Play();
			contWindow++;
		}
	}
	public void IncreaseTelevisionImageCounter()
	{
		if (contTelevision == 0)
		{
			SetAudioClipByName("Guide_Television");
			audioSource.Play();
			contTelevision++;
		}
	}
	public void IncreaseTableImageCounter()
	{
		if (contTable == 0)
		{
			SetAudioClipByName("Guide_Table");
			audioSource.Play();
			contTable++;
		}
	}
	public void IncreaseStairImageCounter()
	{
		if (contStair == 0)
		{
			SetAudioClipByName("Guide_Stair");
			audioSource.Play();
			contStair++;
		}
	}
	public void IncreaseMeetingPointImageCounter()
	{
		if (contMeetingPoint== 0)
		{
			SetAudioClipByName("Guide_MeetingPoint");
			audioSource.Play();
			contMeetingPoint++;
		}
	}
}
