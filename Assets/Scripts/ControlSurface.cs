using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSurface : ControllableBehaviour
{
	public string controlAxis;
	public float deflectionAnglePositive;
	public float deflectionAngleNegative;
	public float deflectionSpeed;

	Transform pivot;

	public bool inverted;

	private void Awake(){
		pivot = transform.GetChild(0);
	}

	public float theta;

	private void Update(){
		float signal = input.GetAxis(controlAxis);
		if (inverted) {
			signal *= -1;
		}
		float desiredRotation;
		if(signal > 0) {
			desiredRotation = signal * deflectionAnglePositive;
		}
		else{
			desiredRotation = signal * deflectionAngleNegative;
		}
		theta = theta + Mathf.Clamp(desiredRotation - (theta * 180 / Mathf.PI), -deflectionSpeed * Time.deltaTime, deflectionSpeed * Time.deltaTime) * Mathf.PI / 180;

		pivot.localEulerAngles = new Vector3(theta * -180 / Mathf.PI, pivot.localEulerAngles.y, pivot.localEulerAngles.z);


	}
}
