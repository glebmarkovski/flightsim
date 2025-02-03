using Mirror;
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

	public Airfoil airfoil;

	public ControlSurface thetaSource;

	private FlightModelDebug debug;

	[SyncVar]
	private float Cd;
    [SyncVar]
    private float Cl;
    [SyncVar]
    private float Cdi;
    [SyncVar]
    private float alpha;
    [SyncVar]
    private float theta;
    [SyncVar]
    private float airspeed;
    [SyncVar]
    private Vector3 wingForce;
    [SyncVar]
    private Vector3 velocity;
    [SyncVar]
    private Vector3 localVelocity;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		debug = GetComponentInChildren<FlightModelDebug>(true);
	}

	void Start(){
		formerPosition = transform.position;
	}

	void FixedUpdate(){
        if (isServer)
        {
            velocity = (transform.position - formerPosition) / Time.fixedDeltaTime;
            localVelocity = new Vector3();
            localVelocity.z = Vector3.Dot(transform.forward, velocity);
            localVelocity.y = Vector3.Dot(transform.up, velocity);
            localVelocity.x = Vector3.Dot(transform.right, velocity);

            alpha = Mathf.Atan2(-localVelocity.y, localVelocity.z);
            theta = thetaSource.GetTheta();
            airspeed = localVelocity.y * localVelocity.y + localVelocity.z * localVelocity.z;

            wingForce = new Vector3();

            Cl = airfoil.GetCl(alpha, theta);
            Cd = airfoil.GetCd(alpha, theta);
            Cdi = Cl * Cl / (Mathf.PI * (span * span / totalArea) * wingEfficiency);

            wingForce += airspeed * Cl * Air.air.Density(transform.position.y) * (Quaternion.AngleAxis(-90, transform.right) * velocity.normalized);
            wingForce += -1.0f * airspeed * (Cd + Cdi) * Air.air.Density(transform.position.y) * velocity.normalized;
            wingForce *= area * 0.5f;

            rb.AddForceAtPosition(wingForce, transform.position);

            formerPosition = transform.position;
        }

		debug.gameObject.SetActive(GlobalDebugSettings.debug);

		if(GlobalDebugSettings.debug)
        {
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
