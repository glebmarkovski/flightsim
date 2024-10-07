using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
	private Transform cameraPivot;
	public float range;
	public LayerMask layerMask;

	private void Awake(){
		cameraPivot = transform.GetChild(0);
	}

	private void Update(){
		if (Input.GetButtonDown("Interact")){
			RaycastHit hit;
			//Debug.Log("Attempting to Interact");
			if(Physics.Raycast(cameraPivot.position, cameraPivot.forward, out hit, range, layerMask)){
				//Debug.Log("Hit Something");
				Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
				if(interactable != null){
					//Debug.Log("Interacting");
					interactable.Interact(gameObject);
				}
			}
		}
	}
}
