using UnityEngine;
using UnityEngine.UI;

public class SpeakerImageScript : MonoBehaviour
{
    [SerializeField] private Image speakerImageLeft;
    [SerializeField] private Image speakerImageRight;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void changeLeftSpeakerImage(Sprite image)
    {
        speakerImageLeft.sprite = image;
    }

    public void changeRightSpeakerImage(Sprite image)
    {
        speakerImageRight.sprite = image;
    }
}
