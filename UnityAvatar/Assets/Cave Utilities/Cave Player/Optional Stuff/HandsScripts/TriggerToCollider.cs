using UnityEngine;
using System.Collections;

public class TriggerToCollider : MonoBehaviour {

	public int objectsInCollission = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.GetComponentInParent<Rigidbody> () != null && collision.tag != "Projektil") {
			if(objectsInCollission > 0){
				objectsInCollission--;
				if (objectsInCollission == 0){
					findSmallesCollider().isTrigger = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.GetComponentInParent<Rigidbody> () != null && collision.tag != "Projektil") {
			if(objectsInCollission == 0){

				findSmallesCollider().isTrigger = false;
			}
			objectsInCollission++;

		}
	}

	SphereCollider findSmallesCollider(){
		SphereCollider[] colls = this.GetComponents<SphereCollider>();
		SphereCollider small = colls[0];
		for (int i = 0; i < colls.Length; i++){
			if(colls[i].radius < small.radius){
				small = colls[i];
			}
		}

		return small;
	}
}
