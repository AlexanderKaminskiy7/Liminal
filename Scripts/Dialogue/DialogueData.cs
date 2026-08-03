using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Liminal/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public List<DialogueEntry> entries = new List<DialogueEntry>();
}
