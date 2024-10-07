using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftEngine : ControllableBehaviour
{
	Rigidbody rb;

	public float thrust;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}

	void Update(){
		rb.AddForce(transform.forward * thrust * input.GetAxis("Throttle"));
		Debug.Log(input.GetAxis("Throttle"));
	}
}
