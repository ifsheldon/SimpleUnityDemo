using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The class handling actions on the Quit button in the playing scene
/// </summary>
public class QuitButton : MonoBehaviour
{
    private UnityEvent<int> quit;
    private EventsManager eventsManager;

    // Start is called before the first frame update
    void Start()
    {
        quit = new GoBackMainEvent();
        eventsManager = EasyGetter.GetUIEventsManager();
        eventsManager.AddEvent(EventType.GoBackMainEvent, quit);
    }

    public void ButtonClick()
    {
        Debug.Log("Test Codes Here");
        ConfigManager.Configuration.chapter += 1;
        quit.Invoke(0);
    }
}