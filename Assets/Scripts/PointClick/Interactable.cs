using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnHoverEnter = new UnityEvent();
    public UnityEvent OnHoverExit = new UnityEvent();
    public UnityEvent OnInteract = new UnityEvent();
}
