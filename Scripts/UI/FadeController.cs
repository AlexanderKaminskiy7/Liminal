using System.Collections;
using UnityEngine;

[AddComponentMenu("Liminal/Fade Controller")]
public class FadeController : MonoBehaviour
{
    [SerializeField] private FadeCanvas fadeCanvas;
    [SerializeField] private float defaultDuration = 0.5f;

    private Coroutine running;

    private void OnValidate()
    {
        if (fadeCanvas == null)
            fadeCanvas = GetComponentInChildren<FadeCanvas>();
    }

    public void FadeIn(float duration)
    {
        StartFade(0f, duration <= 0f ? defaultDuration : duration, false);
    }

    public void FadeOut(float duration)
    {
        StartFade(1f, duration <= 0f ? defaultDuration : duration, true);
    }

    public void InstantBlack()
    {
        StopRunning();
        if (fadeCanvas != null) fadeCanvas.InstantBlack();
    }

    public void InstantClear()
    {
        StopRunning();
        if (fadeCanvas != null) fadeCanvas.InstantClear();
    }

    private void StartFade(float target, float duration, bool keepBlocked)
    {
        StopRunning();
        if (fadeCanvas == null) return;
        running = StartCoroutine(FadeRoutine(target, duration, keepBlocked));
    }

    private void StopRunning()
    {
        if (running != null)
        {
            StopCoroutine(running);
            running = null;
        }
    }

    private IEnumerator FadeRoutine(float target, float duration, bool keepBlocked)
    {
        // Block interactions while fading
        fadeCanvas.SetBlocksRaycasts(true);

        float start = fadeCanvas.Alpha;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, target, duration > 0f ? t / duration : 1f);
            fadeCanvas.Alpha = a;
            yield return null;
        }

        fadeCanvas.Alpha = target;
        fadeCanvas.SetBlocksRaycasts(keepBlocked);
        running = null;
    }
}
