using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorShift : MonoBehaviour
{
    public static ColorShift Instance { get; private set; }

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
    private float StartSaturation = -100f;
    private float currentSaturation;

    void Start()
    {
        GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out _colorAdjustments);
        _colorAdjustments.saturation.Override(-100f);
        currentSaturation = -100f;
    }

    public void UpdateSaturation()
    {
        currentSaturation = _colorAdjustments.saturation.value;
        // GameObject.FindFirstObjectByType<PartyManager>().savedSaturation = currentSaturation + 66f;
        _colorAdjustments.saturation.Override(currentSaturation + 66f);
        
    }
}
