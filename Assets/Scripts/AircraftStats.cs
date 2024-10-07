using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftStats : MonoBehaviour
{
	private FlightModelAdvanced[] wings;

	public Vector3 totalForce;

	private void Awake(){
		wings = GetComponentsInChildren<FlightModelAdvanced>();

		foreach (FlightModelAdvanced wing in wings){
			wing.stats = this;
		}
	}

	private void Update(){
		if (totalForce.magnitude < 1) { return;}
		Debug.Log(totalForce.ToString() + " " + totalForce.magnitude + " Drag " + Vector3.Dot(transform.forward, totalForce) + " Lift " + Vector3.Dot(transform.up, totalForce));
		totalForce = Vector3.zero;
	}
}
