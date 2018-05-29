using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{

    // Use this for initialization
    public GameObject Player;
    bool random = false;


    void Start()
    {
        

    }


    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {

            Assets.Code.ConnectedPatrol connPatrol = c.gameObject.GetComponent<Assets.Code.ConnectedPatrol>();
            connPatrol.enabled = false;
            FreeRandomPatrol randomPatrol = c.gameObject.GetComponent<FreeRandomPatrol>();
            randomPatrol.enabled = true;
        }

       
              


        

       

    }
}
