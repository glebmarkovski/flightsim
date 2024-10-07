using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftBrakes : ControllableBehaviour
{
	private WheelCollider[] wheels;

	public float brakeTorque;


	private void Awake(){
		wheels = GetComponentsInChildren<WheelCollider>();
	}

	private void Update(){
		foreach (WheelCollider wheel in wheels){
			wheel.brakeTorque = Mathf.Clamp(input.GetAxis("Brake"), 0, 1) * brakeTorque;
		}
	}
}