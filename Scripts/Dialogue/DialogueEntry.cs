using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string characterName;
    public Sprite portrait;
    [TextArea(2,6)]
    public string text;
}
