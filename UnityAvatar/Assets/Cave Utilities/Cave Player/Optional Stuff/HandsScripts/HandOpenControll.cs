using UnityEngine;
using System.Collections;

/*This skript is a Helper which represents if the Hands are open or Closed*/
public class HandOpenControll : MonoBehaviour {

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

	/*Indicates if this is the left or the right hand*/
	public bool isLeftHand = false; 

	/*Indicates if the hand is Closed*/
	public bool handClosed = false;

    public bool oldHandClosed = false;

    /*The Instance of the Wii Controller*/
    private WiiController wii;

	/*Indicates if the Wii is used*/
	private bool wiiInUse;


	/*Gets the instance if the wii Controller and checks if the wii is used*/
	void Start () {
		wii = WiiController.Instance;
		if (wii != null) {
			wiiInUse = wii.useWii;
		}
	}
	
	/*Checks if the button is pressed, which indicates that the hand is closed and adjusts the state of the hand*/
	void Update () {
		if (wiiInUse) {
			if(isLeftHand){
                this.oldHandClosed = this.handClosed;

                this.handClosed = wii.holdButtonA();

                if (!this.oldHandClosed && this.handClosed)
                {
                    leftHandAnimator.Play("Hand_Left_grab");
                }
                if (this.oldHandClosed && !this.handClosed)
                {
                    leftHandAnimator.Play("Hand_Left_normal_state");
                }
			}
			else{

                this.oldHandClosed = this.handClosed;

                this.handClosed = wii.holdButtonB();

                if (!this.oldHandClosed && this.handClosed)
                {
                    leftHandAnimator.Play("Hand_Right_grab");
                }
                if (this.oldHandClosed && !this.handClosed)
                {
                    leftHandAnimator.Play("Hand_Right_normal_state");
                }
            }
		}
	}
}
