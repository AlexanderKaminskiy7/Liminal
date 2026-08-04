using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Liminal/Phone/Message")]
public class PhoneMessageData : ScriptableObject
{
    public string sender;
    [TextArea(3, 6)] public string messageText;
    public bool isRead;
}
