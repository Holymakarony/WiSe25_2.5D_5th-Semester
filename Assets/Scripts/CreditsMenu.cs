using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private Button NancyCreditsButton;
    [SerializeField] private Button MaxCreditsButton;
    [SerializeField] private Button BackButton;

    void Start()
    {
        NancyCreditsButton.onClick.AddListener(OnNancyCreditsClicked);
        MaxCreditsButton.onClick.AddListener(OnMaxCreditsClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnNancyCreditsClicked()
    {
        Application.OpenURL("https://sparklecloud474.itch.io");
    }

    private void OnMaxCreditsClicked()
    {
        Application.OpenURL("https://holymakarony.itch.io");
    }
}
