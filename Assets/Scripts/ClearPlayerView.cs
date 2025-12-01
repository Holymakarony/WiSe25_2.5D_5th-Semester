using UnityEngine;

public class ClearPlayerView : MonoBehaviour
{
    [SerializeField] private Color seeThroughColor;
    [SerializeField] private Color normalColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Hideable")
        {
            print("enter");
            other.gameObject.GetComponent<SpriteRenderer>().color = seeThroughColor;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Hideable")
        {
            print("exit");
            other.gameObject.GetComponent<SpriteRenderer>().color = normalColor;
        }
    }
}
