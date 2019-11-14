using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class contains event handlers for events in the Playing Scene
/// </summary>
public class EventHandler : MonoBehaviour
{
    private EventsManager eventsManager;
    private Actioner actioner;
    private BoxCleaner boxCleaner;
    private GameObject globalManager;
    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GameController");
        actioner = globalManager.GetComponent<Actioner>();
        boxCleaner = globalManager.GetComponent<BoxCleaner>();
        eventsManager = globalManager.GetComponent<EventsManager>();
        eventsManager.AddListener(EventType.GoBackMainEvent,HandleGoBackMainEvent);
    }

    public void HandleGoBackMainEvent(int i)
    {
        boxCleaner.cleanAllBoxes();
        actioner.stopAction();
        SceneManager.LoadScene(SceneNames.Main.ToString());
    }

    public void HandlePauseEvent(int i)
    {
        ///
        /// need to implement
        /// 
    }
}
