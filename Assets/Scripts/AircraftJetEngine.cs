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

	[SerializeField]
	private float n1moi;

	[SerializeField]
	private float n2moi;

	[SerializeField]
	private float bpr;

	[SerializeField]
	private float takeoffMfr

	[SerializeField]
	private float n1Max;

	[SerializeField]
	private float n2Max;

	private float n1;
	private float n2;

	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate(){
		
	}

	private bool GetFuel(float volume){
		foreach(TankPriorityLevel fuelLevel in fuel){
			if(fuelLevel.GetFuel(volume)) {
				return true;
			}
		}
		return false;
	}
}
