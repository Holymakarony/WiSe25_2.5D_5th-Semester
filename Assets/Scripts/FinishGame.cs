using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] GameObject FinishPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MovePlayerToFinish()
    {
        GameObject.FindWithTag("Player").transform.position = FinishPosition.transform.position;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
