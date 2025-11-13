using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string EnemyName;
    public int BaseHealth;
    public int BaseStr;
    public int BaseInitiative;
    public GameObject EnemyVisualBattlePrefab; // used in battle scene, needs to be adjusted/expanded for enemys standing around in overworld scene!!!

}
