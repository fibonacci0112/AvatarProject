using UnityEngine;
using System.Collections;

using Cave;
using CaveLogger;

/*
 * WIP-Funktion funktioniert noch nicht ganz ( siehe Update()-Funktion )!
 */

public class KinectMovementController : MonoBehaviour {
	public bool useWip = false;
	private KinectController Kinect;

	private Vector3 curUserRotation;
	//private Transform camHolder;
	private CapsuleCollider body;
	private GameObject leftArm;
	private GameObject rightArm;

	private Vector3 leftArmDirection;
	private Vector3 rightArmDirection;
	private Vector3 headToHandDirection;
	private Vector3 handPosition;
	private bool kinectInUse = false;
	private GameObject player;
	private ErrorLog error;

	private BodyArmature armaturePlayer1;
	public static KinectMovementController Instance { get; private set; }

	void Awake(){
		Instance = this;
	}
	void Start()
	{
		error = ErrorLog.GetInstance ();
		player = GameObject.Find("Player");
		Kinect = KinectController.Instance;
		this.kinectInUse = Kinect.useKinect;
		//camHolder = GameObject.Find("CamHolder").transform;
		body = GameObject.Find("Player").GetComponent<CapsuleCollider>();
		leftArm = GameObject.Find("LeftArm");
		rightArm = GameObject.Find("RightArm");

		armaturePlayer1 = new BodyArmature("PlayerMainAvatar");


	}
		
	//	void Update()
	//	{
	//		if (kinectInUse) {
	//			//setColliderByKinect();
	//		}
	//					
	////			setArmatureByKinect(armaturePlayer1);
	//			/*if (kinectInUse) {
	//						Vector3 leftHand = Kinect.getHandLeft ();
	//						Vector3 rightHand = Kinect.getHandRight ();
	//						Vector3 leftShoulder = Kinect.getShoulderLeft ();
	//						Vector3 rightShoulder = Kinect.getShoulderRight ();
	//						Vector3 head = Kinect.getHead ();
	//			
	//			
	//						Vector3 hip = Kinect.getHipCenter ();
	//			
	//						setBodyByKinect (head, leftShoulder, rightShoulder);
	//						leftArmDirection = setArmByKinect (leftArm, leftShoulder, leftHand, -1f);
	//						rightArmDirection = setArmByKinect (rightArm, rightShoulder, rightHand, 1f);
	//			
	//						calculateHandDirection (head, rightHand, leftHand, hip, rightShoulder, leftShoulder);
	//						//camHolder.localPosition = head;
	//				}*/
	//	
	//	}
	//

	/*
	 * WIP - Beschleunigung:
	 */ 
	void Update() {
		if (Kinect.useKinect) {
			Vector3 direction = Kinect.getShoulderLeft () - Kinect.getShoulderRight ();
			float x = direction.x;
			direction.x = -direction.z;
			direction.z = x;
			direction.y = 0;
			direction = direction / direction.magnitude;
			direction = player.transform.TransformDirection (direction);

			//error.Log ("Wip-Wert: " + this.Kinect.getWipSpeed ());
			if (useWip == true) {
				if (!(float.IsNaN (direction.x) || float.IsNaN (direction.y) || float.IsNaN (direction.z))) {
					player.GetComponent<CharacterController> ().Move (direction * -this.Kinect.getWipSpeed () * Time.deltaTime*8);
				}
			}
		}
	}

	public void setColliderByKinect(){
		Vector3 bodyPos = Kinect.getHipCenter ();

		body.center.Set(bodyPos.x, body.center.y, bodyPos.z);

	}

	public bool useKinect(){
		return this.kinectInUse;
	}
	/// <summary>
	/// Sets the body by kinect.
	/// </summary>
	/// <param name='head'>
	/// Head.
	/// </param>
	/// <param name='leftShoulder'>
	/// Left shoulder.
	/// </param>
	/// <param name='rightShoulder'>
	/// Right shoulder.
	/// </param>
	private void setBodyByKinect(Vector3 head, Vector3 leftShoulder, Vector3 rightShoulder)
	{
		body.radius = Vector3.Distance(leftShoulder, rightShoulder) / 2.0f;
		body.height = head.y;

		// Alt: Blickrichtung selber berechnen
		/*Quaternion rot = new Quaternion();
		rot.SetLookRotation(leftShoulder - rightShoulder);
		Quaternion rotation = rot * Quaternion.Euler(0, 90, 0);
		curUserRotation = rotation * Vector3.forward;*/

		if (this.kinectInUse) {
			// Neu: Blickrichtung vom Server. Sieht erstmal komplizierter aus, aber wenn der das schon berechnet,
			// nehmen wir das für die Wartbarkeit.
			Matrix44f m = Kinect.getSensorMatrix (Cave.Kinect.TrackerId.HEAD); //Kinect.getHead();


			Matrix4x4 mat = Matrix4x4.identity;
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					// Ja, transponiert. Wo kommt das her? Unbekannt. Vielleicht, weil der Server schon transponiert.
					// Da steht genauso ein "transponieren. Warum?". Möglicherweise wegen VR-Juggler.
					mat [i, j] = m [j, i];
				}
			}

			curUserRotation = mat.MultiplyPoint3x4 (Vector3.forward);
			curUserRotation [1] = 0f;
		}
	}


	/// <summary>
	/// Sets the arm by kinect.
	/// </summary>
	/// <returns>
	/// The arm by kinect.
	/// </returns>
	/// <param name='arm'>
	/// Arm.
	/// </param>
	/// <param name='shoulder'>
	/// Shoulder.
	/// </param>
	/// <param name='hand'>
	/// Hand.
	/// </param>
	/// <param name='xOffset'>
	/// X offset.
	/// </param>
	private Vector3 setArmByKinect(GameObject arm, Vector3 shoulder, Vector3 hand, float xOffset)
	{
		hand.x += 100;
		hand.y += 100;
		hand.z += 100;
		shoulder.x += 100;
		shoulder.y += 100;
		shoulder.z += 100;

		Rigidbody body = arm.GetComponent<Rigidbody>();
		Quaternion rotation = new Quaternion();
		Vector3 lookDir = (hand - shoulder);
		if (lookDir == Vector3.zero)
		{
			return new Vector3(0.0f, 0.0f, 1.0f);
		}

		body.angularVelocity = Vector3.zero;
		body.velocity = Vector3.zero;

		//CapsuleCollider armCollider = arm.GetComponentInChildren<CapsuleCollider>();
		//armCollider.height = lookDir.magnitude;
		//armCollider.center = new Vector3(0, 0, lookDir.magnitude / 2.0f);

		lookDir.Normalize();

		lookDir = transform.TransformDirection(lookDir);

		rotation.SetLookRotation(lookDir);
		rotation = rotation * Quaternion.Euler(-90, 0, 0);

		body.MoveRotation(rotation);

		shoulder.x -= 100;
		shoulder.y -= 100;
		shoulder.z -= 100;
		body.MovePosition(transform.TransformPoint(shoulder));

		return lookDir;
	}

	/// <summary>
	/// Calculates the hand direction.
	/// </summary>
	/// <param name='head'>
	/// Head.
	/// </param>
	/// <param name='rightHand'>
	/// Right hand.
	/// </param>
	/// <param name='leftHand'>
	/// Left hand.
	/// </param>
	/// <param name='hip'>
	/// Hip.
	/// </param>
	/// <param name='rightShoulder'>
	/// Right shoulder.
	/// </param>
	/// <param name='leftShoulder'>
	/// Left shoulder.
	/// </param>
	private void calculateHandDirection(Vector3 head, Vector3 rightHand, Vector3 leftHand, Vector3 hip, Vector3 rightShoulder, Vector3 leftShoulder)
	{
		float leftDist = Vector3.Distance(leftHand, hip);
		float rightDist = Vector3.Distance(rightHand, hip);

		if (leftDist > rightDist)
		{
			headToHandDirection = leftHand - head;
			headToHandDirection.y = leftHand.y - leftShoulder.y;
			handPosition = leftHand;
		}
		else
		{
			headToHandDirection = rightHand - head;
			headToHandDirection.y = rightHand.y - rightShoulder.y;
			handPosition = rightHand;
		}
	}

	public Vector3 LeftArmDirection
	{
		get { return leftArmDirection; }
	}

	public Vector3 RightArmDirection
	{
		get { return rightArmDirection; }
	}

	public Vector3 HeadToHandDirection
	{
		get { return headToHandDirection; }
	}

	public Vector3 HandPosition
	{
		get { return handPosition; }
	}

	public Vector3 CurUserRotation
	{
		get { return curUserRotation; }
	}
}
