using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;
    private bool doOnce;
    
    
    public void TriggerReaction()
    {
        if (!doOnce)
        {
            doOnce = true;
            triggerEvent.Invoke();
            print("Event triggered");
        }
    }
}
