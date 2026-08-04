using UnityEngine;
using TMPro;

[AddComponentMenu("Liminal/Time Controller")]
public class TimeController : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float realSecondsPerGameMinute = 2f;

    [Header("Начальное время")]
    [SerializeField] private int startHour = 7;
    [SerializeField] private int startMinute = 0;

    [Header("Конечное время (когда тревога пик)")]
    [SerializeField] private int endHour = 9;
    [SerializeField] private int endMinute = 0;

    private float currentMinutes;
    private bool isRunning = true;

    private void Start()
    {
        currentMinutes = startHour * 60 + startMinute;
        UpdateDisplay();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentMinutes += Time.deltaTime / realSecondsPerGameMinute;

        int totalEnd = endHour * 60 + endMinute;
        if (currentMinutes >= totalEnd)
        {
            currentMinutes = totalEnd;
            isRunning = false;
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (timeText == null) return;

        int h = Mathf.FloorToInt(currentMinutes / 60f);
        int m = Mathf.FloorToInt(currentMinutes % 60f);
        timeText.text = $"{h:D2}:{m:D2}";
    }

    public void StopTime() => isRunning = false;
    public void StartTime() => isRunning = true;
}
