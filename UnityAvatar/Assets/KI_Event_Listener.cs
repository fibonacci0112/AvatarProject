using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KI_Event_Listener : MonoBehaviour {

    private UnityAction followListener;
    private UnityAction patrolListener;
   

    public GameObject KI;
   

   

    private void Awake()
    {
        followListener = new UnityAction(Follow);
        patrolListener = new UnityAction(Patrol);
        

        //ki = GameObject.FindGameObjectWithTag("KI");
    }

   

    void OnEnable()
    {
        EventManager.StartListening("KI_follow", followListener);
        EventManager.StartListening("KI_patrol", patrolListener);
        
    }

    private void OnDisable()
    {
        EventManager.StopListening("KI_follow", followListener);
        EventManager.StopListening("KI_patrol", patrolListener);
        
    }
    private void Follow()
    {
       // ki = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI follow activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = true;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
    }

    private void Patrol()
    {
     //   KI = GameObject.FindGameObjectWithTag("KI");
        Debug.Log("KI patrol activated");
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = true;
    }
       
    
}
