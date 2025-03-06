using _scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _scripts.UI
{
    public class ChangeScene : MonoBehaviour
    {
        public void Play(int sceneNumber)
        {
            AudioManager.instance.PlaySfx("Button");
            SceneManager.LoadScene(sceneNumber);
        }

    }
}