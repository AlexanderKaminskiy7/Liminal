using UnityEngine;

[CreateAssetMenu(fileName = "GameFlag", menuName = "Liminal/Game Flag")]
public class GameFlagData : ScriptableObject
{
    [Tooltip("Unique identifier for the flag")]
    public string id;

    [Tooltip("Human readable name for designers")]
    public string displayName;
}
