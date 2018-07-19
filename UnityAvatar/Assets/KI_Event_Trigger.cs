using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI_Event_Trigger : MonoBehaviour {
    

	void Update () {
        if (Input.GetKeyDown("t"))
        {
            EventManager.TriggerEvent("KI_movement", "100000follow");
        }

        if (Input.GetKeyDown("z"))
        {
            EventManager.TriggerEvent("KI_movement", "010000patrol");
        }
        if(Input.GetKeyDown("u"))
        {
            EventManager.TriggerEvent("KI_movement", "001000table");
        }
        if (Input.GetKeyDown("i"))
        {
            EventManager.TriggerEvent("KI_movement", "000100stop");
        }
        if (Input.GetKeyDown("o"))
        {
            EventManager.TriggerEvent("KI_movement", "000010animate");
        }
        if (Input.GetKeyDown("p"))
        {
            EventManager.TriggerEvent("KI_custom", "10customhund");
        }
    }
   
}
