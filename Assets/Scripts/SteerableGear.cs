using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerableGear : ControllableBehaviour
{
	public string controlAxis;
	public float deflectionAngle;
	public float deflectionSpeed;

	private WheelCollider wheel;
	private	Transform suspension;

	public bool inverted;

	[SyncVar]
	private float springPosition;
	[SyncVar]
    private float currentRotation;

    private void Awake(){
		wheel = GetComponentInChildren<WheelCollider>();
		suspension = wheel.transform.GetChild(0);
	}

	private void Update(){
		if (isServer)
		{
            float signal = input.GetAxis(controlAxis);
            if (inverted)
            {
                signal *= -1;
            }
            float desiredRotation = signal * deflectionAngle;
            currentRotation += Mathf.Clamp(desiredRotation - currentRotation, -deflectionSpeed * Time.deltaTime, deflectionSpeed * Time.deltaTime);

            wheel.steerAngle = currentRotation;
            Quaternion wheelRot;
			Vector3 wheelPos;
            wheel.GetWorldPose(out wheelPos, out wheelRot);
			springPosition = wheel.transform.InverseTransformPoint(wheelPos).y;
        }
		suspension.localPosition = new Vector3(suspension.localPosition.x, springPosition, suspension.localPosition.z);
		suspension.localEulerAngles = new Vector3(0,currentRotation,0);
	}
}
