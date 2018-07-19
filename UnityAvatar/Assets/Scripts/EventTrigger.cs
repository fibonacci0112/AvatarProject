using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{

    // Use this for initialization
    
    
    Animator n_animator;
    
    void Start()
    {
        n_animator = GetComponentInChildren<Animator>();

    }


    
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            EventManager.TriggerEvent("KI_movement", "000010animate");
        }
    }
}
