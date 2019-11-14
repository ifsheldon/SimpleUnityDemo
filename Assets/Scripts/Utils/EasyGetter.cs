using System;
using UnityEngine;

/// <summary>
/// the class is for facilitating retrieving components that are usually used.
/// </summary>
public static class EasyGetter
{
    public static GameObject GetGlobalManager()
    {
        return GameObject.FindGameObjectWithTag("GameController");
    }

    public static GameObject GetGlobalManager(String tag)
    {
        return GameObject.FindGameObjectWithTag(tag);
    }

    public static EventsManager GetEventsManager()
    {
        GameObject manager = GetGlobalManager();
        return manager.GetComponent<EventsManager>();
    }

    public static EventsManager GetEventsManager(GameObject globalManager)
    {
        return globalManager.GetComponent<EventsManager>();
    }
}