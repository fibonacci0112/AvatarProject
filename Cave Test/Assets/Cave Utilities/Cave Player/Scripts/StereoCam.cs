
using UnityEngine;
using Cave;


public class StereoCam : MonoBehaviour
{
	public float eyeDistance;
	public float eyeConvergence;
	public float defaultHeadHight = 0.0f; 

	private Vector3 curUserRotation;
	private KinectController Kinect;

	private Transform camHolder;

	private GameObject leftCam;
	private GameObject frontCam;
	private GameObject rightCam;
	private GameObject bottomCam;

	private Vector3 headRotation;
	private bool kinectInUse = false;

	private Vector3 startPos;
	
	private BodyArmature armaturePlayer1;

	void Start()
	{
		Kinect = KinectController.Instance;
		if (Kinect != null)
			this.kinectInUse = Kinect.useKinect;
		else
			this.kinectInUse = false;

		camHolder = GameObject.Find("CamHolder").transform;

		leftCam = GameObject.Find ("CameraLeft");
		frontCam = GameObject.Find ("CameraFront");
		rightCam = GameObject.Find ("CameraRight");
		bottomCam = GameObject.Find ("CameraBottom");

		leftCam.GetComponent<Camera> ().stereoSeparation = eyeDistance;
		leftCam.GetComponent<Camera> ().stereoConvergence = eyeConvergence;

		frontCam.GetComponent<Camera> ().stereoSeparation = eyeDistance;
		frontCam.GetComponent<Camera> ().stereoConvergence = eyeConvergence;

		rightCam.GetComponent<Camera> ().stereoSeparation = eyeDistance;
		rightCam.GetComponent<Camera> ().stereoConvergence = eyeConvergence;

		bottomCam.GetComponent<Camera> ().stereoSeparation = eyeDistance;
		bottomCam.GetComponent<Camera> ().stereoConvergence = eyeConvergence;

		startPos = camHolder.localPosition;
		//armaturePlayer1 = new BodyArmature("PlayerMainAvatar");
		
		
	}
	
	
	void Update()
	{
		
		//			setArmatureByKinect(armaturePlayer1);
		
		//Vector3 leftHand = Kinect.getHandLeft();
		//Vector3 rightHand = Kinect.getHandRight();
		//Vector3 leftShoulder = Kinect.getShoulderLeft();
		//Vector3 rightShoulder = Kinect.getShoulderRight();
		Vector3 head = new Vector3 (0.0f, defaultHeadHight, 0.0f);
		if (kinectInUse) {
			head = Kinect.getHead ();
		
			//head.Set(head.x, head.y - GameObject.Find("Player").transform.localPosition.y, head.z);
		}


		
		
		//Vector3 hip = Kinect.getHipCenter();
		
		//setHeadByKinect(head);
		//leftArmDirection = setArmByKinect(leftArm, leftShoulder, leftHand, -1f);
		//rightArmDirection = setArmByKinect(rightArm, rightShoulder, rightHand, 1f);
		
		//calculateHandDirection(head, rightHand, leftHand, hip, rightShoulder, leftShoulder);
		camHolder.localPosition = head; //+ startPos
		setCollider ();
		//GetComponentInParent<CapsuleCollider> ().center.Set (head.x, 0.95f, head.z);
		
	}

	public void setCollider(){
		Vector3 camHolderPos = camHolder.transform.localPosition;
		CapsuleCollider collider = camHolder.GetComponent<CapsuleCollider> ();
		collider.height = camHolderPos.y;
		float collHeight = (collider.radius - collider.height) / 2;
		Vector3 collPos = new Vector3 (collider.center.x, collHeight, collider.center.z);
		collider.center = collPos;
	}

	public void getHeadRotation(){

	Matrix44f m = Kinect.getSensorMatrix (Cave.Kinect.TrackerId.HEAD); //Kinect.getHead();
	Matrix4x4 mat = Matrix4x4.identity;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				// Ja, transponiert. Wo kommt das her? Unbekannt. Vielleicht, weil der Server schon transponiert.
				// Da steht genauso ein "transponieren. Warum?". Möglicherweise wegen VR-Juggler.
				mat [i, j] = m [j, i];
			}
		}
		this.headRotation = mat.MultiplyPoint3x4(Vector3.forward);
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
	/*private void setHeadByKinect(Vector3 head)
	{
		//body.radius = Vector3.Distance(leftShoulder, rightShoulder) / 2.0f;
		//body.height = head.y;
		
		// Alt: Blickrichtung selber berechnen
		/*Quaternion rot = new Quaternion();
		rot.SetLookRotation(leftShoulder - rightShoulder);
		Quaternion rotation = rot * Quaternion.Euler(0, 90, 0);
		curUserRotation = rotation * Vector3.forward;*/
		
		
		// Neu: Blickrichtung vom Server. Sieht erstmal komplizierter aus, aber wenn der das schon berechnet,
		// nehmen wir das für die Wartbarkeit.
		/*Matrix44f m = Kinect.getSensorMatrix (Cave.Kinect.TrackerId.HEAD); //Kinect.getHead();
		Matrix4x4 mat = Matrix4x4.identity;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				// Ja, transponiert. Wo kommt das her? Unbekannt. Vielleicht, weil der Server schon transponiert.
				// Da steht genauso ein "transponieren. Warum?". Möglicherweise wegen VR-Juggler.
				mat[i, j] = m[j, i];
			}
		}
		
		curUserRotation = mat.MultiplyPoint3x4(Vector3.forward);
		curUserRotation[1] = 0f;
	}*/
	

	
	public Vector3 CurUserRotation
	{
		get { return curUserRotation; }
	}
	
}