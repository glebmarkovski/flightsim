using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCondition : MonoBehaviour
{
	public Vector3 initialVelocity;
	private Vector3 initialPosition;
	private Rigidbody rb;
	private void Awake(){
		rb = GetComponent<Rigidbody>();
	}
	private void Start(){
		initialPosition = transform.position;
		rb.AddForce(initialVelocity, ForceMode.VelocityChange);
	}
	private void FixedUpdate(){
		if (Input.GetButtonDown("Reset")){
			rb.position = initialPosition;
			Debug.Log("Resetting");
			rb.isKinematic = true;
			StartCoroutine(ResumeFlight());
		}
		if (Input.GetButtonDown("Restart")){
			SceneManager.LoadScene(0);
		}
	}

	IEnumerator ResumeFlight(){
		yield return new WaitForSeconds(2.0f);
		rb.isKinematic = false;
		rb.velocity = initialVelocity;
	}

}
