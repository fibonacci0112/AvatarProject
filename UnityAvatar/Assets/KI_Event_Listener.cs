using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KI_Event_Listener : MonoBehaviour {

    private UnityAction<string> moveListener;
    private UnityAction<string> customListener;

    public GameObject KI;

    private void Awake()
    {
        moveListener = Movement;
        customListener = Custom;
    }

    void OnEnable()
    {
        EventManager.StartListening("KI_movement", moveListener);
        EventManager.StartListening("KI_custom", customListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("KI_movement", moveListener);
        EventManager.StopListening("KI_custom", customListener);

    }
    private void Movement(string a)
    {
        SpeechTest.VoiceOutput(a.Substring(6));
        KI.GetComponent<NPCSimplePatrol>().enabled = a[0].Equals('1');
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = a[1].Equals('1');
        KI.GetComponent<goToWaypoint>().enabled = a[2].Equals('1');
        KI.GetComponent<stop>().enabled = a[3].Equals('1');
        KI.GetComponent<dance>().enabled = a[4].Equals('1');
        KI.GetComponent<wave>().enabled = a[5].Equals('1');
    }

    private void Custom(string a)
    {
        GameObject o = GameObject.Find("CustomScripts");
        MonoBehaviour[] c = o.GetComponentsInChildren<MonoBehaviour>();
        Debug.Log(c.Length);
        string tmp = a.Substring(c.Length);
        SpeechTest.VoiceOutput(tmp);
        for (int i = 0; i < c.Length; i++)
        {
            c[i].enabled = a[i].Equals('1');
        }
    }
}
