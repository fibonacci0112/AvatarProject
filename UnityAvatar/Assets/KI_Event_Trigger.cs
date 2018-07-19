using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI_Event_Trigger : MonoBehaviour {
    

	void Update () {
        if (Input.GetKeyDown("t"))
        {
            EventManager.TriggerEvent("KI_movement", "100follow");
        }

        if (Input.GetKeyDown("z"))
        {
            EventManager.TriggerEvent("KI_movement", "010patrol");
        }
        if(Input.GetKeyDown("u"))
        {
            EventManager.TriggerEvent("KI_movement", "001walk");
        }
    }
   
}
