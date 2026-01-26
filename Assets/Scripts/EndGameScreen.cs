using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private Button BackToMainButton;
    [SerializeField] private GameObject MainMenuUI;
    void Start()
    {
        BackToMainButton.onClick.AddListener(OnBackToMainClicked);
    }

    private void OnAwake()
    {
        GameObject.FindWithTag("Player").GetComponent<CS_PlayerController>().SetCanMove(false);
    }

   private void OnBackToMainClicked()
    {
        gameObject.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
