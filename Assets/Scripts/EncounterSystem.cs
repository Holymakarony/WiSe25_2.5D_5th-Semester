using System;
using UnityEngine;

public class EncounterSystem : MonoBehaviour
{
    [SerializeField] private Encounter[] enemiesInScene;
    [SerializeField] private int maxNumEnemies;

    private EnemeyManager enemyManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemeyManager>();
        enemyManager.GenerateEnemiesByEncounter(enemiesInScene, maxNumEnemies);
        // on scene change from overworld to battle need to disable playercontroller(memory leak issue), enable on change from battle to overworld!!
    }
}

[System.Serializable]

public class Encounter
{
    public EnemyInfo Enemy;
    public int LevelMin;
    public int LevelMax;
}