using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
	Rigidbody rb;
	TMP_Text display;

	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		display = GetComponent<TMP_Text>();
	}
	private void Update(){
		display.text = (rb.velocity.magnitude*2.23f).ToString() + " MPH";
	}

}
