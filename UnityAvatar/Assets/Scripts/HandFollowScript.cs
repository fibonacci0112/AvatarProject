using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowScript : MonoBehaviour {

    public Transform handPosition;

	// Use this for initialization
	void Start () {
        this.transform.position = handPosition.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = handPosition.transform.position;
    }
}
