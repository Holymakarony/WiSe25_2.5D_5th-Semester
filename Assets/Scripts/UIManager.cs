using UnityEngine;

public class UIManager : MonoBehaviour
{

    public MenuType CurrentMenu;
    public enum MenuType
    {
        MainMenuUI,
        PauseMenuUI,
        OptionsMenuUI,
        CreditsMenuUI
    }

    public GameObject MainMenuUI;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject CreditsMenuUI;


    public void ShowMenu(MenuType menu)
    {
        CurrentMenu = menu;

        MainMenuUI.SetActive(menu == MenuType.MainMenuUI);
        PauseMenuUI.SetActive(menu == MenuType.PauseMenuUI);
        OptionsMenuUI.SetActive(menu == MenuType.OptionsMenuUI);
        CreditsMenuUI.SetActive(menu == MenuType.CreditsMenuUI);
    }
}
