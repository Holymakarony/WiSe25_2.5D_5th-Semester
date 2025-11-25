using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject joinPopUp;
    [SerializeField] private TextMeshProUGUI joinPopUpText;
    
    private bool infrontOfPartyMember;
    [AllowNull]private GameObject joinableMember; // idk ob das [AllowNull] wichtig ist, maybe mal Marvin oder Kamil fragen, braucht zum funktionieren: using System.Diagnostics.CodeAnalysis; !!!
    private PlayerControls playerControls;

    private const string PARTY_JOINED_MESSAGE = " joined the Party!";
    private const string NPC_JOINABLE_TAG = "NPCJoinable";
    private const string NPC_DIALOGUE = "NPCDialogue";

    // Dialogue Prototype
    private bool infrontOfDialogue;
    private GameObject dialogueMember;
    
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.Player.Interact.performed += _ => Interact();
    }
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Interact()
    {
        if (infrontOfPartyMember == true && joinableMember != null)
        {
            MemberJoined(joinableMember.GetComponent<JoinableCharacter>().MemberToJoin); // add member
            infrontOfPartyMember = false;
            joinableMember = null;

        }
        // Dialoge Prototype
        else if (infrontOfDialogue == true)
        {
            dialogueMember.GetComponent<DialoguePlayer>().PlayDialogue();
        }
    }

    private void MemberJoined(PartyMemberInfo partyMember)
    {
        GameObject.FindFirstObjectByType<PartyManager>().AddMemberToPartyByName(partyMember.MemberName); // add party member
        joinableMember.GetComponent<JoinableCharacter>().CheckIfJoined(); // disable joinable member overworld object
        // join pop up
        joinPopUp.SetActive(true);
        joinPopUpText.text = partyMember.MemberName + PARTY_JOINED_MESSAGE;
        // add overworld follow member
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            // enable Interact Prompt
            infrontOfPartyMember = true;
            joinableMember = other.gameObject;
            joinableMember.GetComponent<JoinableCharacter>().ShowInteractPrompt(true);
        }
        // Dialogue Prototype
        else if (other.gameObject.tag == NPC_DIALOGUE)
        {
            infrontOfDialogue = true;
            dialogueMember = other.gameObject;
            dialogueMember.GetComponent<DialoguePlayer>().ShowInteractPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            // disable Interact Prompt
            infrontOfPartyMember = false;
            joinableMember.GetComponent<JoinableCharacter>().ShowInteractPrompt(false);
            joinableMember = null;
        }
        // Dialogue Prototype
        else if (other.gameObject.tag == NPC_DIALOGUE)
        {
            infrontOfDialogue = false;
            other.gameObject.GetComponent<DialoguePlayer>().ShowInteractPrompt(false);
            dialogueMember = null;
        }
    }

    
}
