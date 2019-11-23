using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class contains event handlers for events happening in the Main Scene
/// </summary>
public class UIEventHandler : MonoBehaviour
{
    private EventsManager eventsManager;
    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetUIEventsManager();
        eventsManager.AddListener(EventType.GameStartedEvent, HandleStartEvent);
        eventsManager.AddListener(EventType.ExitGameEvent, HandleExitEvent);
    }
    public void HandleStartEvent(int i)
    {
        SceneManager.LoadScene(SceneNames.Playing.ToString());
    }

    public void HandleExitEvent(int i)
    {
        Debug.Log("Config not saved because under development");
//        ConfigManager.save();
        print("Application Quit");
        Application.Quit();
    }


}
