using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class HTWAvatar : MonoBehaviour {

	//public SayMono sayMono;
	//public Object scriptObject;
	//public Action scriptAction;
	public ActionEntry[] actionEntrys;
	//public UnityEvent ActionEntrysE;

	public void perform(string _actionName)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				Debug.Log("Aktion gefunden: "+ ae.gO.GetComponent<Action> ().GetType());
				ae.gO.GetComponent<Action> ().perform();
			}
		}
	}

	public void perform(string _actionName, string _parameter)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				Debug.Log("Aktion gefunden, Parameter: "+ _parameter +" übergeben");
				ae.gO.GetComponent<Action> ().perform(_parameter);
			}
		}
	}

	public void perform(string _actionName, string[] _parameters)
	{
		foreach (ActionEntry ae in actionEntrys)
		{
			if (ae.actionName == _actionName) 
			{
				ae.gO.GetComponent<Action> ().perform(_parameters);
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
