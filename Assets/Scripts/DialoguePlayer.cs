using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] public string NPCName;
    [SerializeField] private GameObject dialoguePopUp;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerLeft;
    [SerializeField] private TextMeshProUGUI speakerRight;
    [SerializeField] private List<DialogueText> dialogueTexts;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject popUpLeft;
    [SerializeField] private GameObject popUpRight;
    [SerializeField] private GameObject gameManager;

    private bool dialogueIsPlaying;
    private int dialogueIndex = 0; 
    
    public bool canDisplayDialogue = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue()
    {
        if (dialogueIsPlaying != true)
        {
            dialogueIsPlaying = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CS_PlayerController>().SetCanMove(false);
            dialogueIndex = 0;
            DisplayDialogue();
        }
        else if (dialogueIsPlaying = true && dialogueIndex < dialogueTexts.Count -1)
        {
            dialogueIndex ++;
            DisplayDialogue();
            //if (dialogueTexts[dialogueIndex].quest)
            //{
            //    FindFirstObjectByType<QuestManager>().AddNewQuest(dialogueTexts[dialogueIndex].quest);
            //}
        }
        else if (dialogueIsPlaying = true && dialogueIndex >= dialogueTexts.Count -1)
        {
            dialoguePopUp.SetActive(false);
            dialogueIsPlaying = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CS_PlayerController>().SetCanMove(true);
            
            if (dialogueTexts[dialogueIndex].quest)
            {
                FindFirstObjectByType<QuestManager>().AddNewQuest(dialogueTexts[dialogueIndex].quest);
            }
            FindFirstObjectByType<QuestManager>().CheckQuestStatus(NPCName);
            canDisplayDialogue = false;
            gameManager.GetComponent<GameManager>().DialogueCompleted(gameObject.name);
        }
    }

    public void DisplayDialogue()
    {
        dialoguePopUp.SetActive(true);
            dialogueText.SetText(dialogueTexts[dialogueIndex].Text);
            if (dialogueTexts[dialogueIndex].DisplayLeft == true)
            {
                popUpLeft.SetActive(true);
                speakerLeft.SetText(dialogueTexts[dialogueIndex].SpeakerName);
                popUpRight.SetActive(false);
            }
            else
            {
                popUpRight.SetActive(true);
                speakerRight.SetText(dialogueTexts[dialogueIndex].SpeakerName);
                popUpLeft.SetActive(false);
            }
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
        if (GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>().CompletedDialoguePlayerNames.Contains(gameObject.name))
        {
            canDisplayDialogue = false;
            interactPrompt.SetActive(false);
        }
        else if (canDisplayDialogue == true)
        {
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
        }
    }
}
