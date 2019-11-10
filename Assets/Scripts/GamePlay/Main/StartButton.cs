using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartButton : MonoBehaviour
{
    

    private EventsManager eventsManager;

    private UnityEvent<int> start;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetEventsManager();
        start = new GameStartedEvent();
        eventsManager.AddEvent(EventType.GameStartedEvent, start);
    }

    public void ButtonClick()
    {
        start.Invoke(0);
    }
}