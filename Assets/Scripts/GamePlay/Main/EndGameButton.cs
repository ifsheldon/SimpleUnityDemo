using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameButton : MonoBehaviour
{
    private EventsManager eventsManager;

    private UnityEvent<int> exit;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetEventsManager();
        exit = new ExitGameEvent();
        eventsManager.AddEvent(EventType.ExitGameEvent, exit);
    }

    public void ButtonClick()
    {
        exit.Invoke(0);
    }
}