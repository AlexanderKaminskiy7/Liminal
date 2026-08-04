using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[AddComponentMenu("Liminal/Fade Canvas")]
public class FadeCanvas : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnValidate()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (fadeImage != null)
        {
            var c = fadeImage.color;
            c.r = 0f; c.g = 0f; c.b = 0f;
            fadeImage.color = c;
        }
    }

    public float Alpha
    {
        get => canvasGroup != null ? canvasGroup.alpha : 0f;
        set
        {
            if (canvasGroup == null) return;
            canvasGroup.alpha = Mathf.Clamp01(value);
            canvasGroup.blocksRaycasts = canvasGroup.alpha > 0f;
        }
    }

    public void InstantBlack()
    {
        if (canvasGroup == null) return;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void InstantClear()
    {
        if (canvasGroup == null) return;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetBlocksRaycasts(bool enabled)
    {
        if (canvasGroup == null) return;
        canvasGroup.blocksRaycasts = enabled;
    }
}
