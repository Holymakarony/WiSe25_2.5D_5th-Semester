using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public GameObject joinPopUp;
    [SerializeField] public TextMeshProUGUI joinPopUpText;
    
    private bool infrontOfPartyMember;
    [AllowNull]private GameObject joinableMember; // idk ob das [AllowNull] wichtig ist, maybe mal Marvin oder Kamil fragen, braucht zum funktionieren: using System.Diagnostics.CodeAnalysis; !!!
    private PlayerControls playerControls;
    private List<GameObject> overWorldCharacters = new List<GameObject>();

    private const string PARTY_JOINED_MESSAGE = " joined the Party!";
    private const string NPC_JOINABLE_TAG = "NPCJoinable";
    private const string NPC_DIALOGUE = "NPCDialogue";

    // Dialogue Prototype
    private bool infrontOfDialogue;
    [AllowNull]private GameObject dialogueMember;

    private bool infrontOfHouse;
    
    // Forced Enemy Encounter Prototype
    private bool infrontOfEnemy;
    [AllowNull]private GameObject forcedEnemy;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls.Player.Interact.performed += _ => Interact();
        SpawnOverworldMembers();
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
        else if (infrontOfEnemy == true)
        {
            forcedEnemy.GetComponent<ForcedEnemyEncounter>().generateForcedEncounter();
            FindFirstObjectByType<CS_PlayerController>().LoadBattleScene();
        }
        else if (infrontOfHouse == true)
        {
            FindFirstObjectByType<CS_PlayerController>().LoadGretelHouse();
        }
    }

    private void MemberJoined(PartyMemberInfo partyMember)
    {
        GameObject.FindFirstObjectByType<PartyManager>().AddMemberToPartyByName(partyMember.MemberName); // add party member
        joinableMember.GetComponent<JoinableCharacter>().CheckIfJoined(); // disable joinable member overworld object
        // join pop up
        joinPopUp.SetActive(true);
        joinPopUpText.text = partyMember.MemberName + PARTY_JOINED_MESSAGE;
        SpawnOverworldMembers(); // add overworld follow member
    }

    private void SpawnOverworldMembers()
    {
        for (int i = 0; i < overWorldCharacters.Count; i++)
        {
            Destroy(overWorldCharacters[i]);
        }
        overWorldCharacters.Clear();

        List<PartyMember> currentParty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();

        for (int i = 0; i < currentParty.Count; i++)
        {
            if(i == 0) // first member will be the player
            {
                GameObject player = gameObject; // get the player
                GameObject playerVisual = Instantiate(currentParty[i].MemberOverworldVisualPrefab, player.transform.position, Quaternion.identity); // spawn the member visual
                
                playerVisual.transform.SetParent(player.transform);
                
                player.GetComponent<CS_PlayerController>().SetOverworldVisuals(playerVisual.GetComponent<Animator>(), playerVisual.GetComponent<SpriteRenderer>());// assign the player controller values
                playerVisual.GetComponent<MemberFollowAI>().enabled = false;
                overWorldCharacters.Add(playerVisual);// add the overworld character visual to the list
                
                // Clear Completed Quests on scene change
                FindFirstObjectByType<QuestManager>().ClearQuestQueue();
            }
            else // any other will be a follower
            {
                Vector3 positionToSpawn = transform.position; // get the follower spawn position
                positionToSpawn.x -= 1;
                
                GameObject tempFollower = Instantiate(currentParty[i].MemberOverworldVisualPrefab, positionToSpawn, Quaternion.identity); // spawn follower

                tempFollower.GetComponent<MemberFollowAI>().SetFollowDistance(i); // set follow ai settings
                overWorldCharacters.Add(tempFollower); // add follow visual to list
            }
        }
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

        else if(other.gameObject.tag == "PickUp")
        {
            
            GameObject.FindFirstObjectByType<Volume>().GetComponent<ColorShift>().UpdateSaturation();
            FindFirstObjectByType<QuestManager>().CheckQuestStatus(other.gameObject.name);
            Destroy(other.gameObject);
        }
        // Forced Enemy Prototype
        else if(other.gameObject.tag == "ForcedEnemy")
        {
            infrontOfEnemy = true;
            forcedEnemy = other.gameObject;
            forcedEnemy.GetComponent<ForcedEnemyEncounter>().ShowInteractPrompt(true);
        }

        else if (other.gameObject.tag == "House")
        {
            infrontOfHouse = true;
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
        // Forced Enemy Prototype
        else if(other.gameObject.tag == "ForcedEnemy")
        {
            infrontOfEnemy = false;
            forcedEnemy.GetComponent<ForcedEnemyEncounter>().ShowInteractPrompt(false);
            forcedEnemy = null;
        }

        else if (other.gameObject.tag == "House")
        {
            infrontOfHouse = false;
        }
    }
}
