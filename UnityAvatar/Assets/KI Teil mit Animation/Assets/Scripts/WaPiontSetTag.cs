using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaPiontSetTag : MonoBehaviour {
    public GameObject waypiont1;
    public GameObject waypiont2;
    public GameObject waypiont3;
    public GameObject waypiont4;
    public GameObject waypiont5;
    public GameObject waypiont6;
    public GameObject waypiont7;
    public GameObject waypiont8;
    // Use this for initialization
    void Start () {
       /* waypiont1.tag = "waypiont1";
        waypiont2.tag = "waypiont2";
        waypiont3.tag = "waypiont3";
        waypiont4.tag = "waypiont4";
        waypiont5.tag = "waypiont5";
        waypiont6.tag = "waypiont6";
        waypiont7.tag = "waypiont7";
        waypiont8.tag = "waypiont8";*/

        waypiont1.name = "waypiont1";
        waypiont2.name = "waypiont2";
        waypiont3.name = "waypiont3";
        waypiont4.name = "waypiont4";
        waypiont5.name = "waypiont5";
        waypiont6.name = "waypiont6";
        waypiont7.name = "waypiont7";
        waypiont8.name = "waypiont8";
        waypiont1.name = "Zusätzlicher name";

        Debug.Log("Names of waypionts:" + waypiont1.name + waypiont2.name + waypiont3.name + waypiont4.name + waypiont5.name + waypiont6.name+ waypiont7.name + waypiont8.name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
