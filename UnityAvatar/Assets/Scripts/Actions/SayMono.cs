using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayMono : Action {

	public void perform(string _param)
	{
		Debug.Log("Avatar says: "+ _param);
	}
}
