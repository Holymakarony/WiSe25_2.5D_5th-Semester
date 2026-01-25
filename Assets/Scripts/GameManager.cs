using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<String> CompletedDialoguePlayerNames;
    [SerializeField] public List<String> CompletedForcedEncounters;
    [SerializeField] public List<String> DeactivatedBarriers;

    private static GameObject instance;

    [AllowNull]public string currentForcedEncounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ForcedEnounterCompleted(String EncounterName)
    {
        CompletedForcedEncounters.Add(EncounterName);
    }

    public void DialogueCompleted(String DialoguePlayerName)
    {
        CompletedDialoguePlayerNames.Add(DialoguePlayerName);
    }

    public void RemoveCompletedDialogue(String DialoguePlayerName)
    {
        CompletedDialoguePlayerNames.Remove(DialoguePlayerName);
    }

    public void DeactivateBarrier(String Barrier)
    {
        DeactivatedBarriers.Add(Barrier);
    }
}
