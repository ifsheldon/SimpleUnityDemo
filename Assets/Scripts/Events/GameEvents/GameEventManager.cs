using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    Dictionary<GameEventType, List<UnityEvent>> gameEventTypeDictionary =
        new Dictionary<GameEventType, List<UnityEvent>>();

    Dictionary<GameEventType, List<UnityAction>>
        gameEventListeners = new Dictionary<GameEventType, List<UnityAction>>();

    void Awake()
    {
        foreach (GameEventType eventType in Enum.GetValues(typeof(GameEventType)))
        {
            if (!gameEventTypeDictionary.ContainsKey(eventType))
            {
                gameEventTypeDictionary.Add(eventType, new List<UnityEvent>());
                gameEventListeners.Add(eventType, new List<UnityAction>());
            }
            else
            {
                gameEventTypeDictionary[eventType].Clear();
                gameEventListeners[eventType].Clear();
            }
        }
    }

    public void AddEvent(GameEventType eventType, UnityEvent e)
    {
        //因为listeners[eventType]的所有listener监听这个eventType的event，所以要给这个intEvent加上listeners[eventType]的所有listener
        foreach (UnityAction listener in gameEventListeners[eventType])
        {
            e.AddListener(listener);
        }

        //因为这个e是eventType的，所以加进这个List
        gameEventTypeDictionary[eventType].Add(e);
    }

    public void AddListener(GameEventType eventType, UnityAction listener)
    {
        // 代码逻辑解释类似AddEvent()
        foreach (UnityEvent e in gameEventTypeDictionary[eventType])
        {
            e.AddListener(listener);
        }

        gameEventListeners[eventType].Add(listener);
    }

    public void RemoveListener(GameEventType eventType, UnityAction listener)
    {
        foreach (UnityEvent unityEvent in gameEventTypeDictionary[eventType])
        {
            unityEvent.RemoveListener(listener);
        }

        gameEventListeners[eventType].Remove(listener);
    }
}