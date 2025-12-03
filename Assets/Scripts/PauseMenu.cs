using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
     public bool GameIsPaused = false;
     public GameObject PauseMenuUI;

    private PlayerControls _playerControls;

    private void Start()
    {
        PauseMenuUI.SetActive(false);    
    }

    void Update()
    {
        
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

    //public void TogglePauseMenu()
    //{
        
    //}

    public void BackToMain()
    {
        PauseMenuUI.SetActive(false);
        //MainMenuUI.SetActive(true);
    }
}
