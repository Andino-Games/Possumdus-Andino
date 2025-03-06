using _scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject panelCredits;
        public GameObject panelMenu;
        public GameObject panelControls;
        private void Start()
        {
            panelMenu.SetActive(true);
            panelCredits.SetActive(false);
            panelControls.SetActive(false);
        }
        public void Credits()
        {
            AudioManager.instance.PlaySfx("Button");
            panelCredits.SetActive(true);
            panelMenu.SetActive(false);
            panelControls.SetActive(false);
        }
        
        public void Controls()
        {
            AudioManager.instance.PlaySfx("Button");
            panelControls.SetActive(true);
            panelCredits.SetActive(false);
            panelMenu.SetActive(false);
        }

        public void GoBack()
        {
            AudioManager.instance.PlaySfx("Button");
            panelMenu.SetActive(true);
            panelCredits.SetActive(false);
            panelControls.SetActive(false);
        }

        public void Quit()
        {
            AudioManager.instance.PlaySfx("Button");
            Application.Quit();
        }

    }
}
