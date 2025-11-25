using UnityEngine;

[CreateAssetMenu(menuName = "New Dialogue Text")]
public class DialogueText : ScriptableObject
{
    public string SpeakerName;
    public bool DisplayLeft;
    public string Text;
}
