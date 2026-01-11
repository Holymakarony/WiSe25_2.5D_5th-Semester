using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] private GameObject SunBeamAnimation;

    [SerializeField] private GameObject playerController;

    void Start()
    {
        gameObject.SetActive(true);
        //Time.timeScale = 0f;

        playerController.GetComponent<CS_PlayerController>().SetCanMove(false);

        StartButton.onClick.AddListener(OnStartClicked);
    }

    void Update()
    {
        
    }

    private void OnStartClicked()
    {
        //Time.timeScale = 1f;
        playerController.GetComponent<CS_PlayerController>().SetCanMove(true);
        gameObject.SetActive(false);
    }

    private void OnCreditsClicked()
    {
        //gameObject.SetActive (false);
        //CreditsMenu.SetActive(true);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
