using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : Action {

	public GameObject KI;

	public override void perform()
	{
		Debug.Log ("zurück gestetzt");
		//Debug.Log ("komonenten:" +KI.transform.renderer.GetComponents ().ToString()); //GetComponent<Material> ().SetColor (1, Random.ColorHSV ());
		KI.transform.SetPositionAndRotation(new Vector3(1, 1, 1),new Quaternion(0,0,0,0));
	}
}
