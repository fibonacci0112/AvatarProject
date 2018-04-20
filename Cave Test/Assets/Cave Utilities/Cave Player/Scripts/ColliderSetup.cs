using UnityEngine;
using System.Collections;

/*Sets the Players Collider according to the users Body. The Colliders height is set to the distance between head and hip and the radius is set by the average value of the distance of each shoulder to the bodies middle. If there is no Kinect Connected, the Collider gets some default values. Height: 0.8 and radius: 0.3*/
public class ColliderSetup : MonoBehaviour {

	/*tollerance in which the value for the collider callculations can differ without changing the collider */
	public float updateTollerance = 0.1f;

	/*Indicates if the Kinect is used*/
	private bool kinectInUse = false;

	/*the instance of the kinect Controller*/
	private KinectController kinectC;

	/*Instance of the Players Capsule Collider*/
	private CapsuleCollider playerCollider;

	/*Initialises the Intsances and sets default Values on the Collider*/
	void Start () {
		kinectC = (KinectController) FindObjectOfType (typeof(KinectController));//  GameObject.Find ("KinectController");
		if (kinectC != null) {
			kinectC = KinectController.Instance;
			kinectInUse = kinectC.useKinect;
		}
		playerCollider = GetComponent<CapsuleCollider> ();
		playerCollider.radius = 0.2f;
		//playerCollider.height = 0.8f;
		playerCollider.height = 0.5f;
	}
	
	/*Updates the Collider if the kinect is used.*/
	void Update () {
		/*if (this.kinectInUse) {
			float radius = ((Vector3.Distance (kinectC.getShoulderLeft (), kinectC.getShoulderCenter())) + (Vector3.Distance (kinectC.getShoulderRight (), kinectC.getShoulderCenter()))) / 2;
			float height = Vector3.Distance (kinectC.getHead (), kinectC.getHipCenter ());
			Vector3 center = new Vector3 ((kinectC.getHead ().x + kinectC.getHipCenter ().x) / 2, (kinectC.getHead ().y + kinectC.getHipCenter ().y) / 2, (kinectC.getHead ().z + kinectC.getHipCenter ().z) / 2);
			if ((Mathf.Abs (playerCollider.height - height) > this.updateTollerance) || (Mathf.Abs (playerCollider.radius - radius) > this.updateTollerance)) {
				playerCollider.radius = radius;
				playerCollider.height = height;
				playerCollider.center = center;
			}
		}*/

	}
}
