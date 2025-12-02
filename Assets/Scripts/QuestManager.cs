using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> activeQuests;
    [SerializeField] private List<Quest> completedQuestQueue;
    [SerializeField] private GameObject PopUp;
    [SerializeField] private TextMeshProUGUI PopUpText;
    [Header("Quest Description PopUp")]
    [SerializeField] private GameObject QuestCornerPopUp;
    [SerializeField] private TextMeshProUGUI QuestCornerTitle;
    [SerializeField] private TextMeshProUGUI QuestCornerDescription;
    
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
                if (activeQuests[i].Target == target && activeQuests[i].currentAmount < activeQuests[i].targetAmount - 1)
                {
                    activeQuests[i].currentAmount++;
                    print("Collectable aufgesammelt");
                }
                else if(activeQuests[i].Target == target && activeQuests[i].currentAmount >= activeQuests[i].targetAmount - 1)
                {
                    QuestCompleted(activeQuests[i]);
                    activeQuests.Remove(activeQuests[i]);
                    
                    

                    if(completedQuestQueue.Count > 0)
                    {
                        for (int f = 0; f < completedQuestQueue.Count; f++)
                        {
                            if (completedQuestQueue[f].QuestType == Quest.Type.Fetch || completedQuestQueue[f].QuestType == Quest.Type.Talk)
                            {
                                ClearQuestQueue();
                                UpdateCornerPopUp();
                            }
                        }
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
            quest.currentAmount = 0;
            // PopUp with Quest Title
            PopUp.SetActive(true);
            PopUpText.text = quest.Title + ACTIVATED_MESSAGE;
            // add quest popup
            QuestCornerPopUp.SetActive(true);
            QuestCornerTitle.text = quest.Title;
            QuestCornerDescription.text = quest.Desc;
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
    
    public void UpdateCornerPopUp()
    {
        QuestCornerPopUp = GameObject.FindFirstObjectByType<CharacterManager>().CornerPopUp;
        QuestCornerDescription = GameObject.FindFirstObjectByType<CharacterManager>().CornerPopUpText;
        QuestCornerTitle = GameObject.FindFirstObjectByType<CharacterManager>().CornerPopUpTitle;

        if(activeQuests.Count > 0)
        {
            QuestCornerTitle.text = activeQuests[0].Title;
            QuestCornerDescription.text = activeQuests[0].Desc;
        }
        else if(activeQuests.Count == 0)
        {
            QuestCornerPopUp.SetActive(false);
        }
    }
}
