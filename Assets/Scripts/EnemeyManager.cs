using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour
{
    [SerializeField] private EnemyInfo[] allEnemies;
    [SerializeField] private List<Enemy> currentEnemies;

    private const float LEVEL_MODIFIER = 0.5f; 
    
    private void Awake() 
    {
        GenerateEnemyByName("Slime", 10);
    }

    private void GenerateEnemyByName(string enemyName, int level)
    {
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (enemyName == allEnemies[i].EnemyName)
            {
                Enemy newEnemy = new Enemy();

                newEnemy.EnemyName = allEnemies[i].EnemyName;
                newEnemy.Level = level;
                float levelModifier = (LEVEL_MODIFIER * newEnemy.Level);

                newEnemy.MaxHealth = Mathf.RoundToInt(allEnemies[i].BaseHealth + (allEnemies[i].BaseHealth * levelModifier));
                newEnemy.CurrentHealth = newEnemy.MaxHealth;
                newEnemy.Strenght = Mathf.RoundToInt(allEnemies[i].BaseStr + (allEnemies[i].BaseStr * levelModifier));
                newEnemy.Initiative = Mathf.RoundToInt(allEnemies[i].BaseInitiative + (allEnemies[i].BaseHealth * levelModifier));
                newEnemy.EnemyBattleVisualPrefab = allEnemies[i].EnemyVisualBattlePrefab;

                currentEnemies.Add(newEnemy);
            }
        }
    }
}

[System.Serializable]
public class Enemy
{
    public string EnemyName;
    public int Level;
    public int CurrentHealth;
    public int MaxHealth;
    public int Strenght;
    public int Initiative;
    public GameObject EnemyBattleVisualPrefab; // used in battle scene, needs to be adjusted/expanded for enemys standing around in overworld scene!!!
}
