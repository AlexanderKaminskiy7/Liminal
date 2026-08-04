using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveManager
{
    private static string SavePath => Application.persistentDataPath + "/liminal_save.json";

    [System.Serializable]
    class SaveData
    {
        public string[] completedQuests;
        public string[] setFlags;
        public string currentScene;
    }

    public static void Save()
    {
        var data = new SaveData();

        var qm = QuestChainManager.Instance;
        if (qm != null) data.completedQuests = qm.GetCompletedIds().ToArray();

        var fm = Object.FindAnyObjectByType<GameFlagManager>();
        if (fm != null) data.setFlags = fm.GetSetFlagIds().ToArray();

        data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        File.WriteAllText(SavePath, JsonUtility.ToJson(data));
        Debug.Log("[SaveManager] Сохранено: " + SavePath);
    }

    public static void Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("[SaveManager] Сохранений нет.");
            return;
        }

        var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath));

        var qm = QuestChainManager.Instance;
        if (qm != null && data.completedQuests != null)
            qm.LoadCompleted(new List<string>(data.completedQuests));

        var fm = Object.FindAnyObjectByType<GameFlagManager>();
        if (fm != null && data.setFlags != null)
            fm.LoadFlags(new List<string>(data.setFlags));

        Debug.Log("[SaveManager] Загружено. Сцена: " + data.currentScene);
    }

    public static bool HasSave() => File.Exists(SavePath);

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("[SaveManager] Сохранение удалено.");
        }
    }

    public static string GetSavedSceneName()
    {
        if (!HasSave()) return null;
        var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath));
        return data.currentScene;
    }
}
