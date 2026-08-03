using UnityEngine;

[AddComponentMenu("Liminal/Objective Trigger")]
public class ObjectiveTrigger : MonoBehaviour
{
    public enum TriggerAction { Unlock, Complete }

    [SerializeField] private ObjectiveManager manager;
    [SerializeField] private ObjectiveData objective;
    [SerializeField] private TriggerAction action = TriggerAction.Unlock;

    // Designer calls this method (e.g. from Interactable.OnInteract)
    public void Activate()
    {
        if (manager == null || objective == null)
            return;

        switch (action)
        {
            case TriggerAction.Unlock:
                manager.UnlockObjective(objective);
                break;
            case TriggerAction.Complete:
                manager.CompleteObjective(objective);
                break;
        }
    }
}
