using System;
using UnityEngine;

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