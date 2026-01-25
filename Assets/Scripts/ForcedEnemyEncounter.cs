using UnityEngine;

public class ForcedEnemyEncounter : MonoBehaviour
{
    [SerializeField] private Encounter[] enemyEncounter;
    [SerializeField] private int maxAmountEnemies;
    [SerializeField] private GameObject interactPrompt;

    private EnemeyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>().CompletedForcedEncounters.Contains(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateForcedEncounter()
    {
        GameObject.FindFirstObjectByType<GameManager>().currentForcedEncounter = gameObject.name;
        enemyManager = GameObject.FindFirstObjectByType<EnemeyManager>();
        enemyManager.GenerateFixedEnemyEncounter(enemyEncounter, maxAmountEnemies);
        FindFirstObjectByType<CS_PlayerController>().LoadBattleScene();
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
    }
}
