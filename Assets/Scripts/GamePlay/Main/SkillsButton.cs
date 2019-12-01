using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillsButton : MonoBehaviour
{
    private EventsManager eventsManager;

    private UnityEvent<int> goToSkills;

    // Start is called before the first frame update
    void Start()
    {
        eventsManager = EasyGetter.GetUIEventsManager();
        goToSkills = new SceneChangeEvent();
        eventsManager.AddEvent(EventType.ChangeSceneEvent, goToSkills);
    }

    public void ButtonClick()
    {
        goToSkills.Invoke((int) SceneNames.Skills);
    }
}