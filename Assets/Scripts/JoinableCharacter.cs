using System.Collections.Generic;
using UnityEngine;

public class JoinableCharacter : MonoBehaviour
{
    public PartyMemberInfo MemberToJoin;
    [SerializeField] private GameObject interactPrompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckIfJoined();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
            interactPrompt.SetActive(showPrompt); // if bool showPrompt true = show, false = hide
    }

    public void CheckIfJoined()
    {
        List<PartyMember> currentParty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();

        for (int i = 0; i < currentParty.Count; i++)
        {
            if (currentParty[i].MemberName == MemberToJoin.MemberName)
            {
                gameObject.SetActive(false);
            }
            else 
            {
                gameObject.SetActive(true);
                FindFirstObjectByType<GameManager>().RemoveCompletedDialogue(gameObject.name);
            }
        }
    }
}
