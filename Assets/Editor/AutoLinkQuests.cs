using UnityEngine;
using UnityEditor;
using System.Linq;

public class AutoLinkQuests : EditorWindow
{
    [MenuItem("Liminal/1. Связать квесты в цепочку")]
    static void LinkQuests()
    {
        var allQuests = AssetDatabase.FindAssets("t:QuestData")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<QuestData>)
            .Where(q => q != null)
            .ToList();

        // Определяем порядок по имени файла
        var ordered = allQuests.OrderBy(q => q.name).ToList();

        for (int i = 0; i < ordered.Count - 1; i++)
        {
            var current = ordered[i];
            var next = ordered[i + 1];

            current.nextQuest = next;
            EditorUtility.SetDirty(current);
            Debug.Log($"[AutoLink] {current.name} → {next.name}");
        }

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Готово!", $"Связано {ordered.Count} квестов в цепочку.", "OK");
    }
}
