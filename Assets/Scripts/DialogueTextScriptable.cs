using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "New Dialogue Text")]
public class DialogueText : ScriptableObject
{
    public string SpeakerName;
    public Sprite speakerSprite;
    public bool DisplayLeft;
    public string Text;
    public Quest quest;
}
