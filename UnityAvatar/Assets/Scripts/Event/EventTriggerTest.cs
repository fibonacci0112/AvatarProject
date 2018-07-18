using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour {

	void Update () {
        EventManager.TriggerEvent("test", "bla");
	}
}
