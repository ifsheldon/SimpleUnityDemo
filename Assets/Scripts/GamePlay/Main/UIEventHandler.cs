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
        eventsManager.AddListener(EventType.ChangeSceneEvent, ChangeScene);
        eventsManager.AddListener(EventType.ExitGameEvent, HandleExitEvent);
        eventsManager.AddListener(EventType.PauseGameEvent, goToOptions);
    }

    public void goToOptions(int i)
    {
        //need to implement
        Debug.Log("Go to options");
    }

    public void ChangeScene(int i)
    {
        SceneNames scene = (SceneNames) i;
        SceneManager.LoadScene(scene.ToString());
    }

    public void HandleExitEvent(int i)
    {
        Debug.Log("Config not saved because under development");
//        ConfigManager.save();
        print("Application Quit");
        Application.Quit();
    }
}