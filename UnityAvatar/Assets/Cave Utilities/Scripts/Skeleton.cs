using UnityEngine;
using System.Collections;

using Cave;

public class Skeleton : MonoBehaviour
{
//	private NetworkObserver networkObserver;
	public GameObject SkeletonJointOriginal;

	private KinectController __kinectController;
	private WiiController __wiiController;
	private GameObject[] __joints = new GameObject[20];
	private bool __enabled = false;
	private bool kinectConected;

	private Cave.ButtonObserverConnector<Cave.WiiMote.ButtonId> __buttonConnector;

	void Start ()
	{
		GameObject kinect = GameObject.Find ("KinectController");
		if (kinect != null) {
						//this.networkObserver = (NetworkObserver)GameObject.FindObjectOfType(typeof(NetworkObserver));
						__wiiController = WiiController.Instance;
		
						__kinectController = KinectController.Instance;

						for (int i = 0; i < 20; i++) {
								__joints [i] = Instantiate (SkeletonJointOriginal, Vector3.zero, Quaternion.identity) as GameObject;
								__joints [i].transform.parent = this.transform;
								__joints [i].name = "SkeletonJoint" + i;
								__joints [i].GetComponent<Renderer> ().enabled = __enabled;
						}
				} else {
			kinectConected = false;
				}
	}

	void Update ()
	{
		if (!kinectConected)
			return;

		for (int i = 0; i < 20; i++)
		{
			__joints[i].transform.localPosition = __kinectController.getSensor((Kinect.TrackerId)i);
		}

		if (__wiiController.buttonMinus()) {
			this.Enabled = !this.Enabled;
		}
	}

	public bool Enabled
	{
		get
		{
			return __enabled;
		}
		set
		{
			__enabled = value;
			for (int i = 0; i < 20; i++)
			{
				__joints[i].GetComponent<Renderer>().enabled = __enabled;
			}
			//if (Game.Instance.IsServer)
			//	networkObserver.SendSkeletonEnabled(__enabled);
		}
	}

	public void OnButtonDown()
	{
		/*if (id == WiiMote.ButtonId.MINUS)
		{
			this.Enabled = !this.Enabled;
		}*/
	}

	public void OnButtonUp(WiiMote.ButtonId id)
	{
	}
}
