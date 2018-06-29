using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStill : Action {

	public GameObject target;

	public override void perform()
	{
		Debug.Log ("Stehe still. My name is: "+ target.gameObject.name);
		MonoBehaviour[] mBList = target.gameObject.GetComponents <MonoBehaviour>();
		foreach (MonoBehaviour mB in mBList) {
			Debug.Log (mB.ToString()+ " is enabled: "+ mB.isActiveAndEnabled);
		}
		foreach (MonoBehaviour mB in mBList) {
			mB.enabled = false;
		}
	}
}