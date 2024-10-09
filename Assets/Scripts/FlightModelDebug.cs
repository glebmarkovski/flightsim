using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlightModelDebug : MonoBehaviour
{
	public float area;
	public float Cl;
	public float Cd;
	public float Cdi;
	public float alpha;
	public float theta;
	public float airspeed;
	public Vector3 wingForce;
	public float lift;
	public float drag;

	private LineRenderer forceLine;
	private TMP_Text values;

	private void Awake(){
		values = GetComponentInChildren<TMP_Text>();
		forceLine = GetComponentInChildren<LineRenderer>();
	}

	private void Update(){
		values.text = "Alpha: " + alpha.ToString() + "\nTheta: " + theta.ToString() + "\nCl: " + Cl.ToString() + "\nCd: " + Cd.ToString() + "\nCdi: " + Cdi.ToString() + "\nLift: " + lift.ToString() + "\nDrag: " + drag.ToString() + "\nArea: " + area.ToString() + "\nGlide Ratio: " + (lift/drag).ToString() + "\nAirspeed: " + airspeed.ToString();
		Vector3[] verteces = {transform.position, transform.position + wingForce * 0.00005f};
		forceLine.SetPositions(verteces);
	}
}
