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
	private CoefficientGradient2D machAlphaCoefficient;
	[SerializeField]
	private CoefficientGradient densityCoefficient;
	private Vector3 formerPosition;
	private AircraftJetEngineDebug debug;
	private float n2;
	private float n1;
	[SerializeField]
	private float n2sensitivity;
	[SerializeField]
	private float n1sensitivity;
	[SerializeField]
	private float bpr;

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
		float n2Thrust = throttle * n2/bpr * nominalThrust * machAlphaCoefficient.GetZ(mach, alpha) * densityCoefficient.GetZ(density);
		float n1Thrust = n1*(1-1/bpr) *nominalThrust * machAlphaCoefficient.GetZ(mach, alpha) * densityCoefficient.GetZ(density);
		float thrust;
        if (GetFuel(n2Thrust * fuelConsumption * Time.fixedDeltaTime)) {
            thrust = n1Thrust+n2Thrust;
            n1 += Mathf.Clamp((throttle - n1) * n1sensitivity * 10f,-n1sensitivity,n1sensitivity) * Time.fixedDeltaTime;
            n2 += Mathf.Clamp((throttle - n2) * n2sensitivity * 10f, -n2sensitivity, n2sensitivity) * Time.fixedDeltaTime;
        }
		else{
			thrust = n1Thrust;
            n1 += Mathf.Clamp(n1 * n1sensitivity * -10f, -n1sensitivity,0) * Time.fixedDeltaTime;
            n2 += Mathf.Clamp(n2 * n2sensitivity * -10f, -n2sensitivity,0) * Time.fixedDeltaTime;
        }
        rb.AddForce(transform.forward * thrust);
        if ((n1Thrust+n2Thrust) != 0 && Mathf.Abs(localVelocity.z) < 0.1f && input.GetAxis("Brake") == 0) {
			rb.AddForce(transform.forward * 10f, ForceMode.Acceleration);
		}
		debug.gameObject.SetActive(input.GetButton("Debug"));
		
		if(input.GetButton("Debug")){
			debug.alpha = alpha;
			debug.mach = mach;
			debug.density = density;
			debug.thrust = thrust;
			debug.throttle = throttle;
			debug.n1 = n1;
			debug.n2 = n2;
			debug.n1Thrust = n1Thrust;
			debug.n2Thrust = n2Thrust;
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
