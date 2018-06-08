using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class HTWAvatar : MonoBehaviour {

	// public SayMono sayMono;
	// public Object scriptObject;
	// public Action scriptAction;
	// public UnityEvent ActionEntrysE;
	public ActionEntry[] actionEntrys;
	public GameObject[] actionsObjects;

	void perform(string _actionName)
	{
		foreach (GameObject go in actionsObjects)
		{
			if (go.name == _actionName) 
			{
				//go.getComponent<Action> ().perform ();// .objectWithActionScript.getComponent<Action> ().perform ();
			}
		}

		foreach (ActionEntry a in actionEntrys)
		{
			if (a.actionName == _actionName) 
			{
				//a.objectWithActionScript.getComponent<Action> ().perform ();
			}
		}
	}

	void perform(string _actionName, string _parameter)
	{
		foreach (ActionEntry a in actionEntrys)
		{
			if (a.actionName == _actionName) 
			{
				//a.objectWithActionScript.getComponent<Action> ().perform (_parameter);
			}
		}
	}
}

[System.Serializable]
public class ActionEntry : MonoBehaviour
{
	public string actionName;
	public GameObject objectWithActionScript;
}