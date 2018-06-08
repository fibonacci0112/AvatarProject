using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour{

	public virtual void perform()
	{
<<<<<<< HEAD
		Debug.Log("The method in your script needs the statement: 'override'");
	}

	public virtual void perform(string parameters)
	{
		Debug.Log("The method in your script needs the statement: 'override'");
=======
		Debug.Log ("No Override here");
	}
		
	public virtual void perform(string parameters)
	{
		Debug.Log ("No Override here");
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
	}

	public virtual void perform(string[] parameters)
	{
<<<<<<< HEAD
		Debug.Log("The method in your script needs the statement: 'override'");
=======
		Debug.Log ("No Override here");
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
	}
}