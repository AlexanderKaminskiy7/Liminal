using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "Liminal/Objective")]
public class ObjectiveData : ScriptableObject
{
    [Tooltip("Unique ID for the objective")]
    public string id;

    [Tooltip("Short name shown in the objective list")]
    public string displayName;

    [TextArea(2,4)]
    [Tooltip("Optional description shown in the UI")]
    public string description;
}
