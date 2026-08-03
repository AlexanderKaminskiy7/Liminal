using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Liminal/Game Flag Manager")]
public class GameFlagManager : MonoBehaviour
{
    private static GameFlagManager instance;
    private Dictionary<GameFlagData, bool> flags = new Dictionary<GameFlagData, bool>();

    private void Awake()
    {
        // Use static instance to prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFlag(GameFlagData flag, bool value)
    {
        if (flag == null) return;
        flags[flag] = value;
    }

    public bool GetFlag(GameFlagData flag)
    {
        if (flag == null) return false;
        if (flags.TryGetValue(flag, out var v)) return v;
        return false;
    }

    public void ResetFlags()
    {
        flags.Clear();
    }
}
