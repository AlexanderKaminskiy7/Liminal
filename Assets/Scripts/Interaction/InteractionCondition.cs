using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Liminal/Interaction Condition")]
public class InteractionCondition : MonoBehaviour
{
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private List<ObjectiveData> requiredObjectives = new List<ObjectiveData>();

    // Returns true if there are no requirements or all required objectives are completed
    public bool IsSatisfied()
    {
        if (requiredObjectives == null || requiredObjectives.Count == 0)
            return true;

        if (objectiveManager == null)
            return false;

        foreach (var o in requiredObjectives)
        {
            if (o == null) continue;
            if (!objectiveManager.IsCompleted(o))
                return false;
        }

        return true;
    }
}
