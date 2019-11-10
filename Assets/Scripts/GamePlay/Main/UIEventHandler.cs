using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventHandler : MonoBehaviour
{
    private EventsManager eventsManager;
    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetEventsManager();
        eventsManager.AddListener(EventType.GameStartedEvent, HandleStartEvent);
        eventsManager.AddListener(EventType.ExitGameEvent, HandleExitEvent);
    }
    public void HandleStartEvent(int i)
    {
        SceneManager.LoadScene(SceneNames.Playing.ToString());
    }

    public void HandleExitEvent(int i)
    {
        Application.Quit();
    }


}
