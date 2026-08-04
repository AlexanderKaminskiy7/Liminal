using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUI ui;

    public UnityEvent OnDialogueEnded = new UnityEvent();

    private DialogueData current;
    private int index;

    public bool IsPlaying => current != null;

    public void StartDialogue(DialogueData data)
    {
        if (current != null)
            return;

        if (data == null || data.entries == null || data.entries.Count == 0)
            return;

        current = data;
        index = 0;

        if (ui != null)
        {
            ui.ShowEntry(current.entries[index]);
            if (ui.NextButton != null)
            {
                ui.NextButton.onClick.RemoveListener(Next);
                ui.NextButton.onClick.AddListener(Next);
            }
        }
    }

    public void Next()
    {
        if (current == null)
            return;

        index++;
        if (index >= current.entries.Count)
        {
            EndDialogue();
            return;
        }

        if (ui != null)
            ui.ShowEntry(current.entries[index]);
    }

    public void EndDialogue()
    {
        current = null;
        index = 0;

        if (ui != null)
        {
            if (ui.NextButton != null)
                ui.NextButton.onClick.RemoveListener(Next);

            ui.Hide();
        }

        OnDialogueEnded.Invoke();
    }
}
