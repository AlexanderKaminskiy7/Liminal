using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Liminal/Inspectable Object")]
public class InspectableObject : MonoBehaviour
{
    [SerializeField] private InspectData data;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Опционально: визуальный feedback")]
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    private void Awake()
    {
        if (dialogueManager == null)
            dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    /// <summary>
    /// Привяжи к Interactable.OnInteract в инспекторе.
    /// </summary>
    public void Inspect()
    {
        if (data == null) return;

        var qm = QuestChainManager.Instance;
        InspectData.Entry bestEntry = null;

        foreach (var e in data.entries)
        {
            if (e.requiredQuestActive != null && qm != null)
            {
                bool isActive = qm.CurrentQuest == e.requiredQuestActive;
                bool isCompleted = qm.IsCompleted(e.requiredQuestActive);

                if (e.requireQuestCompleted && !isCompleted) continue;
                if (!e.requireQuestCompleted && !isActive) continue;

                bestEntry = e;
                break;
            }
            else if (e.requiredQuestActive == null)
            {
                bestEntry = e;
                break;
            }
        }

        if (bestEntry == null) return;

        if (bestEntry.dialogue != null && dialogueManager != null)
        {
            dialogueManager.StartDialogue(bestEntry.dialogue);
        }
        else
        {
            var tempDialogue = ScriptableObject.CreateInstance<DialogueData>();
            var entry = new DialogueEntry
            {
                characterName = "Инга",
                text = bestEntry.text
            };
            tempDialogue.entries.Add(entry);
            dialogueManager.StartDialogue(tempDialogue);
        }
    }

    public void SetData(InspectData newData) => data = newData;
}
