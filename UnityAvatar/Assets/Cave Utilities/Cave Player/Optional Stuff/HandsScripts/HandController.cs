using UnityEngine;
using System.Collections;

/*This script controlls and represents the state of the hands.*/
public class HandController : MonoBehaviour {

	/*indicated if the hand is closed*/
	public bool isClosed;
	/*indicates if a wall is touched*/
	public bool hitsWall;
	/*indicates if this is the left hand*/
	public bool isLeft = false;
	/*Instance of the kinect controller*/
	private KinectController kinect;
	/*instance of the controller which controlls if the hands are open*/
	private HandOpenControll handOpen;
	/*indicates if the kinect is used*/
	private bool kinectInUse = false;

    public Transform lookTarget;

    public Vector3 testPos;


	/*Inistializes the controllers*/
	void Start () {
		kinect = KinectController.Instance;
		if (kinect != null) {
			kinectInUse = kinect.useKinect;
		}
		/*Gets a referenz of the 'HandOpenControll' of the current 	Object*/
		handOpen = GetComponent<HandOpenControll> ();
	}
	
	/*checks every frame if the hands are closed, sets the according state and sests the position of the hands by the Kinect*/
	void FixedUpdate () {
		isClosed = handIsClosed ();



        if (kinectInUse) {
			Vector3 pos = new Vector3();
			if(isLeft){

				pos = GameObject.Find("Player").transform.TransformDirection(kinect.getHandLeft());
			}
			else{
				pos = GameObject.Find("Player").transform.TransformDirection(kinect.getHandRight());
			}
	//	pos = testPos;
	//	Debug.Log (pos);
			pos.Set(pos.x + GameObject.Find("Player").transform.position.x, pos.y + GameObject.Find("Player").transform.position.y, pos.z+ GameObject.Find("Player").transform.position.z);
		
			this.GetComponent<Rigidbody>().MovePosition(pos);
			this.transform.position = pos;

            Vector3 relativePos = this.lookTarget.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            this.transform.rotation = rotation;



        }
	}

	/*When the object collides with another this cheks if the objectis collided with an object with the "Wall" tag and sets the according state*/

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Wall") {
			this.hitsWall = true;
		}
	}

	/*When the object stops colliding a wall this Methode sets the according state*/
	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Wall") {
			this.hitsWall = false;
		}
	}



	/*checks if the hands where closed by the collider*/
	bool handIsClosed(){
		return handOpen.handClosed;
	}
}
