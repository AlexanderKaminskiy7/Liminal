using UnityEngine;
using UnityEngine.UI;
using TMPro;

[AddComponentMenu("Liminal/Phone Controller")]
public class PhoneController : MonoBehaviour
{
    [SerializeField] private GameObject phonePanel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TMP_Text detailText;

    [Header("Контент (создаётся через Assets > Create > Liminal > Phone)")]
    [SerializeField] private PhoneContactData[] contacts;
    [SerializeField] private PhoneMessageData[] messages;

    private bool isOpen = false;

    public void TogglePhone()
    {
        isOpen = !isOpen;
        phonePanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;

        if (isOpen) ShowContacts();
    }

    public void ShowContacts()
    {
        ClearContent();
        if (contacts == null) return;

        foreach (var c in contacts)
        {
            if (c == null) continue;
            var btn = Instantiate(entryPrefab, contentParent).GetComponent<Button>();
            btn.GetComponentInChildren<TMP_Text>().text = c.contactName;
            var capture = c;
            btn.onClick.AddListener(() => ShowDetail(capture.description));
        }
    }

    public void ShowMessages()
    {
        ClearContent();
        if (messages == null) return;

        foreach (var m in messages)
        {
            if (m == null) continue;
            var btn = Instantiate(entryPrefab, contentParent).GetComponent<Button>();
            string prefix = m.isRead ? "" : "[NEW] ";
            btn.GetComponentInChildren<TMP_Text>().text = prefix + m.sender;
            var capture = m;
            btn.onClick.AddListener(() => { capture.isRead = true; ShowDetail(capture.messageText); });
        }
    }

    void ShowDetail(string text)
    {
        if (detailText != null) detailText.text = text;
    }

    void ClearContent()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);
    }
}
