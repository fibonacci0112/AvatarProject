using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KI_Event_Listener : MonoBehaviour {

    private UnityAction followListener;
    private UnityAction patrolListener;
    private UnityAction goToListener;
   
    public GameObject KI;

    private void Awake()
    {
        followListener = new UnityAction(Follow);
        patrolListener = new UnityAction(Patrol);
        goToListener = new UnityAction(GoTo);
        

        //ki = GameObject.FindGameObjectWithTag("KI");
    }

    void OnEnable()
    {
        EventManager.StartListening("KI_follow", followListener);
        EventManager.StartListening("KI_patrol", patrolListener);
        EventManager.StartListening("KI_goTo", goToListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("KI_follow", followListener);
        EventManager.StopListening("KI_patrol", patrolListener);
        EventManager.StopListening("KI_goTo", goToListener);
    }
    private void Follow()
    {
       // ki = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI follow activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = true;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;
    }

    private void Patrol()
    {
     //   KI = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI patrol activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = true;
        KI.GetComponent<goToWaypoint>().enabled = false;
    }
    private void GoTo()
    {
        Debug.Log("KI goTo activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = true;
    }
}
