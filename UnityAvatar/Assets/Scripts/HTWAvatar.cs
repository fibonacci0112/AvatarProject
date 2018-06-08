using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class HTWAvatar : MonoBehaviour {

	public ActionEntry[] actionEntrys;

	public void perform(string _actionName)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform ();
			}
		}
	}

	public void perform(string _actionName, string _parameter)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform (_parameter);
			}
		}
	}

	public void perform(string _actionName, string[] _parameter)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				ae.objectWithActionScript.gameObject.GetComponent<Action> ().perform (_parameter);
			}
		}
	}
}

[System.Serializable]
public class ActionEntry
{
	public string actionName;
	public GameObject objectWithActionScript;
}
