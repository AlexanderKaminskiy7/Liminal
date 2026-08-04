using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[AddComponentMenu("Liminal/Quest Chain Manager")]
public class QuestChainManager : MonoBehaviour
{
    private static QuestChainManager instance;

    [Tooltip("Первый квест игры (например, 'Взять телефон')")]
    [SerializeField] private QuestData startingQuest;

    [Header("События для UI и других систем")]
    public UnityEvent<QuestData> onQuestActivated = new UnityEvent<QuestData>();
    public UnityEvent<QuestData> onQuestCompleted = new UnityEvent<QuestData>();
    public UnityEvent onAllQuestsDone = new UnityEvent();

    private QuestData currentQuest;
    private HashSet<string> completedQuests = new HashSet<string>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (currentQuest == null && startingQuest != null)
            ActivateQuest(startingQuest);
    }

    public static QuestChainManager Instance => instance;
    public QuestData CurrentQuest => currentQuest;

    public bool IsCompleted(QuestData q) => q != null && completedQuests.Contains(q.questId);

    public void ActivateQuest(QuestData q)
    {
        if (q == null) return;
        currentQuest = q;
        onQuestActivated?.Invoke(q);
        Debug.Log($"[QuestChain] Активен: {q.displayText}");

        if (q.autoCompleteOnTrigger)
            CompleteCurrentQuest();
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest == null) return;

        completedQuests.Add(currentQuest.questId);
        onQuestCompleted?.Invoke(currentQuest);
        Debug.Log($"[QuestChain] Выполнен: {currentQuest.displayText}");

        if (currentQuest.nextQuest != null)
        {
            ActivateQuest(currentQuest.nextQuest);
        }
        else
        {
            currentQuest = null;
            onAllQuestsDone?.Invoke();
            Debug.Log("[QuestChain] Все задачи выполнены!");
        }
    }

    public List<string> GetCompletedIds() => new List<string>(completedQuests);

    public void LoadCompleted(List<string> ids)
    {
        completedQuests = new HashSet<string>(ids);
    }
}
