using UnityEngine;
using UnityEngine.SceneManagement;
using _scripts.Managers;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public bool isActive;

    private void OnEnable()
    {
        isActive = false;
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
        CloseMenu();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    { 
        PausePanel.SetActive(true);
        CursorManager.instance.EnableInteractionMode();
        isActive = true;
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        PausePanel.SetActive(false);
        CursorManager.instance.DisableInteractionMode();
        isActive = false;
    }
}
