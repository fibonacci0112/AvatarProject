using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop : MonoBehaviour {

    public GameObject KI;
    Animator _animator;
    Rigidbody rb;
    public Assets.Code.ConnectedPatrol patrol;
    Vector3 _pos;
    // Use this for initialization
    void Start ()
    {
        _animator = KI.GetComponentInChildren<Animator>();
        patrol = KI.GetComponent<Assets.Code.ConnectedPatrol>();
        _pos = KI.transform.position;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        KI.GetComponent<NPCSimplePatrol>().enabled = false;
        KI.GetComponent<Assets.Code.ConnectedPatrol>().enabled = false;
        KI.GetComponent<goToWaypoint>().enabled = false;

        _animator.SetBool("walking", false);

        KI.transform.position = _pos;

        
        
    }
   
}
