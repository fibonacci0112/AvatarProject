using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaPiontSetTag : MonoBehaviour {
    public GameObject waypiont1;
    public GameObject waypiont2;
    public GameObject waypiont3;
    public GameObject waypiont4;
    public GameObject waypiont5;
    public GameObject waypiont6;
    public GameObject waypiont7;
    public GameObject waypiont8;
    List<GameObject> myList = new List<GameObject>();
    // Use this for initialization

    GameObject temp;

    void Start () {


        myList.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));

        /*waypiont2.name = "waypiont2";
        waypiont3.name = "waypiont3";
        waypiont4.name = "waypiont4";
        waypiont5.name = "waypiont5";
        waypiont6.name = "waypiont6";
        waypiont7.name = "waypiont7";
        waypiont8.name = "waypiont8";
        waypiont1.name = "Zusätzlicher name";*/

        temp = myList.Where(gameObject => gameObject.name == "chairs").SingleOrDefault();
       // Debug.Log("Names of waypionts:" + temp.name );
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
