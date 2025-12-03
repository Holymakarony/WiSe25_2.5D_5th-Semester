using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject OptionsMenuUI;
    public GameObject PauseMenuUI;
    public GameObject MainMenuUI;

    public Button BackButton;

    public Slider MasterSlider;
    public Slider SFXSlider;
    public Slider MusicSlider;

    public string LastMenu;

    void Start()
    {
        OptionsMenuUI.SetActive(false);

        BackButton.onClick.AddListener(OnBackButtonClicked);
    }


    public void OnBackButtonClicked()
    {
        

        //if (LastMenu == "PAUSE_MENU")
        //{
        //    Debug.Log("PauseMenu");
        //    PauseMenuUI.SetActive(true);
        //    OptionsMenuUI.SetActive(false);
        //}
        //else if (LastMenu == "MAIN_MENU")
        //{
        //    Debug.Log("MainMenu");
        //    MainMenuUI.SetActive(true);
        //    OptionsMenuUI.SetActive(false);
        //}
    }
}
