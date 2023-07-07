using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<string, List<EventCallback>> eventDictionary;
    

    static EventBus()
    {
        eventDictionary = new Dictionary<string, List<EventCallback>>();
    }

    public static void Subscribe(string eventName, EventCallback callback)
    {
        if (eventDictionary.TryGetValue(eventName, out var callbacks))
        {
            callbacks.Add(callback);
        }
        else
        {
            eventDictionary[eventName] = new List<EventCallback>{ callback };
        }
    }

    public static void Unsubscribe(string eventName, EventCallback callback)
    {
        if (eventDictionary.TryGetValue(eventName, out var callbacks))
        {
            callbacks.Remove(callback);
        }
    }

    public static void Trigger(string eventName)
    {
        if (!eventDictionary.TryGetValue(eventName, out var callbacks)) return;
        
        foreach (var callback in callbacks)
        {
            callback.Invoke();
        }
    }
}

public delegate void EventCallback();