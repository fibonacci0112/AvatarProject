using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour{

	public virtual void perform()
	{
		Debug.Log ("No Override here");
	}
		
	public virtual void perform(string parameters)
	{
		Debug.Log ("No Override here");
	}

	public virtual void perform(string[] parameters)
	{
		Debug.Log ("No Override here");
	}
}