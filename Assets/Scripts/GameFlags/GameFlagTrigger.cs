using UnityEngine;

[AddComponentMenu("Liminal/Game Flag Trigger")]
public class GameFlagTrigger : MonoBehaviour
{
    [SerializeField] private GameFlagManager manager;
    [SerializeField] private GameFlagData flag;
    [SerializeField] private bool value = true;

    private void Awake()
    {
        if (manager == null)
            manager = FindAnyObjectByType<GameFlagManager>();
    }

    public void Activate()
    {
        if (manager == null || flag == null) return;
        manager.SetFlag(flag, value);
    }
}
