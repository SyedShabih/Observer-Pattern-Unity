using System.Collections.Generic;
using UnityEngine;
using System;

public class Observer
{
    private struct CustomEvent
    {
        public GameObject Obj { get; }
        public Action Action { get; }

        public CustomEvent(GameObject obj, Action action)
        {
            Obj = obj;
            Action = action;
        }
    }

    private static Dictionary<string, List<CustomEvent>> eventsDict = new Dictionary<string, List<CustomEvent>>();

    //Register a new evnet
    public static void RegisterCustomEvent(GameObject obj, string eventName, Action action)
    {
        var customEvent = new CustomEvent(obj, action);

        if (!eventsDict.ContainsKey(eventName))
        {
            eventsDict[eventName] = new List<CustomEvent>();
        }

        eventsDict[eventName].Add(customEvent);
        Debug.Log("Registered Custom Event: " + eventName + " for object: "+ obj.name);
    }


    //Fire a new evnet
    public static void DispatchCustomEvent(string eventName)
    {
        if (!eventsDict.ContainsKey(eventName))
        {
            //Debug.LogError($"No event registered with name: {eventName}");
            return;
        }

        for (int i = eventsDict[eventName].Count - 1; i >= 0; i--)
        {
            var customEvent = eventsDict[eventName][i];

            if (customEvent.Obj == null)
            {
                eventsDict[eventName].RemoveAt(i);
            }
            else
            {
                customEvent.Action?.Invoke();
            }
        }
    }

    //Unlisten an event on object with name
    public static void UnregisterCustomEvent(GameObject obj, string eventName)
    {
        for (int i = eventsDict[eventName].Count - 1; i >= 0; i--)
        {
            if (ReferenceEquals(obj, eventsDict[eventName][i].Obj))
            {
                Debug.Log("Custom Event: " + eventName + " Removed from object: "+eventsDict[eventName][i].Obj.name);
                eventsDict[eventName].RemoveAt(i);
            }
        }
    }

    //Unlisten all events with name
    public static void UnregisterAllCustomEventsWithName(string eventName)
    {
        Debug.Log("Custom Event: " + eventName + " Removed");
        eventsDict.Remove(eventName);
    }

    //Unlisten all events on object
    public static void UnregisterAllCustomEventsOnObject(GameObject obj)
    {
        foreach (var pair in eventsDict)
        {
            pair.Value.RemoveAll(customEvent => ReferenceEquals(customEvent.Obj, obj));
            Debug.Log("Removed all custom events on obj: " + obj.name);
        }
    }

    //Unlisten all events
    public static void UnregisterAllCustomEvents()
    {
        eventsDict.Clear();
        Debug.Log("Removed all custom events");
    }
}
