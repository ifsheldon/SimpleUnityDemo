using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains event handlers for events in the Playing Scene
/// </summary>
public class EventHandler : MonoBehaviour
{
    private EventsManager uiEventsManager;
    private BoxCleaner boxCleaner;
    private GameObject globalManager;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = EasyGetter.GetGlobalManager();
        boxCleaner = globalManager.GetComponent<BoxCleaner>();
        uiEventsManager = globalManager.GetComponent<EventsManager>();
        uiEventsManager.AddListener(EventType.GoBackMainEvent, HandleGoBackMainEvent);
    }

    public void HandleGoBackMainEvent(int i)
    {
        boxCleaner.cleanAllBoxes();
        SceneManager.LoadScene(SceneNames.Main.ToString());
    }
}