using System.Text;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Liminal/Objective UI")]
public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private ObjectiveManager manager;
    [SerializeField] private Text activeText;
    [SerializeField] private Text completedText;

    private void OnEnable()
    {
        if (manager != null)
            manager.onObjectivesChanged.AddListener(UpdateUI);

        UpdateUI();
    }

    private void OnDisable()
    {
        if (manager != null)
            manager.onObjectivesChanged.RemoveListener(UpdateUI);
    }

    public void UpdateUI()
    {
        if (manager == null)
            return;

        var sbActive = new StringBuilder();
        foreach (var o in manager.ActiveObjectives)
        {
            if (o == null) continue;
            sbActive.AppendLine($"□ {o.displayName}");
        }

        var sbCompleted = new StringBuilder();
        foreach (var o in manager.CompletedObjectives)
        {
            if (o == null) continue;
            sbCompleted.AppendLine($"✓ {o.displayName}");
        }

        if (activeText != null)
            activeText.text = sbActive.ToString();

        if (completedText != null)
            completedText.text = sbCompleted.ToString();
    }
}
