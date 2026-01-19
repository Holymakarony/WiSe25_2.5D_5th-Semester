using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<String> CompletedDialoguePlayerNames;

    private static GameObject instance;

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

    public void DialogueCompleted(String DialoguePlayerName)
    {
        CompletedDialoguePlayerNames.Add(DialoguePlayerName);
    }
}
