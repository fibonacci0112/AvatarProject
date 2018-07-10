using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class waypiontName : MonoBehaviour {
    public List<string> nameListp = new List<string>();
    static List<string> nameLists = new List<string>();
    static List<GameObject> wayPionts = new List<GameObject>();
    GameObject waypoint;
    // Use this for initialization
    void Start() {
        wayPionts.Add(this.gameObject);
        nameLists = nameListp;
        findWP("ecke");

    }

   
	
	// Update is called once per frame
	void Update () {

        
    }

    static GameObject findWP(string _name)
    {
        foreach(GameObject wp in wayPionts)
        {
            



            foreach (string name in nameLists)
            {

                if (nameLists.Contains(_name))
                {
                    Debug.Log(wp.name);
                    return wp;
                    
                }
                
            }
            

        }
        
        return null;
        
     }
       
        
}


    

