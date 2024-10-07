using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSynchronizer : MonoBehaviour
{
	public WheelCollider wheel;
	public Transform suspensionModel;
	private void Update(){
		Vector3 wheelPos;
		Quaternion wheelRot;
		wheel.GetWorldPose(out wheelPos, out wheelRot);
		suspensionModel.localPosition = new Vector3(0,transform.InverseTransformPoint(wheelPos).y,0);
	}
}
