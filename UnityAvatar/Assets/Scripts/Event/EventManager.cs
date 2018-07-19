using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary<string, ParamEvent> eventDictionary;

    private static EventManager eventManager;


    [Serializable]
    public class ParamEvent : UnityEvent<string>
    {
    }

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
            eventDictionary = new Dictionary<string, ParamEvent>();
        }
    }

    public static void StartListening (string eventName, UnityAction<string> listener)
    {
        ParamEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ParamEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening (string eventName, UnityAction<string> listener)
    {
        if (eventManager == null) return;
        ParamEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent (string eventName, string param)
    {
        ParamEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

}
