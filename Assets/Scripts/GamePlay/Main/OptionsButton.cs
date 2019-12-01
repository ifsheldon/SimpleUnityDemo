using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsButton : MonoBehaviour
{
    private EventsManager eventsManager;

    private UnityEvent<int> goToOptions;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetUIEventsManager();
        goToOptions = new PauseEvent();
        eventsManager.AddEvent(EventType.PauseGameEvent, goToOptions);
    }

    public void ButtonClick()
    {
        goToOptions.Invoke(EventsManager.UNUSED);
    }
}