using UnityEngine;

[CreateAssetMenu(fileName = "Contact", menuName = "Liminal/Phone/Contact")]
public class PhoneContactData : ScriptableObject
{
    public string contactName;
    [TextArea(2, 4)] public string description;
}
