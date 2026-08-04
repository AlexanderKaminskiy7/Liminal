using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Liminal/Intro Controller")]
public class IntroController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private DialogueData introDialogue;
    [SerializeField] private string nextSceneName = "";
    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        if (fadeController != null)
            fadeController.FadeIn(fadeDuration);

        if (dialogueManager != null && introDialogue != null)
            dialogueManager.StartDialogue(introDialogue);
    }

    public void FinishIntro()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("IntroController: nextSceneName пустой.");
            return;
        }

        SaveManager.Save();

        if (fadeController != null)
            fadeController.FadeOut(fadeDuration);

        StartCoroutine(LoadAfterDelay(fadeDuration));
    }

    private IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
