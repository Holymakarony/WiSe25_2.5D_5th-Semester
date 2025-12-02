using UnityEngine;


[CreateAssetMenu(menuName = "New Quest")]
public class Quest : ScriptableObject
{
    public string Title = "null";
    public enum Type{Fetch, Kill, Talk};
    public Type QuestType;
    public string Desc = "null";
    public string Target = "null";
    public int currentAmount = 0;
    public int targetAmount;
}
