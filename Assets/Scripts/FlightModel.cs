using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModel : MonoBehaviour
{
	private Vector3 formerPosition;

	private Rigidbody rb;

	public Vector3 forceCoefficient;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}

	void Start(){
		formerPosition = transform.position;
	}

	void FixedUpdate(){
		Vector3 velocity = transform.position - formerPosition;
		Vector3 localVelocity = new Vector3();
		localVelocity.z = Vector3.Dot(transform.forward, velocity);
		localVelocity.y = Vector3.Dot(transform.up, velocity);
		localVelocity.x = Vector3.Dot(transform.right, velocity);
		//Debug.Log(localVelocity.x.ToString() + " " + localVelocity.y.ToString() + " " + localVelocity.z.ToString());
		
		Vector3 wingForce = new Vector3();

		wingForce += -1.0f * localVelocity.x * forceCoefficient.x * transform.right;
		wingForce += -1.0f * localVelocity.y * forceCoefficient.y * transform.up;
		wingForce += -1.0f * localVelocity.z * forceCoefficient.z * transform.forward;

		Debug.DrawRay(transform.position, wingForce);

		rb.AddForceAtPosition(wingForce, transform.position); 

		formerPosition = transform.position;
	}
}
