using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Liminal/Quest")]
public class QuestData : ScriptableObject
{
    [Tooltip("Внутренний ID для сохранений. Должен быть уникальным.")]
    public string questId;

    [Tooltip("Текст, который видит игрок в списке дел (например: 'Взять телефон')")]
    public string displayText;

    [Tooltip("Описание для геймдизайнера, не видно игроку")]
    [TextArea(2, 4)]
    public string designerNote;

    [Tooltip("Следующий квест, который откроется после выполнения этого")]
    public QuestData nextQuest;

    [Tooltip("Если true — квест считается выполненным сразу при активации (без клика игрока)")]
    public bool autoCompleteOnTrigger;
}
