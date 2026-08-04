using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Liminal/Coffee Order")]
public class CoffeeOrderData : ScriptableObject
{
    public string customerName;
    public string drinkName;

    [Tooltip("Обязательные ингредиенты: cup_small, cup_large, espresso, milk_cow, milk_oat, syrup_vanilla...")]
    public string[] requiredIngredients;

    public bool needsIce;

    [TextArea(2, 4)]
    public string orderDescription;

    [Header("Реакции")]
    public DialogueData successDialogue;
    public DialogueData failDialogue;
}
