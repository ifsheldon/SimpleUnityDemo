using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResumeButton : MonoBehaviour
{
    private UnityEvent resumeEvent;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager gameEventManager = EasyGetter.GetGameEventManager();
        resumeEvent = new EscHit();
        gameEventManager.AddEvent(GameEventType.EscHit,resumeEvent);

    }

    public void click()
    {
        resumeEvent.Invoke();
    }
}
