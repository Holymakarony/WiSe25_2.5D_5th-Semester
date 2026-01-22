using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] public AudioMixer mixer;

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
        QuitButton.onClick.AddListener(OnQuitClicked);

        if(questManager.GetComponent<QuestManager>().showMainMenu == false)
        {
            OnStartClicked();
        }

        ApplyVolume(PlayerPrefs.GetFloat("Master", 0f), PlayerPrefs.GetFloat("SFX", 0f), PlayerPrefs.GetFloat("Music", 0f));
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

    void ApplyVolume(float master, float sfx, float music)
    {
        mixer.SetFloat("MasterVolume", master);
        mixer.SetFloat("SFXVolume", sfx);
        mixer.SetFloat("MusicVolume", music);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
