using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuitButton : MonoBehaviour
{
    private UnityEvent<int> quit;
    private EventsManager eventsManager;

    // Start is called before the first frame update
    void Start()
    {
        quit = new GoBackMainEvent();
        eventsManager = EasyGetter.GetEventsManager();
        eventsManager.AddEvent(EventType.GoBackMainEvent,quit);
    }

    public void ButtonClick()
    {
        quit.Invoke(0);
    }
}