using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[AddComponentMenu("Liminal/Coffee Minigame")]
public class CoffeeMinigame : MonoBehaviour
{
    [Header("Заказы (3 штуки для одной смены)")]
    [SerializeField] private CoffeeOrderData[] orders;
    [SerializeField] private int currentOrderIndex = 0;

    [Header("UI")]
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Transform ingredientsPanel;
    [SerializeField] private GameObject ingredientButtonPrefab;

    [Header("Системы")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private string nextSceneName = "06_Evening";

    [Header("Доступные ингредиенты (кнопки создаются автоматически)")]
    [SerializeField] private string[] availableIngredients;

    private List<string> selectedIngredients = new List<string>();

    public void StartMinigame()
    {
        currentOrderIndex = 0;
        ShowCurrentOrder();
        CreateIngredientButtons();
    }

    void ShowCurrentOrder()
    {
        if (currentOrderIndex >= orders.Length)
        {
            EndMinigame();
            return;
        }

        var order = orders[currentOrderIndex];
        orderText.text = $"Заказ #{currentOrderIndex + 1}: {order.customerName}\n{order.orderDescription}";
        feedbackText.text = "Выберите ингредиенты...";
        selectedIngredients.Clear();
    }

    void CreateIngredientButtons()
    {
        foreach (Transform child in ingredientsPanel)
            Destroy(child.gameObject);

        foreach (var ing in availableIngredients)
        {
            var btn = Instantiate(ingredientButtonPrefab, ingredientsPanel).GetComponent<Button>();
            btn.GetComponentInChildren<TMP_Text>().text = ing;
            var capture = ing;
            btn.onClick.AddListener(() => AddIngredient(capture));
        }

        var serveBtn = Instantiate(ingredientButtonPrefab, ingredientsPanel).GetComponent<Button>();
        serveBtn.GetComponentInChildren<TMP_Text>().text = "[ ПОДАТЬ ]";
        serveBtn.onClick.AddListener(ServeDrink);
    }

    void AddIngredient(string ingredient)
    {
        selectedIngredients.Add(ingredient);
        feedbackText.text = "Добавлено: " + string.Join(", ", selectedIngredients);
    }

    public void ServeDrink()
    {
        if (currentOrderIndex >= orders.Length) return;
        var order = orders[currentOrderIndex];

        bool success = CheckOrder(order);

        if (success && order.successDialogue != null && dialogueManager != null)
            dialogueManager.StartDialogue(order.successDialogue);
        else if (!success && order.failDialogue != null && dialogueManager != null)
            dialogueManager.StartDialogue(order.failDialogue);

        currentOrderIndex++;
        Invoke(nameof(ShowCurrentOrder), 2f);
    }

    bool CheckOrder(CoffeeOrderData order)
    {
        if (order.requiredIngredients == null) return true;

        foreach (var req in order.requiredIngredients)
        {
            if (!selectedIngredients.Contains(req))
                return false;
        }
        return true;
    }

    void EndMinigame()
    {
        feedbackText.text = "Смена окончена!";
        StartCoroutine(EndRoutine());
    }

    System.Collections.IEnumerator EndRoutine()
    {
        SaveManager.Save();
        if (fadeController != null)
        {
            fadeController.FadeOut(0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
