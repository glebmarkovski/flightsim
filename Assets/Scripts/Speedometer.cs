using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
	Rigidbody rb;
	TMP_Text display;
	public string playerName;

	private void Awake(){
		rb = GameObject.Find(playerName).GetComponent<Rigidbody>();
		display = GetComponent<TMP_Text>();
	}
	private void Update(){
		display.text = (rb.velocity.magnitude*2.23f).ToString() + " MPH";
	}

}
