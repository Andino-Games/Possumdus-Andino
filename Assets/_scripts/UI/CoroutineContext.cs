using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _scripts.UI
{
    public class CoroutineContext : MonoBehaviour
    {
        public AudioSource audioSource; // Asigna el AudioSource en el Inspector
        public string sceneName; // Nombre de la escena a la que quieres cambiar

        void Start()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if (audioSource != null)
            {
                StartCoroutine(WaitForAudioAndChangeScene());
            }
            else
            {
                Debug.LogError("No se encontró un AudioSource en el objeto.");
            }
        }

        IEnumerator WaitForAudioAndChangeScene()
        {
            audioSource.Play(); // Reproduce el audio
            yield return new WaitUntil(() => !audioSource.isPlaying);
            SceneManager.LoadScene(sceneName); // Cambia de escena
        }
    }
    }
