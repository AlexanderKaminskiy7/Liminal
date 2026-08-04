using UnityEngine;
using TMPro;

[AddComponentMenu("Liminal/Quest UI")]
public class QuestUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questText;
    [SerializeField] private GameObject questPanel;

    private void Start()
    {
        var qm = QuestChainManager.Instance;
        if (qm != null)
        {
            qm.onQuestActivated.AddListener(OnQuestChanged);
            qm.onQuestCompleted.AddListener(OnQuestCompleted);
        }

        UpdateDisplay();
    }

    private void OnDestroy()
    {
        var qm = QuestChainManager.Instance;
        if (qm != null)
        {
            qm.onQuestActivated.RemoveListener(OnQuestChanged);
            qm.onQuestCompleted.RemoveListener(OnQuestCompleted);
        }
    }

    void OnQuestChanged(QuestData q) => UpdateDisplay();
    void OnQuestCompleted(QuestData q) => UpdateDisplay();

    void UpdateDisplay()
    {
        var qm = QuestChainManager.Instance;
        if (qm == null) return;

        if (qm.CurrentQuest != null)
        {
            if (questPanel != null) questPanel.SetActive(true);
            if (questText != null) questText.text = qm.CurrentQuest.displayText;
        }
        else
        {
            if (questPanel != null) questPanel.SetActive(false);
            if (questText != null) questText.text = "";
        }
    }
}
