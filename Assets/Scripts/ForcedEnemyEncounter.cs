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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateForcedEncounter()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemeyManager>();
        enemyManager.GenerateEnemiesByEncounter(enemyEncounter, maxAmountEnemies);
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
    }
}
