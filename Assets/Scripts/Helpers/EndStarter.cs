using UnityEngine;

public class EndStarter : MonoBehaviour
{
    void Start()
    {
        var dm = FindAnyObjectByType<DialogueManager>();
        var dialogue = Resources.Load<DialogueData>("Dialogues/D_Shadows");
        if (dialogue != null && dm != null)
            dm.StartDialogue(dialogue);
    }
}
