using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class PickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter()
    {
        if (CompareTag("Player"))
        {
            //ColorShift.Instance.UpdateSaturation;
            GameObject.FindFirstObjectByType<Volume>().GetComponent<ColorShift>().UpdateSaturation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
