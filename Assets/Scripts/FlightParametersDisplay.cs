using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlightParametersDisplay : MonoBehaviour
{
	private Rigidbody rb;
	private TMP_Text text;	
	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		text = GetComponentInChildren<TMP_Text>();
	}

	private void Update(){
		string tas = rb.velocity.magnitude.ToString("0.0");
		string agl = rb.position.y.ToString("0.0");
		string climb = rb.velocity.y.ToString("0.0");
		string ias = (rb.velocity.magnitude * Air.air.Density(rb.position.y)/1.225f).ToString("0.0");

		text.text = tas + " M/S\n" + ias + "M/S\n" + agl + "M\n" + climb + "M/S\n";
	}
}
