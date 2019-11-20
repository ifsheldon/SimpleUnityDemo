using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The class for handling actions on StartButton in Main Scene
/// </summary>
public class StartButton : MonoBehaviour
{
    

    private EventsManager eventsManager;

    private UnityEvent<int> start;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetUIEventsManager();
        start = new GameStartedEvent();
        eventsManager.AddEvent(EventType.GameStartedEvent, start);
    }

    public void ButtonClick()
    {
        start.Invoke(0);
    }
}