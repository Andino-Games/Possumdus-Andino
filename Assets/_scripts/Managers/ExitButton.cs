using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
  public void OnClick()
    {
        SceneManager.LoadScene(0);
    }
}
