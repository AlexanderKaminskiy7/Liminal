using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    // Invoked when the pointer starts hovering this object
    public UnityEvent OnHoverEnter = new UnityEvent();

    // Invoked when the pointer stops hovering this object
    public UnityEvent OnHoverExit = new UnityEvent();

    // Invoked when the object is clicked (left mouse button)
    public UnityEvent OnInteract = new UnityEvent();
}
