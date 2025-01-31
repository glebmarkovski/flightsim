using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
	private GameObject driver;
	public Vector3 ejectPosition;
	private GameObject HUD;
	[SerializeField]
	private VehicleInput input;
	private GameObject vehicleCamera;  

	private void Awake(){
		HUD = GetComponentInChildren<Canvas>().gameObject;
		vehicleCamera = GetComponentInChildren<VehicleCamera>().gameObject;
	}

	private void Start(){
		foreach (ControllableBehaviour control in GetComponentsInChildren<ControllableBehaviour>()){
			control.SetInput(input);
		}
		vehicleCamera.SetActive(false);
		HUD.SetActive(false);
		input.SetActive(false);
	}

	private void Eject(){
		driver.transform.position = transform.position + transform.right * ejectPosition.x + transform.up * ejectPosition.y + transform.forward * ejectPosition.z;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		driver.SetActive(true);
		HUD.SetActive(false);
		vehicleCamera.SetActive(false);
		input.SetActive(false);
	}

	private void Update(){
		input.UpdateAxes();
		input.UpdateButtons();
		if (driver != null && input.GetButtonDown("Eject")){
			Eject();
		}
	}

	public void Board(GameObject entity){
		driver = entity;
		driver.SetActive(false);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		HUD.SetActive(true);
		vehicleCamera.SetActive(true);
		input.SetActive(true);
	}	

	public override void Interact(GameObject entity){
		Board(entity);
	}
}
