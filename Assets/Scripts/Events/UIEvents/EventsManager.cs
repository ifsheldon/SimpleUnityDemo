using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    Dictionary<EventType, List<UnityEvent<int>>> eventTypeDictionary =
        new Dictionary<EventType, List<UnityEvent<int>>>();

    Dictionary<EventType, List<UnityAction<int>>> listeners = new Dictionary<EventType, List<UnityAction<int>>>();

    void Awake()
    {
        // init Config manager, because Unity lazily init static classes
        Config c = ConfigManager.Configuration;
        // init self, register all eventTypes
        foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
        {
            if (!eventTypeDictionary.ContainsKey(eventType))
            {
                eventTypeDictionary.Add(eventType, new List<UnityEvent<int>>());
                listeners.Add(eventType, new List<UnityAction<int>>());
            }
            else
            {
                eventTypeDictionary[eventType].Clear();
                listeners[eventType].Clear();
            }
        }
    }

    public void AddEvent(EventType eventType, UnityEvent<int> intEvent)
    {
        //因为listeners[eventType]的所有listener监听这个eventType的event，所以要给这个intEvent加上listeners[eventType]的所有listener
        foreach (UnityAction<int> listener in listeners[eventType])
        {
            intEvent.AddListener(listener);
        }

        //因为这个intEvent是eventType的，所以加进这个List
        eventTypeDictionary[eventType].Add(intEvent);
    }

    public void AddListener(EventType eventType, UnityAction<int> listener)
    {
        // 代码逻辑解释类似AddEvent()
        foreach (UnityEvent<int> intEvent in eventTypeDictionary[eventType])
        {
            intEvent.AddListener(listener);
        }

        listeners[eventType].Add(listener);
    }
}