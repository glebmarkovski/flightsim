using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftJetEngine : ControllableBehaviour
{
	private Rigidbody rb;
	[SerializeField]
	private TankPriorityLevel[] fuel;
	[SerializeField]
	private float nominalThrust;
	[SerializeField]
	private float fuelConsumption;
	[SerializeField]
	private CoefficientGradient machCoefficient;
	[SerializeField]
	private CoefficientGradient densityCoefficient;
	[SerializeField]
	private CoefficientGradient alphaCoefficient;
	private Vector3 formerPosition;
	private AircraftJetEngineDebug debug;

	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		debug = GetComponentInChildren<AircraftJetEngineDebug>(true);
	}

	private void Start(){
		formerPosition = transform.position;
	}

	private void FixedUpdate(){
		Vector3 velocity = (transform.position - formerPosition)/Time.fixedDeltaTime;
                Vector3 localVelocity = new Vector3();
                localVelocity.z = Vector3.Dot(transform.forward, velocity);
                localVelocity.y = Vector3.Dot(transform.up, velocity);
                localVelocity.x = Vector3.Dot(transform.right, velocity);
                float alpha = Mathf.Atan2(Mathf.Pow(localVelocity.x * localVelocity.x + localVelocity.y * localVelocity.y, 0.5f), localVelocity.z);
		formerPosition = transform.position;
		float mach = velocity.magnitude / Air.air.Mach(transform.position.y);
		float density = Air.air.Density(transform.position.y);
		float throttle = input.GetAxis("Throttle");
		float thrust = throttle * nominalThrust * machCoefficient.GetC(mach) * densityCoefficient.GetC(density) * alphaCoefficient.GetC(alpha);
		if(!GetFuel(thrust*fuelConsumption*Time.fixedDeltaTime)){
			thrust = 0;
		}
		rb.AddForce(transform.forward * thrust);
		debug.gameObject.SetActive(input.GetButton("Debug"));

		if(input.GetButton("Debug")){
			debug.alpha = alpha;
			debug.mach = mach;
			debug.density = density;
			debug.thrust = thrust;
			debug.throttle = throttle;
		}
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
