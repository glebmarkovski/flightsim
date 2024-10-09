using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLandingGear : MonoBehaviour
{
	private Animator anim;

	public bool startExtended;

	public string extendAnim;
	public string retractAnim;

	private void Awake(){
		anim = GetComponent<Animator>();
	}

	private void Start(){
		if (startExtended){
			anim.Play(extendAnim, 0, 1);
		}
		if (!startExtended){
			anim.Play(retractAnim, 0, 1);
		}
	}

	public void Extend(){
		anim.Play(extendAnim);
//		Debug.Log("extending");
	}

	public void Retract(){
		anim.Play(retractAnim);
//		Debug.Log("retracting");
	}
}
