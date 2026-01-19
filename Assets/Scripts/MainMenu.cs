using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] private GameObject SunBeamAnimation;

    [SerializeField] private GameObject CreditsMenuUI;

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject questManager; 

    void Start()
    {
        print("started");
        print(questManager.GetComponent<QuestManager>().showMainMenu);
        playerController.GetComponent<CS_PlayerController>().SetCanMove(false);

        StartButton.onClick.AddListener(OnStartClicked);
        CreditsButton.onClick.AddListener(OnCreditsClicked);

        if(questManager.GetComponent<QuestManager>().showMainMenu == false)
        {
            OnStartClicked();
        }
        
    }

    void Update()
    {
        
    }

    private void OnStartClicked()
    {
        playerController.GetComponent<CS_PlayerController>().SetCanMove(true);
        questManager.GetComponent<QuestManager>().showMainMenu = false;
        gameObject.SetActive(false);
    }

    private void OnCreditsClicked()
    {
        gameObject.SetActive(false);
        CreditsMenuUI.SetActive(true);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
