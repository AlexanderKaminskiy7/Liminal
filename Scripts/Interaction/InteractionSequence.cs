using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionSequence", menuName = "Liminal/Interaction Sequence")]
public class InteractionSequence : ScriptableObject
{
    public enum ActionType { StartDialogue, UnlockObjective, CompleteObjective }

    [System.Serializable]
    public class Action
    {
        public ActionType actionType;
        public DialogueData dialogue;
        public ObjectiveData objective;
    }

    public List<Action> actions = new List<Action>();
}
