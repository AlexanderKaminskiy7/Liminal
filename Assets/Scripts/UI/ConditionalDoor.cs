using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

[AddComponentMenu("Liminal/Door")]
public class ConditionalDoor : MonoBehaviour
{
    [Tooltip("Имя сцены для загрузки. Должно быть в Build Settings.")]
    [SerializeField] private string targetScene;

    [Tooltip("Если false — дверь всегда заблокирована.")]
    [SerializeField] private bool canEnter = true;

    [Header("Условия (все должны выполняться)")]
    [SerializeField] private QuestData requiredQuestCompleted;
    [SerializeField] private GameFlagData requiredFlag;

    [Header("Если заблокировано")]
    [SerializeField] private DialogueData blockedDialogue;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private UnityEvent onBlocked = new UnityEvent();

    [Header("Fade (опционально)")]
    [SerializeField] private FadeController fadeController;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (dialogueManager == null)
            dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    public void Interact()
    {
        if (!canEnter)
        {
            Blocked();
            return;
        }

        if (requiredQuestCompleted != null)
        {
            var qm = QuestChainManager.Instance;
            if (qm != null && !qm.IsCompleted(requiredQuestCompleted))
            {
                Blocked();
                return;
            }
        }

        if (requiredFlag != null)
        {
            var fm = FindAnyObjectByType<GameFlagManager>();
            if (fm != null && !fm.GetFlag(requiredFlag))
            {
                Blocked();
                return;
            }
        }

        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogWarning($"{name}: targetScene пустой.");
            onBlocked.Invoke();
            return;
        }

        StartCoroutine(LoadSceneRoutine());
    }

    void Blocked()
    {
        onBlocked.Invoke();
        if (blockedDialogue != null && dialogueManager != null)
            dialogueManager.StartDialogue(blockedDialogue);
    }

    IEnumerator LoadSceneRoutine()
    {
        SaveManager.Save();

        if (fadeController != null)
        {
            fadeController.FadeOut(fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }
        SceneManager.LoadScene(targetScene);
    }
}
