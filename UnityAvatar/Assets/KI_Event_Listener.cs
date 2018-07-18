using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KI_Event_Listener : MonoBehaviour {

    private UnityAction followListener;
    private UnityAction patrolListener;
    private UnityAction goToListener;
    private UnityAction stopListener;
    private UnityAction waveListener;
    private UnityAction danceListener;


    public GameObject KI;
    
   
   
   

    private void Awake()
    {
        followListener = new UnityAction(Follow);
        patrolListener = new UnityAction(Patrol);
        goToListener = new UnityAction(GoTo);
        stopListener = new UnityAction(Stop);
        waveListener = new UnityAction(Wave);
        danceListener = new UnityAction(Dance);


        //ki = GameObject.FindGameObjectWithTag("KI");
    }

    void OnEnable()
    {
        EventManager.StartListening("KI_follow", followListener);
        EventManager.StartListening("KI_patrol", patrolListener);
        EventManager.StartListening("KI_goTo", goToListener);
        EventManager.StartListening("KI_stop", stopListener);
        EventManager.StartListening("KI_wave", waveListener);
        EventManager.StartListening("KI_dance", danceListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("KI_follow", followListener);
        EventManager.StopListening("KI_patrol", patrolListener);
        EventManager.StopListening("KI_goTo", goToListener);
        EventManager.StopListening("KI_stop", stopListener);
        EventManager.StopListening("KI_wave", waveListener);
        EventManager.StopListening("KI_dance", danceListener);
    }
    private void Follow()
    {
       // ki = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI follow activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = true;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;
        KI.GetComponent<stop>().enabled = false;
    }

    private void Patrol()
    {
     //   KI = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI patrol activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = true;
        KI.GetComponent<goToWaypoint>().enabled = false;
        KI.GetComponent<stop>().enabled = false;
    }
    private void GoTo()
    {
        Debug.Log("KI goTo activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = true;
        KI.GetComponent<stop>().enabled = false;
    }
    private void Stop()
    {
        

        Debug.Log("KI stop activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;
        KI.GetComponent<stop>().enabled = true;


    }


    private void Wave()
    {


        Debug.Log("KI wave activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;

       //_animator.SetBool("wave", true);
    }

    private void Dance()
    {


        Debug.Log("KI dance activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;

        //_animator.SetBool("dance", true);
    }



}
