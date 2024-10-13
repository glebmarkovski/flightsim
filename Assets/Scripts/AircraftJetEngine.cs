using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftJetEngine : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField]
	private float takeOffThrust;
	[SerializeField]
	private TankPriorityLevel[] fuel;

	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate(){
		
	}

	private bool FuelAlvailable(){
		foreach(TankPriorityLevel level in fuel){

		}
	}
}
