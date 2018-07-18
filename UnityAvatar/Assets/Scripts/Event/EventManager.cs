using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent<string>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager Script on a GameObject in your Scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
          
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<string>>();
        }
    }

    public static void StartListening (string eventName, UnityAction<string> listener)
    {
        UnityEvent<string> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            Type d1 = typeof(UnityEvent<>);
            Type typeArg = typeof(string);
            Type makeme = d1.MakeGenericType(typeArg);
            object o = Activator.CreateInstance(makeme);

            thisEvent = o as UnityEvent<string>;
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening (string eventName, UnityAction<string> listener)
    {
        if (eventManager == null) return;
        UnityEvent<string> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent (string eventName, string param)
    {
        UnityEvent<string> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

}
