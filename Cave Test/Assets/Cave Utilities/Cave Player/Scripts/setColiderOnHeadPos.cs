using UnityEngine;
using System.Collections;

public class setColiderOnHeadPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 center = GameObject.Find ("StereoHead").transform.position;
		center.y = 0.95f;
		this.GetComponent<CapsuleCollider> ().center = center;
	}
}
