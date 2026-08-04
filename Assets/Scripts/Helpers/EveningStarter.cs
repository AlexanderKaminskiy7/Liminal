using UnityEngine;

public class EveningStarter : MonoBehaviour
{
    void Start()
    {
        var ft = GetComponent<GameFlagTrigger>();
        if (ft != null) ft.Activate();

        var dm = FindAnyObjectByType<DialogueManager>();
        var dialogue = Resources.Load<DialogueData>("Dialogues/D_Friend");
        if (dialogue != null && dm != null)
            dm.StartDialogue(dialogue);
    }
}
