using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModelAdvanced : ControllableBehaviour
{
	private Vector3 formerPosition;

	private Rigidbody rb;

	public float area;
	public float span;	
	public float totalArea;
	public float wingEfficiency;

	public AircraftStats stats;

	public Airfoil airfoil;

	public ControlSurface thetaSource;

	private FlightModelDebug debug;

	private float Cd;
	private float Cl;
	private float Cdi;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		debug = GetComponentInChildren<FlightModelDebug>(true);
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
		
		float alpha = Mathf.Atan2(-localVelocity.y, localVelocity.z);
		float theta = thetaSource.theta;	
		float airspeed = localVelocity.y*localVelocity.y + localVelocity.z * localVelocity.z;

		Vector3 wingForce = new Vector3();
		
		//Debug.Log("Density: " + Air.air.Density(transform.position.y) + " Temp: " + Air.air.Temperature(transform.position.y) + " Pressure: " + Air.air.Pressure(transform.position.y));


		Cl = airfoil.GetCl(alpha, theta);

		Cd = airfoil.GetCd(alpha, theta);

		Cdi = Cl * Cl / (Mathf.PI * (span * span / totalArea) * wingEfficiency);

		wingForce += airspeed * Cl * Air.air.Density(transform.position.y) * (Quaternion.AngleAxis(-90, transform.right) * velocity.normalized);	
		wingForce += -1.0f * airspeed * (Cd + Cdi) * Air.air.Density(transform.position.y) * velocity.normalized;
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
			debug.theta = theta;
			debug.airspeed = Mathf.Pow(airspeed,0.5f);
			debug.Cl = Cl;
			debug.Cd = Cd;
			debug.Cdi = Cdi;
			debug.area = area;
			debug.lift = area * 0.5f * airspeed * Cl * Air.air.Density(transform.position.y);
			debug.drag = area * 0.5f * airspeed * (Cd+Cdi) * Air.air.Density(transform.position.y); 	
		}
	}
}
