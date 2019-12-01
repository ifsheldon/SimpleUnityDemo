using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// The class for handling actions on the Quit button in Main Scene
/// </summary>
public class EndGameButton : MonoBehaviour
{
    private EventsManager eventsManager;

    private UnityEvent<int> exit;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetUIEventsManager();
        exit = new ExitGameEvent();
        eventsManager.AddEvent(EventType.ExitGameEvent, exit);
    }

    public void ButtonClick()
    {
        exit.Invoke(EventsManager.UNUSED);
    }
}