using UnityEngine;

[AddComponentMenu("Liminal/Conditional Dialogue")]
public class ConditionalDialogue : MonoBehaviour
{
    [SerializeField] private GameFlagManager manager;
    [SerializeField] private GameFlagData requiredFlag;
    [SerializeField] private DialogueData dialogueIfTrue;
    [SerializeField] private DialogueData dialogueIfFalse;
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        if (manager == null)
            manager = FindAnyObjectByType<GameFlagManager>();
        if (dialogueManager == null)
            dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    public void Play()
    {
        if (manager == null || dialogueManager == null) return;

        bool flag = manager.GetFlag(requiredFlag);
        var dialogue = flag ? dialogueIfTrue : dialogueIfFalse;

        if (dialogue != null)
            dialogueManager.StartDialogue(dialogue);
    }
}
