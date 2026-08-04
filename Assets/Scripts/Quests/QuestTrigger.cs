using UnityEngine;

[AddComponentMenu("Liminal/Quest Trigger")]
public class QuestTrigger : MonoBehaviour
{
    public enum TriggerMode { CompleteCurrent, ActivateSpecific }

    [SerializeField] private TriggerMode mode = TriggerMode.CompleteCurrent;

    [Tooltip("Для CompleteCurrent: если указан — проверяет, что текущий квест именно этот")]
    [SerializeField] private QuestData specificQuest;

    [Tooltip("Если true — квест завершится только если он сейчас активен")]
    [SerializeField] private bool requireCurrentQuestMatch = true;

    [Header("Если условие не выполнено")]
    [SerializeField] private DialogueData blockedDialogue;
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        if (dialogueManager == null)
            dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    /// <summary>
    /// Привяжи этот метод к Interactable.OnInteract в инспекторе.
    /// </summary>
    public void TryTrigger()
    {
        var qm = QuestChainManager.Instance;
        if (qm == null) return;

        if (mode == TriggerMode.CompleteCurrent)
        {
            if (qm.CurrentQuest == null) return;

            if (requireCurrentQuestMatch && specificQuest != null && qm.CurrentQuest != specificQuest)
            {
                ShowBlocked();
                return;
            }

            qm.CompleteCurrentQuest();
        }
        else if (mode == TriggerMode.ActivateSpecific)
        {
            if (specificQuest != null)
                qm.ActivateQuest(specificQuest);
        }
    }

    void ShowBlocked()
    {
        if (dialogueManager != null && blockedDialogue != null)
            dialogueManager.StartDialogue(blockedDialogue);
    }
}
