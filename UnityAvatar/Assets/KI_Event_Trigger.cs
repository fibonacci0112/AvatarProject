using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI_Event_Trigger : MonoBehaviour {
    

	void Update () {
        if (Input.GetKeyDown("t"))
        {
            EventManager.TriggerEvent("KI_follow");
        }

        if (Input.GetKeyDown("z"))
        {
            EventManager.TriggerEvent("KI_patrol");
        }
        if(Input.GetKeyDown("u"))
        {
            EventManager.TriggerEvent("KI_goTo");
        }
        if (Input.GetKeyDown("i"))
        {
            EventManager.TriggerEvent("KI_stop");
        }
        if(Input.GetKeyDown("o"))
        {
            EventManager.TriggerEvent("KI_wave");
        }
        if (Input.GetKeyDown("p"))
        {
            EventManager.TriggerEvent("KI_dance");
        }





    }
   
}
