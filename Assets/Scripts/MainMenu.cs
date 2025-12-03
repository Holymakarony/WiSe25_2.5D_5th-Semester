using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public UIManager UIManager;

    public GameObject MainMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject CreditsMenuUI;

    public Button StartGameButton;
    public Button OptionsButton;
    public Button CreditsButton;
    public Button QuitGameButton;

    private const string MAIN_MENU = "MainMenu";

    private void Start()
    {
        Time.timeScale = 0f;

        StartGameButton.onClick.AddListener(OnStartClicked);
        OptionsButton.onClick.AddListener(OnOptionsClicked);
        CreditsButton.onClick.AddListener(OnCreditsClicked);
        QuitGameButton.onClick.AddListener(OnQuitGameClicked);
    }

    private void OnStartClicked()
    {
        MainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnOptionsClicked()
    {
        UIManager.ShowMenu(UIManager.MenuType.OptionsMenuUI);

        //MainMenuUI.SetActive(false);
        //OptionsMenuUI.SetActive(true);
        //OptionsMenuUI.GetComponent<OptionsMenu>().LastMenu = MAIN_MENU;
    }

    private void OnCreditsClicked()
    { 
        UIManager.ShowMenu(UIManager.MenuType.CreditsMenuUI);

        //MainMenuUI.SetActive(false);
        //CreditsMenuUI.SetActive(true);
    }

    private void OnQuitGameClicked()
    { 
        Application.Quit();
    }

}
