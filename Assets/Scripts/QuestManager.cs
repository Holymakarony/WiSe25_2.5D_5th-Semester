using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> activeQuests;
    [SerializeField] private List<Quest> completedQuestQueue;
    [SerializeField] private GameObject PopUp;
    [SerializeField] private TextMeshProUGUI PopUpText;
    
    private const string ACTIVATED_MESSAGE = " activated";
    private const string COMPLETED_MESSAGE = " completed"; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckQuestStatus(string target)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if(activeQuests[i].Target == target)
            {
                if (activeQuests[i].Target == target && activeQuests[i].Amount < activeQuests[i].Amount - 1)
                {
                    activeQuests[i].Amount++;
                }
                else if(activeQuests[i].Target == target && activeQuests[i].Amount >= activeQuests[i].Amount - 1)
                {
                    QuestCompleted(activeQuests[i]);
                    activeQuests.Remove(activeQuests[i]);
                    if(activeQuests[i].QuestType == Quest.Type.Fetch)
                    {
                        ClearQuestQueue();
                    }
                }
            }
        }
    }
    
    public void AddNewQuest(Quest quest)
    {
        if(activeQuests.Count < 3)
        {
            activeQuests.Add(quest);
            // PopUp with Quest Title
            PopUp.SetActive(true);
            PopUpText.text = quest.Title + ACTIVATED_MESSAGE;
            // add quest popup
        }
    }

    public void ClearQuestQueue()
    {
            // reset the pop ups
            PopUp = GameObject.FindFirstObjectByType<CharacterManager>().joinPopUp;
            PopUpText = GameObject.FindFirstObjectByType<CharacterManager>().joinPopUpText;
            // Popup with Quest Title
            for (int i = 0; i < completedQuestQueue.Count; i++)
            {
                PopUp.SetActive(true);
                PopUpText.text = completedQuestQueue[i].Title + COMPLETED_MESSAGE;
                completedQuestQueue.Remove(completedQuestQueue[i]);
                // remove quest from list
            }
    }

    public void QuestCompleted(Quest quest)
    {
        completedQuestQueue.Add(quest);
    }

    
}
