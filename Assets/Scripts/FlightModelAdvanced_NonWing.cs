using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModelAdvanced_NonWing : ControllableBehaviour
{
	private Vector3 formerPosition;

	private Rigidbody rb;

	[SerializeField]
	private float area;

	private FlightModelDebug debug;

	[SerializeField]
	private Airfoil airfoil;

    [SyncVar]
    private float Cd;
    [SyncVar]
    private float Cl;
    [SyncVar]
    private float Cdi;
    [SyncVar]
    private float alpha;
    [SyncVar]
    private float beta;
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
		debug = GetComponentInChildren<FlightModelDebug>();
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

            alpha = Mathf.Atan2(Mathf.Pow(localVelocity.x * localVelocity.x + localVelocity.y * localVelocity.y, 0.5f), localVelocity.z);
            beta = Mathf.Atan2(-localVelocity.y, -localVelocity.x);
            airspeed = localVelocity.y * localVelocity.y + localVelocity.z * localVelocity.z + localVelocity.x * localVelocity.x;

            wingForce = new Vector3();

            wingForce += airspeed * airfoil.GetCl(alpha, 0) * Air.air.Density(transform.position.y) * Mathf.Sin(beta) * (Quaternion.AngleAxis(-90, transform.right) * velocity.normalized);
            wingForce += airspeed * airfoil.GetCl(alpha, 0) * Air.air.Density(transform.position.y) * Mathf.Cos(beta) * (Quaternion.AngleAxis(90, transform.up) * velocity.normalized);
            wingForce += -1.0f * airspeed * airfoil.GetCd(alpha, 0) * Air.air.Density(transform.position.y) * velocity.normalized;
            wingForce *= area * 0.5f;

            rb.AddForceAtPosition(wingForce, transform.position);

            formerPosition = transform.position;
        }

		debug.gameObject.SetActive(GlobalDebugSettings.debug);

		if(GlobalDebugSettings.debug){
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
