using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Say : Action {

	public string preConfig = "nothing";

	public override void perform(string _param)
	{
		Debug.Log("Avatar says: "+ _param);
	}

	public override void perform()
	{
		Debug.Log("Avatar says "+preConfig+".");
	}

}
