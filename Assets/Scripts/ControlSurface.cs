using Mirror;
using UnityEngine;

public class ControlSurface : ControllableBehaviour
{
    [SerializeField]
    private string controlAxis;
	[SerializeField]
    private float deflectionAnglePositive;
    [SerializeField]
    private float deflectionAngleNegative;
    [SerializeField]
    private float deflectionSpeed;

	private Transform pivot;

    [SerializeField]
    private bool inverted;

	private void Awake(){
		pivot = transform.GetChild(0);
	}

	public float GetTheta()
	{
		return theta;
	}

	[SyncVar]
    private float theta;

	private void Update(){
		if (isServer)
		{
            float signal = input.GetAxis(controlAxis);
            if (inverted)
            {
                signal *= -1;
            }
            float desiredRotation;
            if (signal > 0)
            {
                desiredRotation = signal * deflectionAnglePositive;
            }
            else
            {
                desiredRotation = signal * deflectionAngleNegative;
            }
            theta = theta + Mathf.Clamp(desiredRotation - (theta * 180 / Mathf.PI), -deflectionSpeed * Time.deltaTime, deflectionSpeed * Time.deltaTime) * Mathf.PI / 180;
        }
        pivot.localEulerAngles = new Vector3(theta * -180 / Mathf.PI, pivot.localEulerAngles.y, pivot.localEulerAngles.z);
    }
}
