using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
	private Transform vehicleCamera;
	private Vector2 view;
	private float distance;

	public float minDistance;
	public float maxDistance;

	public Vector2 viewSensitivity;
	public float zoomSensitivity;

	private void Awake(){
		vehicleCamera = transform.GetChild(0);
		view = new Vector2();
		distance = (minDistance + maxDistance)*0.5f;
	}

	private void Update(){
		view += new Vector2(Input.GetAxis("Mouse X") * viewSensitivity.x, Input.GetAxis("Mouse Y") * viewSensitivity.y);
		view.y = Mathf.Clamp(view.y, -90, 90);
		transform.rotation = Quaternion.Euler(view.y, view.x, 0);

		distance += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
		vehicleCamera.localPosition = new Vector3(0,0,-distance);
	}
}
