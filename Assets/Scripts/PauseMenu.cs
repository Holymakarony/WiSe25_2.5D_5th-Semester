using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //public UIManager UIManager;


    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject MainMenuUI;
    public GameObject OptionsMenuUI;

    public Button ResumeButton;
    public Button OptionsButton;
    public Button BackToMainButton;

    private PlayerControls _playerControls;

    private const string PAUSE_MENU = "PauseMenu";

    private void Start()
    {
        gameObject.SetActive(false);

        ResumeButton.onClick.AddListener(OnResumeClicked);
        OptionsButton.onClick.AddListener(OnOptionsClicked);
        BackToMainButton.onClick.AddListener(OnBackToMainClicked);
    }


    public void Pause(bool isPause)
    {
        PauseMenuUI.SetActive(isPause);
        GameIsPaused = isPause;

        if (isPause )
        { 
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void OnResumeClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnOptionsClicked()
    {
        //UIManager.ShowMenu(UIManager.MenuType.OptionsMenuUI);

        gameObject.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }

    private void OnBackToMainClicked()
    {
        //UIManager.ShowMenu(UIManager.MenuType.MainMenuUI);

        gameObject.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
