using UnityEngine;

public class AircraftBrakes : ControllableBehaviour
{
	private WheelCollider[] wheels;

	[SerializeField]
	private float brakeTorque;

	private void Awake(){
		wheels = GetComponentsInChildren<WheelCollider>();
	}

	private void Update(){
		if (isServer)
		{
            foreach (WheelCollider wheel in wheels)
            {
                wheel.brakeTorque = Mathf.Clamp(input.GetAxis("Brake"), 0, 1) * brakeTorque;
            }
        }
	}
}
