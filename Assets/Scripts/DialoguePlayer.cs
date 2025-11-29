using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePopUp;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerLeft;
    [SerializeField] private TextMeshProUGUI speakerRight;
    [SerializeField] private List<DialogueText> dialogueTexts;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject popUpLeft;
    [SerializeField] private GameObject popUpRight;

    private bool dialogueIsPlaying;
    private int dialogueIndex = 0; 
    
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
            dialogueIndex = 0;
            DisplayDialogue();
        }
        else if (dialogueIsPlaying = true && dialogueIndex < dialogueTexts.Count -1)
        {
            dialogueIndex ++;
            DisplayDialogue();
        }
        else if (dialogueIsPlaying = true && dialogueIndex >= dialogueTexts.Count -1)
        {
            dialoguePopUp.SetActive(false);
            dialogueIsPlaying = false;
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
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
    }
}
