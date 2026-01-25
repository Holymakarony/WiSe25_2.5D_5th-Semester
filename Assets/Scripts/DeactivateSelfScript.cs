using UnityEngine;

public class DeactivateSelfScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindFirstObjectByType<GameManager>().DeactivatedBarriers.Contains(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateSelf()
    {
        GameObject.FindFirstObjectByType<GameManager>().DeactivateBarrier(gameObject.name);

        gameObject.SetActive(false);
    }
}
