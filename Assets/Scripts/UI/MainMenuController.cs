using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Liminal/Main Menu Controller")]
public class MainMenuController : MonoBehaviour
{
    [Header("Кнопки подключаются через Inspector (Button.OnClick)")]

    [Tooltip("Сцена, с которой начинается новая игра")]
    [SerializeField] private string newGameScene = "01_Intro";

    [Tooltip("Если есть сохранение — загрузить эту сцену")]
    public void ContinueGame()
    {
        if (SaveManager.HasSave())
        {
            SaveManager.Load();
            string scene = SaveManager.GetSavedSceneName();
            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.LoadScene(scene);
                return;
            }
        }
        Debug.Log("Нет сохранения для продолжения.");
    }

    public void NewGame()
    {
        SaveManager.DeleteSave();
        SceneManager.LoadScene(newGameScene);
    }

    public void OpenSettings()
    {
        Debug.Log("Настройки: пока заглушка.");
    }

    public void OpenCredits()
    {
        Debug.Log("Credits: пока заглушка.");
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
