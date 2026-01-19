using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] public AudioMixer mixer;

    [SerializeField] public Slider MasterSlider;
    [SerializeField] public Slider MusicSlider;
    [SerializeField] public Slider SFXSlider;

    [SerializeField] private Button BackButton;

    [SerializeField] private GameObject PauseMenuUI;

    const string MASTER_KEY = "Master";
    const string SFX_KEY = "SFX";
    const string MUSIC_KEY = "Music";

    void Start()
    {
       BackButton.onClick.AddListener(OnBackButtonClicked);
        
        MasterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY, 0f);
        SFXSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 0f);
        MusicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 0f);

        ApplyVolume(MasterSlider.value, SFXSlider.value, MusicSlider.value);
    }

    void ApplyVolume(float master, float sfx, float music)
    {
        mixer.SetFloat("MasterVolume", master);
        mixer.SetFloat("SFXVolume", sfx);
        mixer.SetFloat("MusicVolume", music);
    }

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat(MASTER_KEY, value);
    }

    public void SetSfxVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat(SFX_KEY, value);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat (MUSIC_KEY, value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void OnBackButtonClicked()
    {
        gameObject.SetActive(false);
        PauseMenuUI.SetActive(true);
    }
}
