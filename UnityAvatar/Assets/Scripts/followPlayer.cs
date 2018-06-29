using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

    //You may consider adding a rigid body to the zombie for accurate physics simulation
    private GameObject playerPoint;
    private Vector3 playerPointPos;
    //This will be the zombie's speed. Adjust as necessary.
    private float speed = 6.0f;
    void Start()
    {
        //At the start of the game, the zombies will find the gameobject called wayPoint.
        playerPoint = GameObject.Find("playerPoint");
    }

    void Update()
    {
        playerPointPos = new Vector3(playerPoint.transform.position.x, transform.position.y, playerPoint.transform.position.z);
        //Here, the zombie's will follow the waypoint.
        transform.position = Vector3.MoveTowards(transform.position, playerPointPos, speed * Time.deltaTime);
    }
}
