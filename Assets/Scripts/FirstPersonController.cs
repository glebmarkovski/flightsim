using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirstPersonController : NetworkBehaviour
{
	private CharacterController controller;
	public Vector2 walkSpeed;
	public Vector2 runSpeed;
	public float jumpVelocity;
	public float g;
	public float yVelocity;
	public Vector3 movement;
	private Transform cameraPivot;
	public Vector2 viewSensitivity;
	private Vector2 view;
	
	private void Awake(){
		controller = GetComponent<CharacterController>();
		cameraPivot = transform.GetChild(0);
		yVelocity = 0;
		view = new Vector2();
		Application.targetFrameRate = 1000;
	}

	private void OnEnable(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnDisable(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update(){
		if(!isLocalPlayer){
			return;
		}
		movement = new Vector3();
		if (Input.GetButton("Run")){
			movement += transform.forward * Input.GetAxis("Vertical") * runSpeed.y;
			movement += transform.right * Input.GetAxis("Horizontal") * runSpeed.x;
		}	
		else{
			movement += transform.forward * Input.GetAxis("Vertical") * walkSpeed.y;
			movement += transform.right * Input.GetAxis("Horizontal") * walkSpeed.x;
		}
		if (Input.GetButton("Jump") && controller.isGrounded){
			yVelocity = jumpVelocity;
		}

		view += new Vector2(Input.GetAxis("Mouse X") * viewSensitivity.x, Input.GetAxis("Mouse Y") * viewSensitivity.y);

		view.y = Mathf.Clamp(view.y, -90, 90);

		transform.localRotation = Quaternion.Euler(0, view.x, 0);
		cameraPivot.localRotation = Quaternion.Euler(view.y, 0, 0);

		//Debug.Log(cameraPivot.eulerAngles.x);
	}
	private void FixedUpdate(){
		if(!isLocalPlayer){
			return;
		}
		if (controller.isGrounded && yVelocity < 0){
			yVelocity = -1f;
		}
		else{
			yVelocity -= g * Time.fixedDeltaTime;
		}
		movement += new Vector3(0,yVelocity,0);
		controller.Move(movement * Time.fixedDeltaTime);	
		Debug.Log(movement);
	}
}
