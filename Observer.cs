using System.Collections.Generic;
using UnityEngine;
using System;

public class Observer
{
    private class eventParam { public GameObject obj; public string eventName; public Action action; }
    private static List<eventParam> eventsList = new List<eventParam>();

    //Add a custom Event
    public static void RegisterCustomEvent(GameObject obj, string eventName, Action action)
    {
        eventParam param = new eventParam();
        param.obj = obj;
        param.eventName = eventName;
        param.action = action;
        eventsList.Add(param);
        Debug.Log("Custom Event: " + eventName + " Added");
    }

    //Dispatch a custom event by name
    public static void DispatchCustomEvent(string eventName)
    {
        for (int i = 0; i < eventsList.Count; i++)
        {
            if (eventsList[i].obj == null)
            {
                Debug.Log("Custom Event: " + eventName + " on Null Object Removed");
                eventsList.RemoveAt(i);
                i--;
            }
            else if (string.Compare(eventsList[i].eventName, eventName)==0)
            {
                eventsList[i].action?.Invoke();
                Debug.Log("Custom Event: " + eventName + " Called from Object: "+eventsList[i].obj.name);
            }
        }
    }

    //Unregister a custom event with name on the object
    public static void UnregisterCustomEvent(GameObject obj, string eventName)
    {
        for (int i = 0; i < eventsList.Count; i++)
        {
            if (ReferenceEquals(obj,eventsList[i].obj) && string.Compare(eventsList[i].eventName, eventName) == 0)
            {
                Debug.Log("Custom Event: " + eventName + " Removed from Object: "+eventsList[i].obj.name);
                eventsList.RemoveAt(i);
                i--;
            }
        }
    }

    //Unregister all custom events with name
    public static void UnregisterAllCustomEventsWithName(string eventName)
    {
        for (int i = 0; i < eventsList.Count; i++)
        {
            if (string.Compare(eventsList[i].eventName, eventName) == 0)
            {
                Debug.Log("Custom Event: " + eventName + " Removed for All Object: " + eventsList[i].obj.name);
                eventsList.RemoveAt(i);
                i--;
            }
        }
    }

    //Unregister all custom events on the object
    public static void UnregisterAllCustomEventsOnObject(GameObject obj)
    {
        for (int i = 0; i < eventsList.Count; i++)
        {
            if (ReferenceEquals(obj, eventsList[i].obj))
            {
                Debug.Log("Custom Event for obj: " + obj.name + " Removed" + eventsList[i].obj.name);
                eventsList.RemoveAt(i);
                i--;
            }
        }
    }

    //unregister all custom events
    public static void UnregisterAllCustomEvents()
    {
        eventsList.Clear();
    }
}
