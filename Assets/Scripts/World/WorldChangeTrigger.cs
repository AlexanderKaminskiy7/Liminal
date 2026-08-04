using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Liminal/World Change Trigger")]
public class WorldChangeTrigger : MonoBehaviour
{
    [System.Serializable]
    public class ChangeEntry
    {
        [Tooltip("Если этот флаг установлен (или не установлен — см. requireFlagSet)")]
        public GameFlagData requiredFlag;

        [Tooltip("true = нужен установленный флаг, false = нужен сброшенный")]
        public bool requireFlagSet = true;

        [Header("Что меняем")]
        public GameObject objectToEnable;
        public GameObject objectToDisable;
        public SpriteRenderer spriteToChange;
        public Sprite newSprite;
        public InspectData newInspectData;
    }

    [SerializeField] private ChangeEntry[] changes;

    [Tooltip("Проверять при старте сцены автоматически")]
    [SerializeField] private bool checkOnStart = true;

    private void Start()
    {
        if (checkOnStart) ApplyChanges();
    }

    public void ApplyChanges()
    {
        var fm = FindAnyObjectByType<GameFlagManager>();
        if (fm == null) return;

        foreach (var c in changes)
        {
            bool flagState = c.requiredFlag != null ? fm.GetFlag(c.requiredFlag) : false;
            bool condition = c.requireFlagSet ? flagState : !flagState;

            if (!condition) continue;

            if (c.objectToEnable != null) c.objectToEnable.SetActive(true);
            if (c.objectToDisable != null) c.objectToDisable.SetActive(false);
            if (c.spriteToChange != null && c.newSprite != null) c.spriteToChange.sprite = c.newSprite;

            if (c.newInspectData != null)
            {
                var inspect = GetComponent<InspectableObject>();
                if (inspect != null)
                    inspect.SetData(c.newInspectData);
            }
        }
    }
}
