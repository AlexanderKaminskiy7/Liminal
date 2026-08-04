using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LayerMask interactableLayer = ~0;

    private Interactable currentInteractable;

    private void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void Update()
    {
        if (targetCamera == null) return;

        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPoint = targetCamera.ScreenToWorldPoint(mousePos);

        Collider2D col = Physics2D.OverlapPoint(worldPoint, interactableLayer);
        Interactable found = null;
        if (col != null)
        {
            found = col.GetComponentInParent<Interactable>();
        }

        if (found != currentInteractable)
        {
            if (currentInteractable != null)
                currentInteractable.OnHoverExit.Invoke();

            currentInteractable = found;

            if (currentInteractable != null)
                currentInteractable.OnHoverEnter.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentInteractable != null)
                currentInteractable.OnInteract.Invoke();
        }
    }
}
