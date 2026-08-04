using UnityEngine;

[CreateAssetMenu(fileName = "Inspect", menuName = "Liminal/Inspect Data")]
public class InspectData : ScriptableObject
{
    [System.Serializable]
    public class Entry
    {
        [Tooltip("Если null — показывать всегда (fallback). Иначе — только когда этот квест активен.")]
        public QuestData requiredQuestActive;

        [Tooltip("Если true — показывать только когда requiredQuestActive выполнен")]
        public bool requireQuestCompleted;

        [TextArea(3, 6)]
        public string text;

        [Tooltip("Опционально: запустить готовый диалог вместо простого текста")]
        public DialogueData dialogue;
    }

    public Entry[] entries;
}
