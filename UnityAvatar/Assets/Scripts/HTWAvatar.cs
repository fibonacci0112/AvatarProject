using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class HTWAvatar : MonoBehaviour {

<<<<<<< HEAD
	public ActionEntry[] actionEntrys;
=======
	//public SayMono sayMono;
	//public Object scriptObject;
	//public Action scriptAction;
	public ActionEntry[] actionEntrys;
	//public UnityEvent ActionEntrysE;
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f

	public void perform(string _actionName)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
<<<<<<< HEAD
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform ();
=======
				Debug.Log("Aktion gefunden: "+ ae.gO.GetComponent<Action> ().GetType());
				ae.gO.GetComponent<Action> ().perform();
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
			}
		}
	}

	public void perform(string _actionName, string _parameter)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
<<<<<<< HEAD
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform (_parameter);
=======
				Debug.Log("Aktion gefunden, Parameter: "+ _parameter +" übergeben");
				ae.gO.GetComponent<Action> ().perform(_parameter);
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
			}
		}
	}

<<<<<<< HEAD
	public void perform(string _actionName, string[] _parameter)
=======
	public void perform(string _actionName, string[] _parameters)
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
<<<<<<< HEAD
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform (_parameter);
=======
				ae.gO.GetComponent<Action> ().perform(_parameters);
>>>>>>> 6b0bd696fd9d2af13a4610e95cea874cd15e761f
			}
		}
	}
}

[System.Serializable]
public class ActionEntry
{
	public string actionName;
	public GameObject gO;
}
