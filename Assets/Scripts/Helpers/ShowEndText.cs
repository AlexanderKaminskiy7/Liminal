using UnityEngine;

public class ShowEndText : MonoBehaviour
{
    public GameObject endText;
    public GameObject menuButton;
    public float delay = 2f;

    void Start()
    {
        Invoke(nameof(Show), delay);
    }

    void Show()
    {
        if (endText != null) endText.SetActive(true);
        if (menuButton != null) menuButton.SetActive(true);
    }
}
