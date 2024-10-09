using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModelAdvanced_NonWing : ControllableBehaviour
{
	private Vector3 formerPosition;

	private Rigidbody rb;

	public float area;

	public AircraftStats stats;

	private FlightModelDebug debug;

	[SerializeField]
	private Airfoil airfoil;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		debug = GetComponentInChildren<FlightModelDebug>();
	}

	void Start(){
		formerPosition = transform.position;
	}

	void FixedUpdate(){
		Vector3 velocity = (transform.position - formerPosition)/Time.fixedDeltaTime;
		Vector3 localVelocity = new Vector3();
		localVelocity.z = Vector3.Dot(transform.forward, velocity);
		localVelocity.y = Vector3.Dot(transform.up, velocity);
		localVelocity.x = Vector3.Dot(transform.right, velocity);
		//Debug.Log(localVelocity.x.ToString() + " " + localVelocity.y.ToString() + " " + localVelocity.z.ToString());
		

		float alpha = Mathf.Atan2(Mathf.Pow(localVelocity.x * localVelocity.x + localVelocity.y * localVelocity.y, 0.5f), localVelocity.z);

		//Debug.Log(alpha);

		float beta = Mathf.Atan2(-localVelocity.y, -localVelocity.x);

		float airspeed = localVelocity.y*localVelocity.y + localVelocity.z * localVelocity.z + localVelocity.x * localVelocity.x;

		Vector3 wingForce = new Vector3();
		
		//Debug.Log("Density: " + Air.air.Density(transform.position.y) + " Temp: " + Air.air.Temperature(transform.position.y) + " Pressure: " + Air.air.Pressure(transform.position.y));

		wingForce += airspeed * airfoil.GetCl(alpha, 0) * Air.air.Density(transform.position.y) * Mathf.Sin(beta) * (Quaternion.AngleAxis(-90, transform.right) * velocity.normalized);	
		wingForce += airspeed * airfoil.GetCl(alpha, 0) * Air.air.Density(transform.position.y) * Mathf.Cos(beta) * (Quaternion.AngleAxis(90, transform.up) * velocity.normalized);	
		wingForce += -1.0f * airspeed * airfoil.GetCd(alpha, 0) * Air.air.Density(transform.position.y) * velocity.normalized;
		wingForce *= area * 0.5f;

		//Debug.DrawRay(transform.position, wingForce*0.00005f);
		//Debug.Log(gameObject.name + " alpha: " + alpha + " Cl: " + airfoil.GetCl(alpha,theta) + " Airspeed: " + Mathf.Pow(airspeed, 0.5f));

		rb.AddForceAtPosition(wingForce, transform.position); 
		
		if (stats != null){
			stats.totalForce += wingForce;
		}

		formerPosition = transform.position;

		//Debug.Log(alpha*180/3.14f);
		
		debug.gameObject.SetActive(input.GetButton("Debug"));

		if(input.GetButton("Debug")){
			debug.wingForce = wingForce;
			debug.alpha = alpha;
			debug.theta = 0;
			debug.airspeed = Mathf.Pow(airspeed,0.5f);
			debug.Cl = airfoil.GetCl(alpha,0);
			debug.Cd = airfoil.GetCd(alpha,0);
			debug.area = area;
			debug.lift = area * 0.5f * airspeed * airfoil.GetCl(alpha, 0) * Air.air.Density(transform.position.y);
			debug.drag = area * 0.5f * airspeed * airfoil.GetCd(alpha, 0) * Air.air.Density(transform.position.y); 	
		}
	}
}
