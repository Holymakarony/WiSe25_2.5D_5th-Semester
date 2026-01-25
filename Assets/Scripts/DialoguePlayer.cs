using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] public string NPCName;
    [SerializeField] private TypeWriterEffect typeWriter;
    [SerializeField] private GameObject dialoguePopUp;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerLeft;
    [SerializeField] private TextMeshProUGUI speakerRight;
    [SerializeField] private List<DialogueText> dialogueTexts;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject popUpLeft;
    [SerializeField] private GameObject popUpRight;
    [SerializeField] private GameObject gameManager;

    public bool dialogueIsPlaying;
    private int dialogueIndex = 0; 
    
    public bool canDisplayDialogue = true;
    public float WaitForSeconds = 1.5f;

    public UnityEvent dialogueEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
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
        else if (dialogueIsPlaying == true)
        {
            if (typeWriter.IsTyping)
            {
                typeWriter.Skip();
                return;
            }
            if (dialogueIndex < dialogueTexts.Count - 1)
            {
                dialogueIndex++;
                DisplayDialogue();
            }
            else
            {
                EndDialoge();
            }
        }
    }
    private void EndDialoge()
    { 
        dialoguePopUp.SetActive(false);
        dialogueIsPlaying = false; 

        GameObject.FindFirstObjectByType<CharacterManager>().ResetDialogueReferences();

        GameObject.FindGameObjectWithTag("Player")
            .GetComponent<CS_PlayerController>()
            .SetCanMove(true);

        if (dialogueTexts[dialogueIndex].quest)
        {
            FindFirstObjectByType<QuestManager>()
                .AddNewQuest(dialogueTexts[dialogueIndex].quest, WaitForSeconds);
        }

        FindFirstObjectByType<QuestManager>()
            .CheckQuestStatus(NPCName);

        canDisplayDialogue = false;

        if (interactPrompt)
            {interactPrompt.SetActive(false);}

        gameManager.GetComponent<GameManager>()
            .DialogueCompleted(gameObject.name);
        
        dialogueEvent.Invoke();
    }

    public void DisplayDialogue()
    {
        dialoguePopUp.SetActive(true);

        typeWriter.Play(dialogueTexts[dialogueIndex].Text);

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

    public void AddNewQuest(Quest quest)
    {
        FindFirstObjectByType<QuestManager>()
                .AddNewQuest(quest, WaitForSeconds);
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
        if (!GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>().CompletedDialoguePlayerNames.Contains(gameObject.name))
        {
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
        }
        if (GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>().CompletedDialoguePlayerNames.Contains(gameObject.name))
        {
            canDisplayDialogue = false;
            interactPrompt.SetActive(false);
        }
    }
}

