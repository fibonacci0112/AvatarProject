using UnityEngine;
using System.Collections;

using Cave;

public class KinectController : MonoBehaviour, IAvatarAdapter
{
	public bool useKinect = true;
	public string kinectServerAdress = "localhost";
	public string kinectServerPort = "3884";

	public float defaultWalkVelocityMeterPerSec = 1.4f; // durchschn. laut Wikipedia
	public float defaultTurnVelocityDegPerSec = 90f; // viertel Kreis pro Sekunde

	private float walkVelocityMeterPerSec = 0.0f;
	private float turnVelocityDegPerSec = 0.0f;

	private WalkingInPlaceNavigationMediator wipWalkMediator;
	private RedirectToFrontNavigationMediator redirectToFrontTurnMediator;

	private AvatarAdapterConnector avatarConnector;

	public Kinect Kinect { get; private set; }

	public static KinectController Instance { get; private set; }
	
	void Awake(){
		Instance = this;
	}
	/*
	void Start(){

		if (this.useKinect) {
			string adress = "Kinect@" + this.kinectServerAdress + ":" + this.kinectServerPort;
			Logger.Log ("Verbinde zu Kinect: " + adress);
			Kinect = new Kinect (adress);
		}
	}
*/

	void Start(){

		if (this.useKinect) {
			string adress = "Kinect@" + this.kinectServerAdress + ":" + this.kinectServerPort;
			Logger.Log ("Verbinde zu Kinect: " + adress);
			Kinect = new Kinect (adress);

			avatarConnector = new AvatarAdapterConnector(this);
			wipWalkMediator = new WalkingInPlaceNavigationMediator (Kinect, avatarConnector);

			redirectToFrontTurnMediator = new RedirectToFrontNavigationMediator (Kinect, avatarConnector);

			wipWalkMediator.setEnabled(true);
			redirectToFrontTurnMediator.setEnabled(true);
		}
	}

	void Update()
	{
		try
		{
			if (!this.useKinect)
				return;

			Kinect.update();

		}
		catch (System.Exception e)
		{
			Logger.LogException(e);
		}
	}

	void OnDestroy()
	{
		Kinect = null;
	}

	/// <summary>
	/// Gets the sensor.
	/// </summary>
	/// <returns>
	/// The sensor.
	/// </returns>
	/// <param name='sensor'>
	/// Sensor.
	/// </param>
	public Vector3 getSensor(Kinect.TrackerId sensor)
	{
		if (!this.useKinect)
			return new Vector3();

		Cave.Matrix44f m = Kinect.getTracker(sensor);

		//if (Config.Instance.IsStandalone)
		//	vec.y += Config.Instance.bodyHeightLocalKinect;

		// Der Server verwendet ein rechtshändiges Koordinatensystem (Z zeigt nach hinten),
		// Unity ein linkshändiges (Z zeigt nach vorn), deswegen Z umdrehen.
		Vector3 vec = new Vector3(m[0, 3], m[1, 3], m[2, 3]);
		vec.z *= -1;
		return vec;
	}

	public float getAnalog(Kinect.AnalogId analog) {
		if (!this.useKinect)
			return 0.5f;

		return Kinect.getAnalogValue (analog);
	}

	public Matrix44f getSensorMatrix(Kinect.TrackerId sensor)
	{
		if (!this.useKinect) {
			return null;
		}
		
		Cave.Matrix44f m = Kinect.getTracker(sensor);

		return m;
	}


	public Vector3 getAnkleLeft(){
		return this.getSensor (Kinect.TrackerId.ANKLE_LEFT);
	}

	public Vector3 getAnkleRight(){
		return this.getSensor (Kinect.TrackerId.ANKLE_RIGHT);
	}

	public Vector3 getElbowLeft(){
		return this.getSensor (Kinect.TrackerId.ELBOW_LEFT);
	}
	
	public Vector3 getElbowRight(){
		return this.getSensor (Kinect.TrackerId.ELBOW_RIGHT);
	}

	public Vector3 getFootLeft(){
		return this.getSensor (Kinect.TrackerId.FOOT_LEFT);
	}
	
	public Vector3 getFootRight(){
		return this.getSensor (Kinect.TrackerId.FOOT_RIGHT);
	}

	public Vector3 getHandLeft(){
		return this.getSensor (Kinect.TrackerId.HAND_LEFT);
	}
	
	public Vector3 getHandRight(){
		return this.getSensor (Kinect.TrackerId.HAND_RIGHT);
	}

	public Vector3 getHead(){
		return this.getSensor (Kinect.TrackerId.HEAD);
	}
	
	public Vector3 getHipCenter(){
		return this.getSensor (Kinect.TrackerId.HIP_CENTER);
	}
	
	public Vector3 getHipLeft(){
		return this.getSensor (Kinect.TrackerId.HIP_LEFT);
	}
	
	public Vector3 getHipRight(){
		return this.getSensor (Kinect.TrackerId.HIP_RIGHT);
	}

	public Vector3 getKneeLeft(){
		return this.getSensor (Kinect.TrackerId.KNEE_LEFT);
	}
	
	public Vector3 getKneeRight(){
		return this.getSensor (Kinect.TrackerId.KNEE_RIGHT);
	}

	public Vector3 getShoulderCenter(){
		return this.getSensor (Kinect.TrackerId.SHOULDER_CENTER);
	}
	
	public Vector3 getShoulderLeft(){
		return this.getSensor (Kinect.TrackerId.SHOULDER_LEFT);
	}
	
	public Vector3 getShoulderRight(){
		return this.getSensor (Kinect.TrackerId.SHOULDER_RIGHT);
	}

	public Vector3 getSpine(){
		return this.getSensor (Kinect.TrackerId.SPINE);
	}
	
	public Vector3 getWristLeft(){
		return this.getSensor (Kinect.TrackerId.WRIST_LEFT);
	}
	
	public Vector3 getWristRight(){
		return this.getSensor (Kinect.TrackerId.WRIST_RIGHT);
	}

	// Zustand der rechten Hand im Wertebereich(in Anwendung) von 0.0(offen) bis 1.0 (geschlossen)
	public float getRightHandOpen() {
		return this.getAnalog (Kinect.AnalogId.GESTURE_RIGHT_HAND_STATE);
	}

	public float getWipSpeed(){

		return Kinect.getAnalogValue (Kinect.AnalogId.GESTURE_WIPSPEED);
	}






	/// <summary>
	/// Gets the gesture.
	/// </summary>
	/// <returns>
	/// The gesture.
	/// </returns>
	/*public Gesture getGesture()
	{
		if (!Config.UseKinect)
			return Gesture.None;

		float qualityLeftHand  = VrpnKinect.getValue(VrpnKinect.Analog.LeftHandGestureTrackingState);
		float qualityRightHand = VrpnKinect.getValue(VrpnKinect.Analog.RightHandGestureTrackingState);
		float stateLeftHand	= VrpnKinect.getValue(VrpnKinect.Analog.LeftHandGestureState);
		float stateRightHand   = VrpnKinect.getValue(VrpnKinect.Analog.RightHandGestureState);

		if (qualityLeftHand < 0.5 || qualityRightHand < 0.5)
			return Gesture.None;

		if (stateLeftHand > 0.5 && stateRightHand < 0.5)
			return Gesture.RotateLeft;
		if (stateLeftHand < 0.5 && stateRightHand > 0.5)
			return Gesture.RotateRight;
		if (stateLeftHand > 0.5 && stateRightHand > 0.5)
			return Gesture.Forward;
		else
			return Gesture.None; // Gesture.Backward;
	}*/

	/*private void setArmatureByKinect(BodyArmature armature)
	{
		/*
		Vector3 leftHand = getSensor(VrpnKinect.Sensor.LeftHand);
		Vector3 leftElbow = getSensor(VrpnKinect.Sensor.LeftElbow);
		Vector3 leftShoulder = getSensor(VrpnKinect.Sensor.LeftShoulder);

		leftHand.z *= -1;
		leftElbow.z *= -1;
		leftShoulder.z *= -1;

		//armature.setArmLeftDirection(leftElbow - leftShoulder);
		armature.setArmLowerLeftDirection(leftHand - leftElbow);

		Vector3 head = getSensor(VrpnKinect.Sensor.Head);
		Vector3 waist = getSensor(VrpnKinect.Sensor.Waist);

		//armature.setSpineDirection(head - waist);

		// get angle from Kinect: head - shoulderCenter
		// set neck angle

		// get angle from Kinect: waiste - shoulderCenter
		// set spine angle

		// get angle from Kinect: shoulder - elbow
		// set armupper angle

		// get angle from Kinect: elbow - wrist
		// set armlower angle

		// ---- ?????

		// get angle from Kinect: hip - knee
		// set legupper angle

		// get angle from Kinect: knee - ankle
		// set leglower angle

	}*/


		public void setTargetVelocity(Vec3f vel)
		{
			this.walkVelocityMeterPerSec = vel.__data[2];
		}

		public void setTargetTurnVelocity(Vec3f vel)
		{
			// RedirectToFrontNavigationMediator in Radiant, WiiTurnNavigationMediator auch
			this.turnVelocityDegPerSec = vel[1] * Mathf.Rad2Deg;
		}

		public Vec3f getVelocity()
		{
			return new Vec3f(0f, 0f, this.walkVelocityMeterPerSec);
		}

		public Vec3f getTurnVelocity()
		{
			return new Vec3f(0f, this.turnVelocityDegPerSec * Mathf.Deg2Rad, 0f);
		}




}
