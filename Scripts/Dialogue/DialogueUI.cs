using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Button nextButton;

    public Button NextButton => nextButton;

    private void Awake()
    {
        if (root != null)
            root.SetActive(false);
    }

    public void ShowEntry(DialogueEntry entry)
    {
        if (root != null)
            root.SetActive(true);

        if (characterNameText != null)
            characterNameText.text = entry != null ? entry.characterName : string.Empty;

        if (dialogueText != null)
            dialogueText.text = entry != null ? entry.text : string.Empty;

        if (portraitImage != null)
            portraitImage.sprite = entry != null ? entry.portrait : null;
    }

    public void Hide()
    {
        if (root != null)
            root.SetActive(false);
    }
}
