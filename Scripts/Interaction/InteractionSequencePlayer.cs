using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Liminal/Interaction Sequence Player")]
public class InteractionSequencePlayer : MonoBehaviour
{
    [SerializeField] private InteractionSequence sequence;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private UnityEvent onBlocked = new UnityEvent();

    // This method is intended to be called from Interactable.OnInteract
    public void Play()
    {
        // Check condition component on the same GameObject (optional)
        var condition = GetComponent<InteractionCondition>();
        if (condition != null && !condition.IsSatisfied())
        {
            onBlocked.Invoke();
            return;
        }

        if (sequence == null)
            return;

        foreach (var act in sequence.actions)
        {
            if (act == null) continue;

            switch (act.actionType)
            {
                case InteractionSequence.ActionType.StartDialogue:
                    if (act.dialogue != null && dialogueManager != null)
                        dialogueManager.StartDialogue(act.dialogue);
                    break;
                case InteractionSequence.ActionType.UnlockObjective:
                    if (act.objective != null && objectiveManager != null)
                        objectiveManager.UnlockObjective(act.objective);
                    break;
                case InteractionSequence.ActionType.CompleteObjective:
                    if (act.objective != null && objectiveManager != null)
                        objectiveManager.CompleteObjective(act.objective);
                    break;
            }
        }
    }
}
