using UnityEngine;

[AddComponentMenu("Liminal/Game Flag Trigger")]
public class GameFlagTrigger : MonoBehaviour
{
    [SerializeField] private GameFlagManager manager;
    [SerializeField] private GameFlagData flag;
    [SerializeField] private bool value = true;

    // Designer calls this from Interactable.OnInteract
    private void Awake()
    {
        if (manager == null)
            manager = FindFirstObjectByType<GameFlagManager>();
    }
    public void Activate()
    {
        if (manager == null || flag == null) return;
        manager.SetFlag(flag, value);
    }
}
