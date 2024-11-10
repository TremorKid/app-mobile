using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Before
{
	public class GuideBefore : MonoBehaviour
	{
	    //Variables para las animaciones del personaje
		public Animator animator; 
		public int layerIndex1;
		public int layerIndex2;
		
		//El modelado 3D del personaje
		public GameObject guideMeshObject;
		
		//Lista de botones para la interacción en InteractionCanvas
		public List<Button> buttonInteraction;
		
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
		
		// Condicional para saber si el audio termino
		private bool audioFinishFlag;

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
			animator.SetLayerWeight(layerIndex1, 2f);
			
			//Model
			modelDictionary = new Dictionary<string, GameObject>();
			foreach (var model in modelList.Where(model => model != null))
			{
				modelDictionary[model.name] = model;
			}
		}

		private void Update()
		{
			ControlAnimationPartameters();
		}

		private void InitialMovementClipPlay()
		{
			audioSource.clip = birdSongSoundEffectClip;
			audioSource.Play();
		}

		//FUNCIÓN DE CONTROL DE ANIMACIONES Y AUDIO PARA LA REPRODUCCIÓN DE AUDIO Y ANIMACIONES EN LA ESCENA
		// ReSharper disable Unity.PerformanceAnalysis
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
				if (animator.GetCurrentAnimatorStateInfo(0).IsName("EmergencyBackpackMovement") &&
				    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
				    !animator.IsInTransition(0))
				{
					//Audio
					audioSource.Pause();
					SetAudioClipByName("guide_scene3_emergencyBackpack");
					contAudioReproduce++;
					//Animation
					animator.SetInteger("action_before", 1);
					audioSource.Play();
				}
			}

			if (contAudioReproduce != 1) return;
			//CUANDO EL AUDIO INICIAL DEL GUIA TERMINA, DA PASO A LA INTERACTIVIDAD DE LAS IMAGENES, POR ELLO SE ABELITIA CON SETACTIVE
			if (audioSource.clip.name != "guide_scene3_emergencyBackpack" || currentTime != 0 ||
			    audioSource.isPlaying) return;
			//gameObject.SetActive(false);
			foreach (var aux in modelDictionary)
			{
				aux.Value.SetActive(true);
			}

			guideMeshObject.SetActive(false);

			//CUANDO ESCANEA LA IMAGEN DE LA COLUMNA, Y TERMINA SU AUDIO, SE HABILITA TODOS LOS MODELOS
			//ESTO PARA SEGUIR ESCANEANDO UNO POR UNO, Y, ASÍ, NO TENER INTERFERENCIA SI DE CASUALIDAD SE ESCANEA 2 MODELOS

		}

		//FUNCIÓN PARA CONTROLAR EL TIEMPO DE CADA AUDIO Y COLOCAR LOS BOTONES AZULES DE INTERACTIVIDAD
		// ReSharper disable Unity.PerformanceAnalysis
		private void CheckAudioTime()
		{
			currentTime = audioSource.time;

			Debug.Log(currentTime);

			switch (currentTime)
			{
				case >= 7 and < 8 when contInteractive == 0:
					audioSource.Pause();
					buttonInteraction[0].gameObject.SetActive(true);
					contInteractive++;
					break;
				case >= 30 and < 31 when contInteractive == 1:
					audioSource.Pause();
					buttonInteraction[1].gameObject.SetActive(true);
					contInteractive++;
					break;
			}

			if (!(currentTime >= 55) || !(currentTime < 56) || contInteractive != 2) return;
			audioSource.Pause();
			buttonInteraction[2].gameObject.SetActive(true);
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
		public void SetActiveModel(bool flag, string nameModel)
		{
			foreach (var aux in modelDictionary)
			{ 
				aux.Value.SetActive(true);
			}

			modelDictionary[nameModel].SetActive(flag);
		}
		
		public void LoadScene(string nameScene)
		{
			SceneManager.LoadScene(nameScene);
		}
	}
}
