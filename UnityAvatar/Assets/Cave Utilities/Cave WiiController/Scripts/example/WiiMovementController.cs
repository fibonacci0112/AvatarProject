/*
 * Beispiel wie der WiiController in einem Programm der Cave Integriert wurde.
 * Es zeigt wie der Wii Controller Verwendet werden kann und darf gerne als Vorlage für eigene Programme genuzt werden.
 * */

using UnityEngine;
using System.Collections;

using Cave;

public class WiiMovementController : MonoBehaviour
{
	public float defaultWalkVelocityMeterPerSec = 1.4f; // durchschn. laut Wikipedia
	public float defaultTurnVelocityDegPerSec = 90f; // viertel Kreis pro Sekunde

	private float walkVelocityMeterPerSec = 0.0f;
	private float turnVelocityDegPerSec = 0.0f;

	
	private WiiController wiiController;
	private GameObject player;
	private GameObject playerAvatar;

	private bool useKinect = false;
	private KinectController kinect;


	//private NetworkObserver observer;
	/// <summary>
	/// Start this instance.
	/// alle Bewegungseingaben werden hier behandelt (WASD, oder WII)2
	/// </summary>
	void Start()
	{
		
			
		wiiController = WiiController.Instance;
		kinect = GameObject.FindObjectOfType<KinectController> ();
		if (kinect != null) {
			Debug.Log ("Kinect found");
			useKinect = kinect.useKinect;
		}

		
		//this.InitNavigation();
		Debug.Log ("searching player");
		player = GameObject.Find("Player");
		Debug.Log (player.ToString());
		
		playerAvatar = GameObject.Find("AvatarMainPlayer");
		//this.observer = (NetworkObserver)GameObject.FindObjectOfType(typeof(NetworkObserver));
	}
	
	void OnDestroy()  {}
	/*
	private void InitNavigation()
	{
		avatarConnector = new AvatarAdapterConnector(this);
		if (wiiController.useWii)
		{
			wiiController = GetComponentInChildren<WiiController>();
			
			wiiTurnMediator = new SimpleWiiTurnNavigationMediator(wiiController.WiiMote, avatarConnector);
			// RedirectToFrontTurnMediator benutzt Radianten. Da es den selben Callback setTurnVelocity() (siehe unten)
			// nutzt, muss das hier auch in Radiant sein.
			wiiTurnMediator.setTargetTurnVelocity(defaultTurnVelocityDegPerSec * Mathf.Deg2Rad);
			
			wiiWalkMediator = new SimpleWiiWalkNavigationMediator(wiiController.WiiMote, avatarConnector);
			wiiWalkMediator.setTargetWalkVelocity(defaultWalkVelocityMeterPerSec);
		}

	}
	
*/

	void FixedUpdate()
	{
		//this.controllerUpdate ();
		
		if (wiiController.useWii)
		{
			this.controllerUpdate();
		}
			


		/*
		 * Some old stuff were Im not quite shure if it isnt needet anymore.
		 */

		/*if (Config.Instance.UseKinect)
		{
			redirectToFrontTurnMediator.update(Time.deltaTime);
		}
		*/
		//player.GetComponent<Rigidbody>().velocity = new Vector3(); // Gegen Sliden, wenn irgendwo gegengelaufen wird
		
		/*if (turnVelocityDegPerSec != 0.0f)
		{
			// Wenn Kinect angeschlossen, können wir um den User rotieren
			if (Config.Instance.UseKinect)
			{
				Vector3 pos = kinectController.getSensor(Kinect.TrackerId.HIP_CENTER);
				
				// rotieren um den Benutzer: Zur Position hin...
				player.transform.Translate(pos);
				// um diese drehen...
				player.transform.Rotate(Vector3.up, turnVelocityDegPerSec * Time.deltaTime);
				// und wieder zurück.
				player.transform.Translate(-pos);
			}
			else // Ansonsten um den Mittelpunkt, wie früher.
			{
				player.transform.Rotate(Vector3.up, turnVelocityDegPerSec * Time.deltaTime);
			}
			
			playerAvatar.transform.Rotate(Vector3.up, turnVelocityDegPerSec * Time.deltaTime);
			
			
		}
		
		if (walkVelocityMeterPerSec != 0)
		{
			if (Config.Instance.UseKinect)
			{
				player.transform.position = (player.transform.position + player.transform.TransformDirection(kinectController.CurUserRotation) * Time.deltaTime * walkVelocityMeterPerSec);
			}
			else
			{
				player.transform.position = (player.transform.position + player.transform.forward * Time.deltaTime * walkVelocityMeterPerSec);
			}
			
		}*/
	}

	public void controllerUpdate(){
		
		if (wiiController.holdButtonUp())
		{
			if(useKinect){
				Vector3 direction = kinect.getShoulderLeft() - kinect.getShoulderRight();
				float x = direction.x;
				direction.x = -direction.z;
				direction.z = x;
				direction = direction / direction.magnitude;

				direction = player.transform.TransformDirection(direction);
				player.GetComponent<CharacterController>().Move(direction * -this.defaultWalkVelocityMeterPerSec * Time.deltaTime);
			}
			else{
				Vector3 direction = new Vector3(0,0,1);
				direction = player.transform.TransformDirection(direction);
				player.GetComponent<CharacterController>().Move(direction * this.defaultWalkVelocityMeterPerSec * Time.deltaTime);
				}
		}
		
		if (wiiController.holdButtonDown() )
		{
			if(useKinect){
				Vector3 direction = kinect.getShoulderLeft() - kinect.getShoulderRight();
				float x = direction.x;
				direction.x = -direction.z;
				direction.z = x;
				direction = direction / direction.magnitude;
				
				direction = player.transform.TransformDirection(direction);
				player.GetComponent<CharacterController>().Move(direction * this.defaultWalkVelocityMeterPerSec * Time.deltaTime);
			}
			else{
				Vector3 direction = new Vector3(0,0,-1);
				direction = player.transform.TransformDirection(direction);
				player.GetComponent<CharacterController>().Move(direction * this.defaultWalkVelocityMeterPerSec * Time.deltaTime);
			}
		}
		
		if (wiiController.holdButtonRight())
		{
			player.transform.Rotate(new Vector3 (0, this.defaultTurnVelocityDegPerSec * Time.deltaTime, 0));
		}
		
		if (wiiController.holdButtonLeft())
		{
			player.transform.Rotate(new Vector3 (0, -this.defaultTurnVelocityDegPerSec * Time.deltaTime, 0));
		}


		if (wiiController.buttonPlus ()) {
			Application.LoadLevel(Application.loadedLevel);
			//this.GetComponentInChildren<Skeleton>().Enabled = !this.GetComponentInChildren<Skeleton>().Enabled;
		}

		if (wiiController.buttonHome ()) {
			Application.Quit();
		}

		if (wiiController.buttonPlus()){
		}
	}
}
