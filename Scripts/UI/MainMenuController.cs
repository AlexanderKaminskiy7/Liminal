using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("Liminal/Main Menu Controller")]
public class MainMenuController : MonoBehaviour
{
    // Buttons should be connected manually via Button.OnClick in the Inspector.

    // Loads the intro scene. Intentionally uses the literal scene name as requested.
    public void PlayGame()
    {
        SceneManager.LoadScene("01_Intro");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings not implemented");
    }

    public void OpenCredits()
    {
        Debug.Log("Credits not implemented");
    }

    public void QuitGame()
    {
        Debug.Log("Quit requested");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
