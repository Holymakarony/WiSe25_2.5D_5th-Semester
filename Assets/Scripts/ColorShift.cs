using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorShift : MonoBehaviour
{
    public static ColorShift Instance { get; private set; }

    public AudioClip Door;
    public AudioClip Thunder;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private Component GlobalVolume;
    private ColorAdjustments _colorAdjustments;
    private float StartSaturation = -80f;
    private float currentSaturation;
    private Animator anim;

    private const string PLAY_START_PARAM = "PlayStart";

    // start anim
    public float changePerSecond = 1f;
    public bool playAnim = false;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out _colorAdjustments);
        currentSaturation = _colorAdjustments.saturation.value;
    }

    public void Update()
    {
        if (playAnim && currentSaturation > -80f)
        {
            _colorAdjustments.saturation.Override(currentSaturation - changePerSecond * Time.deltaTime);
            currentSaturation = _colorAdjustments.saturation.value;
        }
        else if (currentSaturation <= -80f)
        {
            playAnim = false; 
        }
    }

    public void UpdateSaturation()
    {
        currentSaturation = _colorAdjustments.saturation.value;
        // GameObject.FindFirstObjectByType<PartyManager>().savedSaturation = currentSaturation + 66f;
        _colorAdjustments.saturation.Override(currentSaturation + 60f);
        
    }

    public void StartAnimation()
    {
        GameObject.FindWithTag("Player").GetComponent<CS_PlayerController>().SetCanMove(false);
        playAnim = true;
    }

    public void StopAnimation()
    {
        GameObject.FindWithTag("Player").GetComponent<CS_PlayerController>().SetCanMove(true);
    }

    public void PlaySound()
    {
        // hier tür sound und explosion oder so idk
    }

    public void PlayStartAnimation()
    {
        anim.SetTrigger(PLAY_START_PARAM);
    }

    public void PlayDoor()
    {
        _audioSource.PlayOneShot(Door);
    }

    public void PlayThunder()
    {
        _audioSource.PlayOneShot(Thunder);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
