using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;

public class HTWAvatar : MonoBehaviour {

	public SayMono sayMono;
	public Object scriptObject;
	public Action scriptAction;
	public ActionEntry[] actionEntrys;
	public UnityEvent ActionEntrysE;

	void perform(string _actionName)
	{
		foreach (ActionEntry a in actionEntrys)
		{
			if (a.actionName == _actionName) 
			{
				//a.script.perfor
			}
		}
	}

	// Use this for initialization
	void Start () {

		scriptAction = GetComponent<Action>();
		scriptAction.perform ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class ActionEntry
{
	public string actionName;
	public Object script;
}
