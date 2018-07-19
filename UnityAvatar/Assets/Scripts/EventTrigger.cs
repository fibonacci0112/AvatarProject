using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{

    // Use this for initialization
    public GameObject Player;
    bool random = false;
    Animator n_animator;
    bool differentAn = false;
    bool walking = true;
    GameObject AnimatedObject;
    public Animation youranimation;
    void Start()
    {
        n_animator = GetComponentInChildren<Animator>();

    }


    
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            n_animator.SetTrigger("zombie");
        }
    }
}
