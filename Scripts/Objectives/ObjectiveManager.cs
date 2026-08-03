using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Liminal/Objective Manager")]
public class ObjectiveManager : MonoBehaviour
{
    private static ObjectiveManager instance;
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

    [SerializeField] private List<ObjectiveData> locked = new List<ObjectiveData>();
    [SerializeField] private List<ObjectiveData> active = new List<ObjectiveData>();
    [SerializeField] private List<ObjectiveData> completed = new List<ObjectiveData>();

    public UnityEvent onObjectivesChanged = new UnityEvent();

    public IReadOnlyList<ObjectiveData> ActiveObjectives => active.AsReadOnly();
    public IReadOnlyList<ObjectiveData> CompletedObjectives => completed.AsReadOnly();

    public void UnlockObjective(ObjectiveData obj)
    {
        if (obj == null) return;
        if (IsCompleted(obj)) return;
        if (active.Contains(obj)) return;

        // remove from locked if present
        if (locked.Contains(obj)) locked.Remove(obj);

        active.Add(obj);
        onObjectivesChanged.Invoke();
    }

    public void CompleteObjective(ObjectiveData obj)
    {
        if (obj == null) return;
        if (IsCompleted(obj)) return;

        if (active.Contains(obj)) active.Remove(obj);
        if (locked.Contains(obj)) locked.Remove(obj);

        completed.Add(obj);
        onObjectivesChanged.Invoke();
    }

    public bool IsCompleted(ObjectiveData obj)
    {
        if (obj == null) return false;
        return completed.Contains(obj);
    }
}
