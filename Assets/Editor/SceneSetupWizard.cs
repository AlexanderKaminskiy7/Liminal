using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class SceneSetupWizard : EditorWindow
{
    [MenuItem("Liminal/2. Настроить сцену (Мастер)")]
    static void Open() => GetWindow<SceneSetupWizard>("Сетап сцены");

    bool addTime = true;
    bool addPhone = true;
    bool addQuestUI = true;
    bool addDialogue = true;
    bool addFade = true;

    void OnGUI()
    {
        GUILayout.Label("Быстрый сетап сцены", EditorStyles.boldLabel);
        addTime = GUILayout.Toggle(addTime, "Часы (TimeController)");
        addPhone = GUILayout.Toggle(addPhone, "Телефон (PhoneController)");
        addQuestUI = GUILayout.Toggle(addQuestUI, "Квесты (QuestUI)");
        addDialogue = GUILayout.Toggle(addDialogue, "Диалоги (DialogueManager + UI)");
        addFade = GUILayout.Toggle(addFade, "Fade (FadeController)");

        GUILayout.Space(10);
        if (GUILayout.Button("СОЗДАТЬ ВСЁ!", GUILayout.Height(40)))
        {
            SetupScene();
        }

        GUILayout.Space(20);
        EditorGUILayout.HelpBox("После создания НАЗНАЧЬ ссылки вручную в Inspector:\n" +
            "- QuestChainManager → Starting Quest\n" +
            "- TimeController → Time Text\n" +
            "- QuestUI → Quest Text + Panel\n" +
            "- DialogueManager → UI\n" +
            "- PhoneController → Panel + Content + DetailText", MessageType.Info);
    }

    void SetupScene()
    {
        // Managers
        var managers = GameObject.Find("Managers");
        if (managers == null) managers = new GameObject("Managers");

        if (managers.GetComponent<QuestChainManager>() == null)
            managers.AddComponent<QuestChainManager>();

        if (managers.GetComponent<GameFlagManager>() == null)
            managers.AddComponent<GameFlagManager>();

        var ic = FindAnyObjectByType<InteractionController>();
        if (ic == null)
        {
            var icGo = new GameObject("InteractionController");
            icGo.AddComponent<InteractionController>();
        }

        // Canvas
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            canvas = new GameObject("Canvas");
            var c = canvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.sortingOrder = 0;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
        }

        // Fade
        if (addFade)
        {
            if (managers.GetComponent<FadeController>() == null)
                managers.AddComponent<FadeController>();

            var fadeCanvas = GameObject.Find("FadeCanvas");
            if (fadeCanvas == null)
            {
                fadeCanvas = new GameObject("FadeCanvas");
                fadeCanvas.transform.SetParent(canvas.transform);
                var fc = fadeCanvas.AddComponent<Canvas>();
                fc.renderMode = RenderMode.ScreenSpaceOverlay;
                fc.sortingOrder = 999;
                fadeCanvas.AddComponent<CanvasGroup>();
                var img = fadeCanvas.AddComponent<Image>();
                img.color = Color.black;
                var rt = img.GetComponent<RectTransform>();
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.sizeDelta = Vector2.zero;
                fadeCanvas.AddComponent<FadeCanvas>();
            }
        }

        // QuestUI
        if (addQuestUI)
        {
            var questPanel = GameObject.Find("QuestPanel");
            if (questPanel == null)
            {
                questPanel = new GameObject("QuestPanel");
                questPanel.transform.SetParent(canvas.transform);
                var rt = questPanel.AddComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 0.12f);
                rt.sizeDelta = Vector2.zero;
                var img = questPanel.AddComponent<Image>();
                img.color = new Color(0, 0, 0, 0.7f);

                var questText = new GameObject("QuestText");
                questText.transform.SetParent(questPanel.transform);
                var trt = questText.AddComponent<RectTransform>();
                trt.anchorMin = Vector2.zero;
                trt.anchorMax = Vector2.one;
                trt.sizeDelta = new Vector2(-20, -10);
                var tmp = questText.AddComponent<TextMeshProUGUI>();
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.fontSize = 22;
                tmp.color = Color.white;

                questPanel.AddComponent<QuestUI>();
            }
        }

        // TimeController
        if (addTime)
        {
            var timeObj = GameObject.Find("TimeText");
            if (timeObj == null)
            {
                timeObj = new GameObject("TimeText");
                timeObj.transform.SetParent(canvas.transform);
                var rt = timeObj.AddComponent<RectTransform>();
                rt.anchorMin = new Vector2(0.85f, 0.9f);
                rt.anchorMax = new Vector2(1, 1);
                rt.sizeDelta = Vector2.zero;
                var tmp = timeObj.AddComponent<TextMeshProUGUI>();
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.fontSize = 32;
                tmp.color = Color.white;
                tmp.text = "07:00";

                if (managers.GetComponent<TimeController>() == null)
                    managers.AddComponent<TimeController>();
            }
        }

        // Dialogue
        if (addDialogue)
        {
            if (managers.GetComponent<DialogueManager>() == null)
                managers.AddComponent<DialogueManager>();

            var dui = GameObject.Find("DialogueUI");
            if (dui == null)
            {
                dui = new GameObject("DialogueUI");
                dui.transform.SetParent(canvas.transform);
                var rt = dui.AddComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 0.22f);
                rt.sizeDelta = Vector2.zero;
                var img = dui.AddComponent<Image>();
                img.color = new Color(0, 0, 0, 0.9f);

                // Portrait
                var port = new GameObject("Portrait");
                port.transform.SetParent(dui.transform);
                var prt = port.AddComponent<RectTransform>();
                prt.anchorMin = new Vector2(0.01f, 0.1f);
                prt.anchorMax = new Vector2(0.12f, 0.9f);
                prt.sizeDelta = Vector2.zero;
                port.AddComponent<Image>();

                // Name
                var nameObj = new GameObject("Name");
                nameObj.transform.SetParent(dui.transform);
                var nrt = nameObj.AddComponent<RectTransform>();
                nrt.anchorMin = new Vector2(0.14f, 0.7f);
                nrt.anchorMax = new Vector2(0.5f, 0.95f);
                nrt.sizeDelta = Vector2.zero;
                var ntmp = nameObj.AddComponent<TextMeshProUGUI>();
                ntmp.fontSize = 20;
                ntmp.color = Color.yellow;

                // Text
                var textObj = new GameObject("Text");
                textObj.transform.SetParent(dui.transform);
                var trt2 = textObj.AddComponent<RectTransform>();
                trt2.anchorMin = new Vector2(0.14f, 0.1f);
                trt2.anchorMax = new Vector2(0.82f, 0.7f);
                trt2.sizeDelta = Vector2.zero;
                var ttmp = textObj.AddComponent<TextMeshProUGUI>();
                ttmp.fontSize = 18;

                // Next button
                var btnObj = new GameObject("NextButton");
                btnObj.transform.SetParent(dui.transform);
                var brt = btnObj.AddComponent<RectTransform>();
                brt.anchorMin = new Vector2(0.84f, 0.1f);
                brt.anchorMax = new Vector2(0.99f, 0.5f);
                brt.sizeDelta = Vector2.zero;
                var bimg = btnObj.AddComponent<Image>();
                bimg.color = new Color(0.2f, 0.2f, 0.2f, 1);
                btnObj.AddComponent<Button>();
                var btxt = new GameObject("Text");
                btxt.transform.SetParent(btnObj.transform);
                var btrt = btxt.AddComponent<RectTransform>();
                btrt.anchorMin = Vector2.zero;
                btrt.anchorMax = Vector2.one;
                btrt.sizeDelta = Vector2.zero;
                var btmp = btxt.AddComponent<TextMeshProUGUI>();
                btmp.alignment = TextAlignmentOptions.Center;
                btmp.text = "Далее";

                dui.AddComponent<DialogueUI>();
            }
        }

        // Phone
        if (addPhone)
        {
            var phonePanel = GameObject.Find("PhonePanel");
            if (phonePanel == null)
            {
                phonePanel = new GameObject("PhonePanel");
                phonePanel.transform.SetParent(canvas.transform);
                phonePanel.SetActive(false);
                var rt = phonePanel.AddComponent<RectTransform>();
                rt.anchorMin = new Vector2(0.7f, 0.1f);
                rt.anchorMax = new Vector2(0.95f, 0.9f);
                rt.sizeDelta = Vector2.zero;
                var img = phonePanel.AddComponent<Image>();
                img.color = new Color(0.1f, 0.1f, 0.1f, 0.95f);

                var content = new GameObject("Content");
                content.transform.SetParent(phonePanel.transform);
                var crt = content.AddComponent<RectTransform>();
                crt.anchorMin = new Vector2(0.05f, 0.3f);
                crt.anchorMax = new Vector2(0.95f, 0.8f);
                crt.sizeDelta = Vector2.zero;

                var detail = new GameObject("DetailText");
                detail.transform.SetParent(phonePanel.transform);
                var drt = detail.AddComponent<RectTransform>();
                drt.anchorMin = new Vector2(0.05f, 0.05f);
                drt.anchorMax = new Vector2(0.95f, 0.25f);
                drt.sizeDelta = Vector2.zero;
                var dtmp = detail.AddComponent<TextMeshProUGUI>();
                dtmp.fontSize = 14;

                // Buttons
                var btnParent = new GameObject("Buttons");
                btnParent.transform.SetParent(phonePanel.transform);
                var bprt = btnParent.AddComponent<RectTransform>();
                bprt.anchorMin = new Vector2(0.05f, 0.85f);
                bprt.anchorMax = new Vector2(0.95f, 0.98f);
                bprt.sizeDelta = Vector2.zero;

                CreatePhoneButton(btnParent, "Контакты", 0);
                CreatePhoneButton(btnParent, "Сообщения", 1);
                CreatePhoneButton(btnParent, "Закрыть", 2);

                phonePanel.AddComponent<PhoneController>();
            }
        }

        EditorUtility.DisplayDialog("Готово!", 
            "Объекты созданы.\n\nТЕПЕРЬ ВРУЧНУЮ назначь:\n" +
            "1. QuestChainManager → Starting Quest\n" +
            "2. TimeController → Time Text (TimeText)\n" +
            "3. QuestUI → Quest Text + Quest Panel\n" +
            "4. DialogueManager → UI (DialogueUI)\n" +
            "5. PhoneController → все поля\n" +
            "6. FadeController → Fade Canvas", "OK");
    }

    void CreatePhoneButton(GameObject parent, string text, int index)
    {
        var btn = new GameObject(text);
        btn.transform.SetParent(parent.transform);
        var rt = btn.AddComponent<RectTransform>();
        float w = 1f / 3f;
        rt.anchorMin = new Vector2(index * w + 0.01f, 0);
        rt.anchorMax = new Vector2((index + 1) * w - 0.01f, 1);
        rt.sizeDelta = Vector2.zero;
        var img = btn.AddComponent<Image>();
        img.color = new Color(0.3f, 0.3f, 0.3f, 1);
        btn.AddComponent<Button>();
        var txt = new GameObject("Text");
        txt.transform.SetParent(btn.transform);
        var trt = txt.AddComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.sizeDelta = Vector2.zero;
        var tmp = txt.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 14;
    }
}
