using UnityEngine;

public class CafeStartDialogue : MonoBehaviour
{
    void Start()
    {
        var dm = FindAnyObjectByType<DialogueManager>();
        var dialogue = Resources.Load<DialogueData>("Dialogues/D_Admin_Start");
        if (dialogue != null && dm != null)
            dm.StartDialogue(dialogue);
    }
}
