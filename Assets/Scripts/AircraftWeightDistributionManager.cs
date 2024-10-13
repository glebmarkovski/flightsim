using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftWeightDistributionManager : MonoBehaviour
{
	private Rigidbody rb;
	private AircraftFuelTank[] tanks;
	private Vector3 initialCom;
	private float initialMass;
	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}
	private void FixedUpdate(){
		Vector3 com = initialCom;
		float mass = initialMass;
		foreach(AircraftFuelTank tank in tanks){
			com = (com * mass + tank.transform.localPosition * tank.GetMass()) / (mass+tank.GetMass());
			mass += tank.GetMass();
		}
		rb.mass = mass;
		rb.centerOfMass = com;
	}
}
