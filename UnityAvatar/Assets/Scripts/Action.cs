using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour{

	public virtual void perform()
	{
		Debug.Log("The method in your script needs the statement: 'override'");
	}

	public virtual void perform(string parameters)
	{
		Debug.Log("The method in your script needs the statement: 'override'");
	}
		
	public virtual void perform(string[] parameters)
	{
		Debug.Log("The method in your script needs the statement: 'override'");
	}
}