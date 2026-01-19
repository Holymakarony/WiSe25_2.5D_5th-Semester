using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;
    private bool doOnce;
    
    
    public void TriggerReaction()
    {
        if (!doOnce && !GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>().CompletedDialoguePlayerNames.Contains(gameObject.name))
        {
            doOnce = true;
            triggerEvent.Invoke();
        }
    }
}
