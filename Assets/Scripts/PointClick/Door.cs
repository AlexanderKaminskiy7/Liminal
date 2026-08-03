using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[AddComponentMenu("Liminal/Door")]
public class Door : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private bool canEnter = true;
    [SerializeField] private UnityEvent onBlocked = new UnityEvent();

    // Метод для привязки к Interactable.OnInteract
    public void Interact()
    {
        if (canEnter)
        {
            if (string.IsNullOrEmpty(targetScene))
            {
                Debug.LogWarning($"{name}: targetScene пустой. Смена сцены отменена.");
                onBlocked.Invoke();
                return;
            }

            SceneManager.LoadScene(targetScene);
            return;
        }

        onBlocked.Invoke();
    }
}
