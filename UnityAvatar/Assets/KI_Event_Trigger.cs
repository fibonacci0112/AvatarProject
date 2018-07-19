using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI_Event_Trigger : MonoBehaviour {
    

	void Update () {
        if (Input.GetKeyDown("1"))
        {
            EventManager.TriggerEvent("KI_movement", "100000follow");
        }

        if (Input.GetKeyDown("2"))
        {
            EventManager.TriggerEvent("KI_movement", "010000walk");
        }
        if(Input.GetKeyDown("3"))
        {
            EventManager.TriggerEvent("KI_movement", "001000table");
        }
        if (Input.GetKeyDown("4"))
        {
            EventManager.TriggerEvent("KI_movement", "000100stop");
        }
        if (Input.GetKeyDown("5"))
        {
            EventManager.TriggerEvent("KI_movement", "000010animate");
        }
        if (Input.GetKeyDown("6"))
        {
            EventManager.TriggerEvent("KI_movement", "000100nothing");
            EventManager.TriggerEvent("KI_custom", "10customhund");
        }

        if (Input.GetKeyDown("7"))
        {
            EventManager.TriggerEvent("KI_movement", "000100nothing");
            EventManager.TriggerEvent("KI_custom", "01customkatze");
        }

        if (Input.GetKeyDown("8"))
        {
            EventManager.TriggerEvent("KI_movement", "000001hello");
        }

        if (Input.GetKeyDown("9"))
        {
            EventManager.TriggerEvent("KI_movement", "000001bye");
        }
    }
   
}
