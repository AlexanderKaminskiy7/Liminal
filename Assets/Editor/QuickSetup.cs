using UnityEngine;
using UnityEditor;

public class QuickSetup : EditorWindow
{
    [MenuItem("Liminal/3. Быстрый сетап объекта %q")]
    static void Open() => GetWindow<QuickSetup>("Quick Setup");

    GameObject target;
    SetupType type;
    QuestData questData;
    GameFlagData flagData;
    InspectData inspectData;
    string doorScene = "";

    enum SetupType { Quest, Inspect, Door, Flag }

    void OnGUI()
    {
        GUILayout.Label("Быстрая настройка объекта", EditorStyles.boldLabel);
        target = EditorGUILayout.ObjectField("Объект:", target, typeof(GameObject), true) as GameObject;
        type = (SetupType)EditorGUILayout.EnumPopup("Тип:", type);

        if (type == SetupType.Quest)
            questData = EditorGUILayout.ObjectField("Квест:", questData, typeof(QuestData), false) as QuestData;
        if (type == SetupType.Flag)
            flagData = EditorGUILayout.ObjectField("Флаг:", flagData, typeof(GameFlagData), false) as GameFlagData;
        if (type == SetupType.Inspect)
            inspectData = EditorGUILayout.ObjectField("Inspect Data:", inspectData, typeof(InspectData), false) as InspectData;
        if (type == SetupType.Door)
            doorScene = EditorGUILayout.TextField("Сцена:", doorScene);

        GUILayout.Space(10);
        if (GUILayout.Button("НАСТРОИТЬ!", GUILayout.Height(40)) && target != null)
        {
            Setup();
        }
    }

    void Setup()
    {
        Undo.RecordObject(target, "Liminal Setup");

        // Collider2D
        var col = target.GetComponent<Collider2D>();
        if (col == null)
        {
            col = target.AddComponent<BoxCollider2D>();
            (col as BoxCollider2D).size = new Vector2(1, 1);
        }

        // Interactable
        var interactable = target.GetComponent<Interactable>();
        if (interactable == null) interactable = target.AddComponent<Interactable>();

        // Слой
        int interactableLayer = LayerMask.NameToLayer("Interactable");
        if (interactableLayer == -1)
        {
            EditorUtility.DisplayDialog("Ошибка!", "Создай слой 'Interactable' в Edit > Project Settings > Tags and Layers", "OK");
            return;
        }
        target.layer = interactableLayer;

        // Очистим старые события
        var soInteractable = new SerializedObject(interactable);
        var onInteract = soInteractable.FindProperty("onInteract");
        onInteract.ClearArray();
        soInteractable.ApplyModifiedProperties();

        switch (type)
        {
            case SetupType.Quest:
                SetupQuest(interactable);
                break;
            case SetupType.Inspect:
                SetupInspect(interactable);
                break;
            case SetupType.Door:
                SetupDoor(interactable);
                break;
            case SetupType.Flag:
                SetupFlag(interactable);
                break;
        }

        EditorUtility.SetDirty(target);
        Debug.Log($"[Liminal] {target.name} настроен как {type}");
    }

    void SetupQuest(Interactable interactable)
    {
        var qt = target.GetComponent<QuestTrigger>();
        if (qt == null) qt = target.AddComponent<QuestTrigger>();

        // Настраиваем QuestTrigger через SerializedObject
        var so = new SerializedObject(qt);
        so.FindProperty("specificQuest").objectReferenceValue = questData;
        so.FindProperty("requireCurrentQuestMatch").boolValue = true;
        so.ApplyModifiedProperties();

        // Добавляем в OnInteract
        AddListener(interactable, qt, "TryTrigger");
    }

    void SetupInspect(Interactable interactable)
    {
        var io = target.GetComponent<InspectableObject>();
        if (io == null) io = target.AddComponent<InspectableObject>();

        var so = new SerializedObject(io);
        so.FindProperty("data").objectReferenceValue = inspectData;
        so.ApplyModifiedProperties();

        AddListener(interactable, io, "Inspect");
    }

    void SetupDoor(Interactable interactable)
    {
        var cd = target.GetComponent<ConditionalDoor>();
        if (cd == null) cd = target.AddComponent<ConditionalDoor>();

        var so = new SerializedObject(cd);
        so.FindProperty("targetScene").stringValue = doorScene;
        so.FindProperty("canEnter").boolValue = true;
        so.ApplyModifiedProperties();

        AddListener(interactable, cd, "Interact");
    }

    void SetupFlag(Interactable interactable)
    {
        var ft = target.GetComponent<GameFlagTrigger>();
        if (ft == null) ft = target.AddComponent<GameFlagTrigger>();

        var so = new SerializedObject(ft);
        so.FindProperty("flag").objectReferenceValue = flagData;
        so.FindProperty("value").boolValue = true;
        so.ApplyModifiedProperties();

        AddListener(interactable, ft, "Activate");
    }

    void AddListener(Interactable interactable, Component targetComponent, string methodName)
    {
        var so = new SerializedObject(interactable);
        var onInteract = so.FindProperty("onInteract");
        onInteract.ClearArray();
        onInteract.arraySize = 1;

        var entry = onInteract.GetArrayElementAtIndex(0);
        entry.FindPropertyRelative("targetObject").objectReferenceValue = target.gameObject;

        var callState = entry.FindPropertyRelative("m_CallState");
        callState.intValue = 2; // EditorAndRuntime

        var persistentCall = entry.FindPropertyRelative("m_PersistentCalls.m_Calls");
        persistentCall.arraySize = 1;
        var call = persistentCall.GetArrayElementAtIndex(0);
        call.FindPropertyRelative("m_Target").objectReferenceValue = targetComponent;
        call.FindPropertyRelative("m_MethodName").stringValue = methodName;
        call.FindPropertyRelative("m_Mode").intValue = 1; // Void
        call.FindPropertyRelative("m_Arguments.m_ObjectArgumentAssemblyTypeName").stringValue = "UnityEngine.Object, UnityEngine";
        call.FindPropertyRelative("m_CallState").intValue = 2;

        so.ApplyModifiedProperties();
    }
}
