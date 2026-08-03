using UnityEngine;

/// <summary>
/// Простая контроллерная компонента для 2D point-and-click взаимодействий.
/// - Использует Physics2D.OverlapPoint для определения интерактивных объектов под курсором.
/// - Вызывает UnityEvent'ы на Interactable компонентах: OnHoverEnter, OnHoverExit, OnInteract.
/// </summary>
public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private LayerMask interactableLayer = ~0; // по умолчанию все слои

    private Interactable currentInteractable;

    private void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void Update()
    {
        if (targetCamera == null)
            return;

        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPoint = targetCamera.ScreenToWorldPoint(mousePos);

        Collider2D col = Physics2D.OverlapPoint(worldPoint, interactableLayer);
        Interactable found = null;
        if (col != null)
        {
            found = col.GetComponentInParent<Interactable>();
        }

        // Hover enter / exit
        if (found != currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnHoverExit.Invoke();
            }

            currentInteractable = found;

            if (currentInteractable != null)
            {
                currentInteractable.OnHoverEnter.Invoke();
            }
        }

        // Click
        if (Input.GetMouseButtonDown(0))
        {
            if (currentInteractable != null)
                currentInteractable.OnInteract.Invoke();
        }
    }
}
