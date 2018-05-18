using UnityEngine;
using System.Collections;

public class MouseKeyboardMovement : MonoBehaviour {

	public float walkSpeed = 2.0f;
	public float gravity = 9.81f;
	public float verticalRotationSpeed = -5f;
	public float horizontalRotationSpeed = 5f;
	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (verticalRotationSpeed * Input.GetAxis ("Mouse Y"), 0, 0);		
		this.transform.Rotate (0, horizontalRotationSpeed * Input.GetAxis ("Mouse X"), 0, Space.World);
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= walkSpeed;
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

	}
}
