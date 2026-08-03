using UnityEngine;

[AddComponentMenu("Liminal/Conditional Dialogue")]
public class ConditionalDialogue : MonoBehaviour
{
    [SerializeField] private GameFlagManager manager;
    [SerializeField] private GameFlagData flag;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData firstDialogue;
    [SerializeField] private DialogueData repeatedDialogue;

    // Designer calls this method (e.g. from Interactable.OnInteract)
    private void Awake()
    {
        if (manager == null)
            manager = FindFirstObjectByType<GameFlagManager>();
    }
    public void Play()
    {
        if (dialogueManager == null) return;

        bool has = false;
        if (manager != null && flag != null)
            has = manager.GetFlag(flag);

        if (!has)
        {
            if (firstDialogue != null)
                dialogueManager.StartDialogue(firstDialogue);
        }
        else
        {
            if (repeatedDialogue != null)
                dialogueManager.StartDialogue(repeatedDialogue);
        }
    }
}
