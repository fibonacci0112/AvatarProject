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
        //KI.GetComponent<stop>().enabled = a[3].Equals('1');

        //_animator.SetBool("dance", a[4].Equals('1'));
        //_animator.SetBool("wave", a[5].Equals('1'));
    }

    private void Custom(string a)
    {
        SpeechTest.VoiceOutput(a.Substring(3));
        //richtiger verweis noetig
        GameObject o = new GameObject();
        MonoBehaviour[] c = o.GetComponents<MonoBehaviour>();

        for (int i = 0; i < c.Length; i++)
        {
            c[i].enabled = a[i].Equals('1');
        }
    }
}
